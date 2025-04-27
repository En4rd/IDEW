using Guna.UI2.WinForms;
using IDIEW.Classes;
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
    public partial class Main_form : Form
    {
        private bool desplegado = true;
        private void AbrirFormularioEnPanel(Form Form1)
        {
            Form1.TopLevel = false;
            Form1.Dock = DockStyle.Fill;   // Hace que ocupe todo el panel
            panelContenedor.Controls.Clear();
            guna2ShadowForm1.SetShadowForm(this);
            panelContenedor.Controls.Add(Form1); // Agrega el form al panel 
            Form1.Show();                  // Muestra el form
        }
        public Main_form()
        {
            InitializeComponent();
            AbrirFormularioEnPanel(new Form1(panelContenedor));
            ThemeManager.AplicarTema(this);
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new Form1(panelContenedor));
        }

        private void guna2GradientTileButton2_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new Form2(panelContenedor));
        }

        private void guna2GradientTileButton3_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new ImgPoints(panelContenedor));
        }

        private void guna2GradientTileButton4_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new PdfToTable(panelContenedor));
        }

        private void guna2GradientTileButton5_Click(object sender, EventArgs e)
        {
            // Alterna entre claro y oscuro
            ThemeManager.CambiarTema(!ThemeManager.ModoClaroActivo);

            // Cambia el texto del botón según el tema
            var btn = sender as Guna2GradientTileButton;
            if (btn != null)
            {
                btn.Text = ThemeManager.ModoClaroActivo ? "Modo Claro" : "Modo Oscuro";
            }

            // Aplica el nuevo tema a todos los formularios abiertos
            foreach (Form f in Application.OpenForms)
            {
                ThemeManager.AplicarTema(f);
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (desplegado == true)
            {
                panel1.Width = 60;
                label2.Visible = false;
                guna2Separator1.Visible = false;
                label3.Visible = false;
                guna2Separator2.Visible = false;
                desplegado = false;

                foreach (Control ctrl in panel1.Controls)
                {
                    if (ctrl is Guna.UI2.WinForms.Guna2GradientTileButton)
                    {
                        var btn = (Guna2GradientTileButton)ctrl;
                        btn.ImageSize = new Size(33, 33);
                        btn.ImageOffset = new Point(0, 15);
                        btn.TextOffset = new Point(0, 30);
                    }
                }

            }
            else
            {
                panel1.Width = 167;
                desplegado = true;

                label2.Visible = true;
                guna2Separator1.Visible = true;
                label3.Visible = true;
                guna2Separator2.Visible = true;
                
                foreach (Control ctrl in panel1.Controls)
                {
                    if (ctrl is Guna.UI2.WinForms.Guna2GradientTileButton)
                    {
                        var btn = (Guna2GradientTileButton)ctrl;
                        btn.ImageSize = new Size(20, 20);
                        btn.ImageOffset = new Point(0, 12);
                        btn.TextOffset = new Point(0, -12);
                    }
                }
            }
        }
    }
    
}
