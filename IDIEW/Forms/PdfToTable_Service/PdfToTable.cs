using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDIEW
{
    public partial class PdfToTable : Form
    {
        private Panel _panelContenedor;
        private static readonly HttpClient client = new HttpClient();


        public PdfToTable(Panel panelContenedor)
        {
            InitializeComponent();
        }

        private async void Btn_Enviar_Click(object sender, EventArgs e)
        {

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Multiselect = false
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var filePath = ofd.FileName;
                var fileName = Path.GetFileName(filePath);

                var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(File.OpenRead(filePath));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");

                content.Add(fileContent, "file", fileName); // "archivo" debe coincidir con el nombre del campo en Make

                try
                {
                    var response = await client.PostAsync("https://hook.us2.make.com/68y6m0f0qygqm8fquy219edc4xa8q8u6", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("PDF enviado correctamente.");
                    }
                    else
                    {
                        string respuesta = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al enviar PDF: {response.StatusCode}\n{respuesta}");
                    }
                }
                catch (HttpRequestException hre)
                {
                    MessageBox.Show("Error de solicitud HTTP:\n" + hre.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error general:\n" + ex.Message);
                }
            }
        }
    }
}
