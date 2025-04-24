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

        private void AbrirFormularioEnPanel(Form Form1)
        {
            Form1.TopLevel = false;
            Form1.Dock = DockStyle.Fill;   // Hace que ocupe todo el panel
            panelContenedor.Controls.Clear();
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
    }
    
}
