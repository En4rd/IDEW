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
    }
    
}
