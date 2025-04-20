using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDIEW
{
    public partial class add_profile : Form
    {
        private Panel _panelContenedor;
        private FirebaseClient client;

        public add_profile(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new Form1(_panelContenedor));
        }

        private void AbrirFormularioEnPanel(Form nuevoFormulario)
        {
            _panelContenedor.Controls.Clear();
            nuevoFormulario.TopLevel = false;
            nuevoFormulario.Dock = DockStyle.Fill;
            _panelContenedor.Controls.Add(nuevoFormulario);
            nuevoFormulario.Show();
        }

        private async void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación: Verificar si los TextBox no están vacíos
                if (string.IsNullOrEmpty(TXT_ALTO.Text) ||
                    string.IsNullOrEmpty(TXT_ANCHO.Text) ||
                    string.IsNullOrEmpty(TXT_PAGINA.Text) ||
                    string.IsNullOrEmpty(TXT_RANGOS.Text) ||
                    string.IsNullOrEmpty(Nombre_txt.Text))
                {
                    MessageBox.Show("Todos los campos deben ser completados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear un objeto Perfil desde los TextBox
                var nuevoPerfil = new Classes.Perfil
                {
                    Alto = TXT_ALTO.Text,
                    Ancho = TXT_ANCHO.Text,
                    Pagina = TXT_PAGINA.Text,
                    Rangos = TXT_RANGOS.Text
                };

                string nombrePerfil = Nombre_txt.Text;

                // Guardar en Firebase
                bool guardado = await Classes.FireBase.PerfilServiceInstance.GuardarPerfilAsync(nombrePerfil, nuevoPerfil);

                if (guardado)
                {
                    DialogResult result = MessageBox.Show("Datos guardados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (result == DialogResult.OK)
                    {
                        // Limpiar los campos de texto
                        TXT_ALTO.Clear();
                        TXT_ANCHO.Clear();
                        TXT_PAGINA.Clear();
                        TXT_RANGOS.Clear();
                        Nombre_txt.Clear();

                        AbrirFormularioEnPanel(new Form1(_panelContenedor)); // Esto asumo que es tu recarga
                    }
                }
                else
                {
                    MessageBox.Show("Error al guardar el perfil en Firebase.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void add_profile_Load(object sender, EventArgs e)
        {

        }
    }
}
