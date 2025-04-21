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
        public static void ExportarDesdeCarpeta(ProgressBar progressBar)
        {
            Thread hilo = new Thread(() =>
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                if (folderDialog.ShowDialog() != DialogResult.OK) return;

                string[] archivosJson = Directory.GetFiles(folderDialog.SelectedPath, "*.json");

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
                    Worksheet nuevaHoja = (Worksheet)workbook.Sheets.Add(After: workbook.Sheets[hojaIndex]);
                    hojaBase.Copy(Before: nuevaHoja);
                    nuevaHoja = workbook.Sheets[hojaIndex + 1];

                    // Asignar NoHDS incremental solo con número si está vacío
                    if (string.IsNullOrWhiteSpace(item.Datos.NoHDS))
                    {
                        item.Datos.NoHDS = contadorHDS.ToString();
                    }

                    string nombreSeguro = item.Datos.NombreDelProducto ?? $"Hoja{hojaIndex}";
                    nuevaHoja.Name = nombreSeguro.Length > 31 ? nombreSeguro.Substring(0, 31) : nombreSeguro;

                    LlenarHoja(nuevaHoja, item.Datos);
                    hojaIndex++;
                    contadorHDS++;

                    progressBar.Invoke((MethodInvoker)(() =>
                    {
                        progressBar.Value += 1;
                    }));
                }

                string rutaSalida = Path.Combine(folderDialog.SelectedPath, "HDS_Exportado.xlsx");
                workbook.SaveAs(rutaSalida);

                workbook.Close(false);
                excelApp.Quit();

                MessageBox.Show("Exportación completada con éxito.");
            });

            hilo.SetApartmentState(ApartmentState.STA);
            hilo.Start();
        }

        private static void LlenarHoja(Worksheet hoja, Classes.HDSData datos)
        {
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
        }
    }
}

