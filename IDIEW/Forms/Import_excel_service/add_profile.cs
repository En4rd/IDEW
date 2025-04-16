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
                    string.IsNullOrEmpty(TXT_RANGOS.Text))
                {
                    // Mostrar un mensaje de error si algún campo está vacío
                    MessageBox.Show("Todos los campos deben ser completados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear un objeto JSON con los valores ingresados en los TextBox
                JObject newData = new JObject
                {
                    ["Alto"] = TXT_ALTO.Text,
                    ["Ancho"] = TXT_ANCHO.Text,
                    ["Pagina"] = TXT_PAGINA.Text,
                    ["Rangos"] = TXT_RANGOS.Text,
                };

                // Obtener la tabla seleccionada y la clave (nombre) desde el TextBox
                string table = "Perfiles";
                string userKey = Nombre_txt.Text;

                // Enviar los datos a Firebase
                FirebaseResponse response = await client.SetAsync($"{table}/{userKey}", newData);

                // Mostrar un mensaje de éxito
                DialogResult result = MessageBox.Show("Datos guardados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Acción a realizar cuando se presiona "OK"
                if (result == DialogResult.OK)
                {
                    // Limpiar los campos de texto
                    TXT_ALTO.Clear();
                    TXT_ANCHO.Clear();
                    TXT_PAGINA.Clear();
                    TXT_RANGOS.Clear();
                    Nombre_txt.Clear();
                    AbrirFormularioEnPanel(new Form1(_panelContenedor));
                }
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error si algo falla al conectar con Firebase
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void add_profile_Load(object sender, EventArgs e)
        {
            string url = "https://enard-d0ae2-default-rtdb.firebaseio.com/";
            string secretKey = "AsHzTIxmlBw4qAzqjveHp6U8XpZc5iwYXohNB1xa";

            try
            {
                var config = new FirebaseConfig
                {
                    AuthSecret = secretKey,
                    BasePath = url
                };

                client = new FirebaseClient(config); // Pasamos el objeto de configuración al constructor de FirebaseClient
 

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al intentar conectar a Firebase: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
