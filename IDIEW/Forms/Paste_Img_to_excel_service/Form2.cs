using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace IDIEW
{
    

    public partial class Form2 : Form
    {
        private string imageFolderPath = "";
        private string excelFilePath = "";  // Ruta del archivo Excel
        private string startCell = "";

        private Panel _panelContenedor;

        public Form2(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    imageFolderPath = folderDialog.SelectedPath;
                }

                ImgRute_txt.Text = imageFolderPath;
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excelFilePath = openFileDialog.FileName;
                }

                ExlRute_txt.Text = excelFilePath;
            }
        }



        private void InsertImagesInExcel()
        {
            try
            {
                // Crear una instancia de Excel
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excelApp.Workbooks.Open(excelFilePath);
                Worksheet worksheet = (Worksheet)workbook.Worksheets[1];  // Selecciona la primera hoja

                // Establecer la altura de todas las filas a 195 píxeles
                worksheet.Rows.RowHeight = 195;

                // Obtenemos las imágenes de la carpeta
                var imageFiles = Directory.GetFiles(imageFolderPath, "*.*")
                                          .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".jpeg"))
                                          .ToList();

                Invoke(new Guna.UI2.WinForms.Internal.Action(() =>
                {
                    guna2ProgressBar1.Maximum = imageFiles.Count;
                    guna2ProgressBar1.Value = 0;
                }));

                // Obtener la celda de inicio a partir de la referencia proporcionada (ejemplo: G7)
                Range startRange = worksheet.Range[startCell];
                int startRow = startRange.Row;
                int startColumn = startRange.Column;

                int currentRow = startRow;  // Empezamos en la fila de la celda de inicio
                int currentColumn = startColumn;

                foreach (var imageFile in imageFiles)
                {
                    // Insertar la imagen en la celda correspondiente
                    string imagePath = imageFile;
                    Picture picture = worksheet.Pictures().Insert(imagePath);

                    // Calcular la posición y tamaño de la imagen
                    Range targetCell = worksheet.Cells[currentRow, currentColumn];
                    picture.Top = targetCell.Top;
                    picture.Left = targetCell.Left;
                    picture.Width = targetCell.Width;  // Ajustar el tamaño de la imagen para que llene la celda
                    picture.Height = targetCell.Height;

                    // Mover a la siguiente fila
                    currentRow++;

                    // Actualizar el ProgressBar (esto se hace en el hilo principal)
                    Invoke(new Guna.UI2.WinForms.Internal.Action(() =>
                    {
                        guna2ProgressBar1.Value++;
                        lblProgress.Text = $"{guna2ProgressBar1.Value} / {guna2ProgressBar1.Maximum}"; // Actualizar el texto del progreso
                    }));
                }

                // Guardar los cambios en el archivo Excel
                workbook.Save();

                // Hacer visible Excel (esto se ejecuta en el hilo principal)
                Invoke(new System.Action(() => { excelApp.Visible = true; }));

                // Liberar recursos
                workbook.Close(false);
                excelApp.Quit();
                workbook = null;
                excelApp = null;
            }
            catch (Exception ex)
            {
                // Si ocurre algún error, mostrar un mensaje en la interfaz de usuario
                Invoke(new System.Action(() =>
                {
                    MessageBox.Show("Error: " + ex.Message);
                }));
            }
        }

        private async void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            startCell = startCell_txt.Text;
            if (string.IsNullOrEmpty(imageFolderPath) || string.IsNullOrEmpty(excelFilePath))
            {
                MessageBox.Show("Por favor selecciona una carpeta de imágenes y un archivo Excel.");
                return;
            }

            // Deshabilitar el botón para evitar que el usuario haga clic varias veces
            btnInsertImages.Enabled = false;
            guna2ProgressBar1.Visible = true;

            // Ejecutar la inserción de imágenes en un hilo secundario
            await Task.Run(() =>
            {
                InsertImagesInExcel();
            });

            // Volver a habilitar el botón después de que haya terminado
            btnInsertImages.Enabled = true;
            guna2ProgressBar1.Visible = false;
        }
    }
}
