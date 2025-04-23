using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace IDIEW.Classes
{
    public static class HDSExcelExporter
    {

        private static bool _cancelar = false;

        public static void CancelarExportacion()
        {
            _cancelar = true;
        }

        public static void ExportarDesdeCarpeta(ProgressBar progressBar, Form PdfToTable, Panel panelContenedor)
        {
            Thread hilo = new Thread(() =>
            {
                bool cancelar = false;

                string carpetaSeleccionada = string.Empty;
                PdfToTable.Invoke((MethodInvoker)(() =>
                {
                    using (var folderDialog = new FolderBrowserDialog())
                    {
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            carpetaSeleccionada = folderDialog.SelectedPath;
                        }
                    }
                }));

                if (string.IsNullOrEmpty(carpetaSeleccionada)) return;

                string[] archivosJson = Directory.GetFiles(carpetaSeleccionada, "*.json");

                List<JsonConDatos> datosList = new List<JsonConDatos>();
                foreach (var archivo in archivosJson)
                {
                    try
                    {
                        string json = File.ReadAllText(archivo);
                        var datos = JsonConvert.DeserializeObject<Classes.HDSData>(json);

                        datosList.Add(new JsonConDatos
                        {
                            Archivo = archivo,
                            Datos = datos
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al procesar {archivo}: {ex.Message}");
                    }
                }

                string rutaPlantilla = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plantillas", "plantilla.xlsx");
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Open(rutaPlantilla);

                Worksheet hojaBase = workbook.Sheets[1];
                int hojaIndex = 1;

                progressBar.Invoke((MethodInvoker)(() =>
                {
                    progressBar.Maximum = datosList.Count;
                    progressBar.Value = 0;
                }));

                int contadorHDS = 1;
                foreach (var item in datosList)
                {
                    if (cancelar)
                        break;

                    var pictogramasDisponibles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas"))
                                                           .Select(Path.GetFileName)
                                                           .ToList();

                    DialogResult result = DialogResult.None;
                    PdfToTable.Invoke((MethodInvoker)(() =>
                    {
                        int indiceActual = datosList.IndexOf(item) + 1;
                        var editor = new Forms.PdfToTable_Service.EditorHDS(item, pictogramasDisponibles, indiceActual, datosList.Count);
                        result = editor.ShowDialog();
                    }));

                    if (result != DialogResult.OK)
                    {
                        cancelar = true;
                        break;
                    }

                    Worksheet nuevaHoja = (Worksheet)workbook.Sheets.Add(After: workbook.Sheets[hojaIndex]);
                    hojaBase.Copy(Before: nuevaHoja);
                    nuevaHoja = workbook.Sheets[hojaIndex + 1];

                    if (string.IsNullOrWhiteSpace(item.Datos.NoHDS))
                        item.Datos.NoHDS = contadorHDS.ToString();

                    string nombreSeguro = item.Datos.NombreDelProducto ?? $"Hoja{hojaIndex}";
                    nuevaHoja.Name = nombreSeguro.Length > 31 ? nombreSeguro.Substring(0, 31) : nombreSeguro;

                    LlenarHoja(nuevaHoja, item);

                    hojaIndex++;
                    contadorHDS++;

                    progressBar.Invoke((MethodInvoker)(() =>
                    {
                        progressBar.Value += 1;
                    }));
                }

                if (!cancelar)
                {
                    string rutaSalida = Path.Combine(carpetaSeleccionada, "HDS_Exportado.xlsx");
                    workbook.SaveAs(rutaSalida);
                    MessageBox.Show("Exportación completada con éxito.");
                }
                else
                {
                    MessageBox.Show("Exportación cancelada por el usuario.");
                }

                workbook.Close(false);
                excelApp.Quit();

            });

            hilo.SetApartmentState(ApartmentState.STA);
            hilo.Start();
        }

        private static void LlenarHoja(Worksheet hoja, JsonConDatos datosConPictograma)
        {
            var datos = datosConPictograma.Datos;

            hoja.Cells[3, 1] = datos.NoHDS;
            hoja.Cells[3, 2] = datos.NombreDelProducto;
            hoja.Cells[3, 3] = datos.NoCAS;
            hoja.Cells[3, 4] = datos.LimiteDeExposicion;
            hoja.Cells[3, 5] = datos.CaracteristicasFisicoQuimicas;
            hoja.Cells[3, 6] = datos.MedidasSanitarias;
            hoja.Cells[3, 7] = datos.Sintomas;
            hoja.Cells[3, 8] = datos.PrimerosAuxilios;
            hoja.Cells[3, 9] = datos.OrganosAfectados;

            hoja.Cells[8, 1] = datos.NoHDS;
            hoja.Cells[8, 2] = datos.Area;
            hoja.Cells[8, 3] = datos.Fabricante;
            hoja.Cells[8, 4] = datos.NombreDelProducto;
            hoja.Cells[8, 5] = datos.Uso;
            hoja.Cells[8, 6] = datos.Cantidad;
            hoja.Cells[8, 7] = datos.FechaDeActualizacionDeHDS;
            hoja.Cells[8, 8] = datos.HDSEnEspanol;
            hoja.Cells[8, 9] = datos.TemperaturaDeInflamacion;
            hoja.Cells[8, 10] = datos.Descripcion;
            hoja.Cells[8, 12] = datos.EquipoDeProteccionPersonal;
            hoja.Cells[8, 13] = datos.Incompatibilidad;
            hoja.Cells[8, 14] = datos.CondicionesAEvitar;

            // Insertar imágenes de pictogramas en la celda K8
            if (datosConPictograma.PictogramasSeleccionados != null && datosConPictograma.PictogramasSeleccionados.Any())
            {
                var celdaK8 = hoja.Cells[8, 11] as Range;

                float left = (float)celdaK8.Left + 5;
                float topInicial = (float)celdaK8.Top + 5;
                float altoDisponible = (float)celdaK8.Height - 10; // un poco de margen
                int totalPictogramas = datosConPictograma.PictogramasSeleccionados.Count;

                // Altura sugerida por pictograma (ajustable según lo que quepa)
                float alturaPictograma = Math.Min(70, altoDisponible / totalPictogramas);
                float anchoPictograma = 60; // puedes ajustar si lo prefieres más chico

                float top = topInicial;

                foreach (var pictograma in datosConPictograma.PictogramasSeleccionados)
                {
                    string rutaPictograma = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", pictograma);
                    if (File.Exists(rutaPictograma))
                    {
                        hoja.Shapes.AddPicture(rutaPictograma,
                            Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoCTrue,
                            left, top, anchoPictograma, alturaPictograma);

                        top += alturaPictograma + 2; // espacio entre pictogramas
                    }
                }
            }
        
        }
        
    }
}

