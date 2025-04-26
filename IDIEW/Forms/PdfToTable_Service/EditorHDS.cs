using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDIEW.Forms.PdfToTable_Service
{
    public partial class EditorHDS : Form
    {
        private Classes.JsonConDatos datosActual;
        private Button btnCancelar;
        private Guna2GradientButton botonSeleccionado;
        private List<Classes.JsonConDatos> datosList;
        private int indiceActual;
        private List<string> pictogramasDisponibles;
      



        public EditorHDS(List<Classes.JsonConDatos> datosList, List<string> pictogramasDisponibles)
        {
            InitializeComponent();
            this.datosList = datosList;
            this.pictogramasDisponibles = pictogramasDisponibles;
            indiceActual = 0;


            CrearBotonesNavegacion();
            MostrarDatos();

        }

        private void cmbPictogramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPictograma1.SelectedItem == null) return;
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", cmbPictograma1.SelectedItem.ToString());
            pictureBox.Image = Image.FromFile(ruta);


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var datos = datosList[indiceActual];

            datos.Datos.Area = txtArea.Text;
            datos.Datos.Cantidad = txtCantidad.Text;
            datos.Datos.Uso = txtUso.Text;
            datos.Datos.FechaDeActualizacionDeHDS = txtFecha.Text;
            datos.Datos.TemperaturaDeInflamacion = txtTDI.Text;
            datos.Datos.Fabricante = txtFabricante.Text;
            datos.Datos.HDSEnEspanol = txtHDSes.Text;
            datos.Datos.NoCAS = txtNoCAS.Text;
            datos.Datos.EquipoDeProteccionPersonal = txtEPP.Text;
            datos.Datos.Descripcion = txtDescripcion.Text;
            datos.Datos.Incompatibilidad = txtIncompatibilidad.Text;
            datos.Datos.CondicionesAEvitar = txtEvitar.Text;
            datos.Datos.CaracteristicasFisicoQuimicas = txtCaracteristicas.Text;
            datos.Datos.LimiteDeExposicion = txtLimites.Text;
            datos.Datos.PrimerosAuxilios = txtPrimeros.Text;
            datos.Datos.MedidasSanitarias = txtMedidas.Text;
            datos.Datos.OrganosAfectados = txtOrganos.Text;
            datos.Datos.Sintomas = txtSintomas.Text;

            List<string> pictosSeleccionados = new List<string>();
            List<ComboBox> comboList = new List<ComboBox> { cmbPictograma1, cmbPictograma2, cmbPictograma3, cmbPictograma4 };
            foreach (var cmb in comboList.Where(c => c.Visible && c.SelectedItem != null))
            {
                pictosSeleccionados.Add(cmb.SelectedItem.ToString());
            }
            datos.PictogramasSeleccionados = pictosSeleccionados;
            datos.ImagenPictograma = string.Join(";", pictosSeleccionados);

            MessageBox.Show("Cambios guardados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
    }

        private void NumPictogramas_ValueChanged(object sender, EventArgs e)
        {
        int count = (int)NumPictogramas.Value;
        List<ComboBox> comboList = new List<ComboBox> { cmbPictograma1, cmbPictograma2, cmbPictograma3, cmbPictograma4 };
        List<Label> lblList = new List<Label> { Lbl1, Lbl2, Lbl3, Lbl4 };
        List<PictureBox> pcblist = new List<PictureBox> { pictureBox, pictureBox1, pictureBox2, pictureBox3 };

        for (int i = 0; i < comboList.Count; i++)
        {
            comboList[i].Visible = i < count;
            lblList[i].Visible = i < count;
            pcblist[i].Visible = i < count;
        }
        }

        private void cmbPictograma2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPictograma2.SelectedItem == null) return;
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", cmbPictograma2.SelectedItem.ToString());
            pictureBox1.Image = Image.FromFile(ruta);
        }

        private void cmbPictograma3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPictograma3.SelectedItem == null) return;
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", cmbPictograma3.SelectedItem.ToString());
            pictureBox2.Image = Image.FromFile(ruta);
        }

        private void cmbPictograma4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPictograma4.SelectedItem == null) return;
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", cmbPictograma4.SelectedItem.ToString());
            pictureBox3.Image = Image.FromFile(ruta);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show("¿Seguro que deseas cancelar la exportación?", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                Classes.HDSExcelExporter.CancelarExportacion();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }


        private void CrearBotonesNavegacion()
        {
            panelBotones.Controls.Clear();

            for (int i = datosList.Count - 1; i >= 0; i--) // Invertido: último primero
            {
                int index = i;
                string nombre = datosList[i].Datos.NombreDelProducto ?? $"Químico {i + 1}";

                Guna2GradientButton btn = new Guna2GradientButton
                {
                    Text = nombre.Length > 25 ? nombre.Substring(0, 25) + "..." : nombre,
                    Dock = DockStyle.Top,
                    Tag = index,
                    Height = 40,
                    FillColor = Color.White,
                    FillColor2 = Color.White,
                    ForeColor = Color.Black,
                    HoverState = {
                FillColor = Color.LightGray,
                FillColor2 = Color.LightGray
            },
                    Cursor = Cursors.Hand
                };

                btn.Click += (s, e) =>
                {
                    indiceActual = index;
                    MostrarDatos();
                    ResaltarBotonSeleccionado(btn);
                };

                panelBotones.Controls.Add(btn);

                // Resaltar automáticamente el botón del elemento actual
                if (index == indiceActual)
                    ResaltarBotonSeleccionado(btn);
            }
        }


        private void MostrarDatos()
        {
            var datos = datosList[indiceActual];
            lblProgreso.Text = $"{indiceActual + 1}/{datosList.Count}";

            txtNombreProducto.Text = datos.Datos.NombreDelProducto;
            txtArea.Text = datos.Datos.Area;
            txtCantidad.Text = datos.Datos.Cantidad;
            txtUso.Text = datos.Datos.Uso;
            txtFecha.Text = datos.Datos.FechaDeActualizacionDeHDS;
            txtTDI.Text = datos.Datos.TemperaturaDeInflamacion;
            txtFabricante.Text = datos.Datos.Fabricante;
            txtHDSes.Text = datos.Datos.HDSEnEspanol;
            txtNoCAS.Text = datos.Datos.NoCAS;
            txtEPP.Text = datos.Datos.EquipoDeProteccionPersonal;
            txtDescripcion.Text = datos.Datos.Descripcion;
            txtIncompatibilidad.Text = datos.Datos.Incompatibilidad;
            txtEvitar.Text = datos.Datos.CondicionesAEvitar;
            txtCaracteristicas.Text = datos.Datos.CaracteristicasFisicoQuimicas;
            txtLimites.Text = datos.Datos.LimiteDeExposicion;
            txtPrimeros.Text = datos.Datos.PrimerosAuxilios;
            txtMedidas.Text = datos.Datos.MedidasSanitarias;
            txtOrganos.Text = datos.Datos.OrganosAfectados;
            txtSintomas.Text = datos.Datos.Sintomas;

            var comboList = new List<ComboBox> { cmbPictograma1, cmbPictograma2, cmbPictograma3, cmbPictograma4 };
            var pictureBoxes = new List<PictureBox> { pictureBox, pictureBox1, pictureBox2, pictureBox3 };
            var lblList = new List<Label> { Lbl1, Lbl2, Lbl3, Lbl4 };

            // Asegurarse de que los ComboBox tengan cargada la lista de pictogramas
            foreach (var cmb in comboList)
            {
                if (cmb.Items.Count == 0 && pictogramasDisponibles != null)
                {
                    cmb.Items.AddRange(pictogramasDisponibles.ToArray());
                }
            }

            if (datos.PictogramasSeleccionados != null && datos.PictogramasSeleccionados.Any())
            {
                NumPictogramas.Value = datos.PictogramasSeleccionados.Count;

                for (int i = 0; i < comboList.Count; i++)
                {
                    if (i < datos.PictogramasSeleccionados.Count)
                    {
                        var pictograma = datos.PictogramasSeleccionados[i];
                        comboList[i].Visible = true;
                        comboList[i].SelectedItem = pictograma;

                        string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", pictograma);
                        if (File.Exists(ruta))
                        {
                            pictureBoxes[i].Visible = true;
                            pictureBoxes[i].Image = Image.FromFile(ruta);
                        }
                        lblList[i].Visible = true;
                    }
                    else
                    {
                        comboList[i].Visible = false;
                        comboList[i].SelectedItem = null;
                        pictureBoxes[i].Visible = false;
                        pictureBoxes[i].Image = null;
                        lblList[i].Visible = false;
                    }
                }
            }
            else
            {
                // Si no hay pictogramas seleccionados
                NumPictogramas.Value = 0;

                for (int i = 0; i < comboList.Count; i++)
                {
                    comboList[i].Visible = false;
                    comboList[i].SelectedItem = null;

                    pictureBoxes[i].Visible = false;
                    pictureBoxes[i].Image = null;

                    lblList[i].Visible = false;
                }
            }
        }
        private void Btn_siguiente_Click(object sender, EventArgs e)
        {
            if (indiceActual < datosList.Count - 1)
            {
                btnGuardar_Click(sender, e);
                indiceActual++;
                MostrarDatos();
            }
            else
            {
                btnGuardar_Click(sender, e);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


        private void ResaltarBotonSeleccionado(Guna2GradientButton nuevoSeleccionado)
        {
            // Restaurar estilo del botón anterior
            if (botonSeleccionado != null)
            {
                botonSeleccionado.FillColor = Color.White;
                botonSeleccionado.FillColor2 = Color.White;
                botonSeleccionado.ForeColor = Color.Black;
            }

            // Aplicar estilo al nuevo botón seleccionado
            nuevoSeleccionado.FillColor = Color.FromArgb(255, 192, 128);
            nuevoSeleccionado.FillColor2 = Color.FromArgb(255, 128, 128);
            nuevoSeleccionado.ForeColor = Color.White;

            botonSeleccionado = nuevoSeleccionado;
        }
    }
}
