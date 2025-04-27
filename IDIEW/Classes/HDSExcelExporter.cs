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
using Newtonsoft.Json.Linq;

namespace IDIEW.Classes
{
    public static class HDSExcelExporter
    {
        private static bool _cancelar = false;
        public static string CarpetaSeleccionada { get; private set; }


        public static void CancelarExportacion()
        {
            _cancelar = true;
        }

        public static void Restructurar()
        {
            // Ruta donde están tus archivos JSON
            string folderPath = CarpetaSeleccionada ; ;  // Cambia esta ruta

            // Obtener todos los archivos .json en la carpeta
            string[] files = Directory.GetFiles(folderPath, "*.json");

            foreach (string file in files)
            {
                try
                {
                    // Leer el contenido del archivo JSON
                    string jsonContent = File.ReadAllText(file);

                    // Parsear el JSON
                    JObject jsonObject = JObject.Parse(jsonContent);

                    // Verificar si "Datos" y "PictogramasSeleccionados" están bien estructurados
                    if (jsonObject["Datos"] != null && jsonObject["PictogramasSeleccionados"] != null)
                    {
                        // Mover "PictogramasSeleccionados" fuera de "Datos" si está dentro
                        var pictogramasSeleccionados = jsonObject["Datos"]["PictogramasSeleccionados"];
                        if (pictogramasSeleccionados != null)
                        {
                            jsonObject["PictogramasSeleccionados"] = pictogramasSeleccionados;
                            
                        }

                        // Guardar el archivo corregido
                        string correctedJsonContent = jsonObject.ToString();
                        File.WriteAllText(file, correctedJsonContent);

                        Console.WriteLine($"Archivo corregido: {file}");
                    }
                    else
                    {
                        Console.WriteLine($"No se necesita corrección en el archivo: {file}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar el archivo {file}: {ex.Message}");
                }
            }

            Console.WriteLine("Proceso completado.");
        }


        public static void ExportarDesdeCarpeta(ProgressBar progressBar, Form PdfToTable, Panel panelContenedor)
        {
            Thread hilo = new Thread(() =>
            {
                bool cancelar = false;
                List<string> archivosFallidos = new List<string>();

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

                CarpetaSeleccionada = carpetaSeleccionada;
                if (string.IsNullOrEmpty(carpetaSeleccionada)) return;

                string[] archivosJson = Directory.GetFiles(carpetaSeleccionada, "*.json");

                List<JsonConDatos> datosList = new List<JsonConDatos>();
                foreach (var archivo in archivosJson)
                {
                    try
                    {
                        string json = File.ReadAllText(archivo);
                        var datos = JsonConvert.DeserializeObject<Classes.JsonConDatos>(json);

                        datos.Archivo = archivo; // 
                        datosList.Add(datos);
                    }
                    catch
                    {
                        archivosFallidos.Add(Path.GetFileName(archivo));
                    }
                }

                var pictogramasDisponibles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas"))
                                                  .Select(Path.GetFileName)
                                                  .ToList();

                DialogResult result = DialogResult.None;
                PdfToTable.Invoke((MethodInvoker)(() =>
                {
                    var editor = new Forms.PdfToTable_Service.EditorHDS(datosList, pictogramasDisponibles);
                    result = editor.ShowDialog();
                }));

                if (result != DialogResult.OK)
                {
                    MessageBox.Show("Exportación cancelada por el usuario.");
                    return;
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

                    hojaBase.Copy(After: workbook.Sheets[workbook.Sheets.Count]); // Copiar al final
                    Worksheet nuevaHoja = (Worksheet)workbook.Sheets[workbook.Sheets.Count]; // La nueva hoja es la última

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

                hojaBase.Delete();


                string rutaSalida = Path.Combine(carpetaSeleccionada, "HDS_Exportado.xlsx");
                workbook.SaveAs(rutaSalida);

                string mensaje = "Exportación completada con éxito.";
                if (archivosFallidos.Count > 0)
                {
                    mensaje += "\n\nArchivos que no se pudieron procesar:\n" +
                               string.Join("\n", archivosFallidos);

                    File.WriteAllLines(Path.Combine(carpetaSeleccionada, "Errores_JSON.txt"), archivosFallidos);
                }

                MessageBox.Show(mensaje);

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

            if (datosConPictograma.PictogramasSeleccionados != null && datosConPictograma.PictogramasSeleccionados.Any())
            {
                var celdaK8 = hoja.Cells[8, 11] as Range;

                float left = (float)celdaK8.Left + 5;
                float topInicial = (float)celdaK8.Top + 5;
                float altoDisponible = (float)celdaK8.Height - 10;
                int totalPictogramas = datosConPictograma.PictogramasSeleccionados.Count;

                float alturaPictograma = Math.Min(70, altoDisponible / totalPictogramas);
                float anchoPictograma = 60;

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

                        top += alturaPictograma + 2;
                    }
                }
            }
        }
    }
    }

