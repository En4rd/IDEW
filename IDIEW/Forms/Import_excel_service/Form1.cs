using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Threading;
using System.IO;
using FireSharp.Config;
using FireSharp;
using FireSharp.Response;
using static IDIEW.Classes.ImagnImportService;
using static IDIEW.Classes.FireBase;
using IDIEW.Classes;

namespace IDIEW
{
    public partial class Form1 : Form
    {
        private Panel _panelContenedor;
        private FirebaseClient client;
        private Thread hiloImportacion;

        public Form1(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            hiloImportacion = new Thread(ImportarImagenesDesdeExcelAWord);
            
        }

        private async void ImportarImagenesDesdeExcelAWord()
        {
            try
            {
                var config = new Classes.ImagnImportService.ImportConfig
                {
                    ExcelFilePath = TXT_RUTAEXEL.Text,
                    WordFilePath = TXT_RUTAWORD.Text,
                    Rangos = TXT_RANGOS.Text.Split(','),
                    AnchoCm = float.Parse(TXT_ANCHO.Text),
                    AltoCm = float.Parse(TXT_ALTO.Text),
                    PaginaDestino = int.Parse(TXT_PAGINA.Text)
                };

                var servicio = new ImagenImportService();
                await servicio.ImportarAsync(config,
                    (actual, total) =>
                    {
                        guna2ProgressBar1.Invoke((MethodInvoker)(() =>
                        {
                            guna2ProgressBar1.Value = actual;
                        }));

                        lbl_CONTADOR.Invoke((MethodInvoker)(() =>
                        {
                            lbl_CONTADOR.Text = $"{actual} / {total}";
                        }));
                    },
                    (error, hoja) =>
                    {
                        Registro_dgv.Invoke((MethodInvoker)(() =>
                        {
                            Registro_dgv.Rows.Add(error, hoja);
                        }));
                        MessageBox.Show($"Error al copiar rango en hoja {hoja}: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });

                MessageBox.Show("Proceso completado correctamente.", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la importación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFilePath(Guna.UI2.WinForms.Guna2TextBox gunatextBox, string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                gunatextBox.Text = openFileDialog.FileName;
            }
        }

        private void btnExaminarExcel_Click(object sender, EventArgs e)
        {
            SetFilePath(TXT_RUTAEXEL, "Archivos de Excel|*.xlsx;*.xls;*.xlsm");
            string nombreArchivoExcel = Path.GetFileName(TXT_RUTAEXEL.Text);
            exl_filename_lbl.Text = nombreArchivoExcel;
            ukn1_pic.Visible = false;
            excel_pic.Visible = true;
        }

        private void btnExaminarWord_Click(object sender, EventArgs e)
        {
            SetFilePath(TXT_RUTAWORD, "Archivos de Word|*.docx;*.doc");
            string nombreArchivoWord = Path.GetFileName(TXT_RUTAWORD.Text);
            wrd_filename_lbl.Text = nombreArchivoWord;
            ukn2_pic.Visible = false;
            word_pic.Visible = true;

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // Crear un nuevo hilo para la importación de imágenes
            hiloImportacion.Start();
            guna2ProgressBar1.Visible = true;
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            hiloImportacion.Abort();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            ThemeManager.AplicarTema(this);
            try
            {
                var perfiles = await Classes.FireBase.PerfilServiceInstance.ObtenerPerfilesAsync();

                if (perfiles != null)
                {
                    foreach (var perfil in perfiles.Keys)
                    {
                        Perfiles_cmb.Items.Add(perfil);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al intentar conectar a Firebase: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void Perfiles_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = Perfiles_cmb.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(seleccion)) return;

            try
            {
                // Ya no necesitas crear una nueva instancia
                var perfiles = await Classes.FireBase.PerfilServiceInstance.ObtenerPerfilesAsync();

                if (perfiles != null && perfiles.ContainsKey(seleccion))
                {
                    var perfilSeleccionado = perfiles[seleccion];

                    // Mostrar en etiquetas o textboxes
                    TXT_ALTO.Text = perfilSeleccionado.Alto;
                    TXT_ANCHO.Text = perfilSeleccionado.Ancho;
                    TXT_PAGINA.Text = perfilSeleccionado.Pagina;
                    TXT_RANGOS.Text = perfilSeleccionado.Rangos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el perfil seleccionado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new add_profile(_panelContenedor));
        }

        private void AbrirFormularioEnPanel(Form nuevoFormulario)
        {
            _panelContenedor.Controls.Clear();
            nuevoFormulario.TopLevel = false;
            nuevoFormulario.Dock = DockStyle.Fill;
            _panelContenedor.Controls.Add(nuevoFormulario);
            nuevoFormulario.Show();
        }

        private async void Btn_Eliminar_Click(object sender, EventArgs e)
        {
            string seleccion = Perfiles_cmb.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(seleccion))
            {
                MessageBox.Show("Por favor, seleccione un perfil para eliminar.");
                return;
            }

            try
            {
                var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar el perfil '{seleccion}'?",
                                                    "Confirmar eliminación",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    bool eliminado = await Classes.FireBase.PerfilServiceInstance.EliminarPerfilAsync(seleccion);

                    if (eliminado)
                    {
                        MessageBox.Show("Perfil eliminado correctamente.");

                        // Eliminar el perfil del ComboBox
                        Perfiles_cmb.Items.Remove(seleccion);

                        // Limpiar campos asociados al perfil
                        TXT_ALTO.Clear();
                        TXT_ANCHO.Clear();
                        TXT_PAGINA.Clear();
                        TXT_RANGOS.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al eliminar el perfil.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar perfil: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
