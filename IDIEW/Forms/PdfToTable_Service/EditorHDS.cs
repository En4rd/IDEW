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


        public EditorHDS(Classes.JsonConDatos datos, List<string> pictogramasDisponibles)
        {
            InitializeComponent();
            
            datosActual = datos;
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

            List<ComboBox> comboList = new List<ComboBox> { cmbPictograma1, cmbPictograma2, cmbPictograma3, cmbPictograma4 };
            foreach (var cmb in comboList)
            {
                cmb.Items.AddRange(pictogramasDisponibles.ToArray());
                cmb.Visible = false;
            }

            // Mostrar el primero por defecto
            cmbPictograma1.Visible = true;

            if (!string.IsNullOrEmpty(datos.ImagenPictograma))
                cmbPictograma1.SelectedItem = Path.GetFileName(datos.ImagenPictograma);
      
        }

        private void cmbPictogramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", cmbPictograma1.SelectedItem.ToString());
            pictureBox.Image = Image.FromFile(ruta);
            

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            datosActual.Datos.Area = txtArea.Text;
            datosActual.Datos.Cantidad = txtCantidad.Text;
            datosActual.Datos.Uso = txtUso.Text;
            datosActual.Datos.FechaDeActualizacionDeHDS = txtFecha.Text;
            datosActual.Datos.TemperaturaDeInflamacion = txtTDI.Text;
            datosActual.Datos.Fabricante = txtFabricante.Text;
            datosActual.Datos.HDSEnEspanol = txtHDSes.Text;
            datosActual.Datos.NoCAS = txtNoCAS.Text;
            datosActual.Datos.EquipoDeProteccionPersonal = txtEPP.Text;
            datosActual.Datos.Descripcion = txtDescripcion.Text;
            datosActual.Datos.Incompatibilidad = txtIncompatibilidad.Text;
            datosActual.Datos.CondicionesAEvitar = txtEvitar.Text;
            datosActual.Datos.CaracteristicasFisicoQuimicas = txtCaracteristicas.Text;
            datosActual.Datos.LimiteDeExposicion = txtLimites.Text;
            datosActual.Datos.PrimerosAuxilios = txtPrimeros.Text;
            datosActual.Datos.MedidasSanitarias = txtMedidas.Text;
            datosActual.Datos.OrganosAfectados = txtOrganos.Text;
            datosActual.Datos.Sintomas = txtSintomas.Text;

            List<string> pictosSeleccionados = new List<string>();
            List<ComboBox> comboList = new List<ComboBox> { cmbPictograma1, cmbPictograma2, cmbPictograma3, cmbPictograma4 };

            foreach (var cmb in comboList.Where(c => c.Visible && c.SelectedItem != null))
            {
                pictosSeleccionados.Add(cmb.SelectedItem.ToString()); // Solo el nombre, sin ruta completa
            }

            datosActual.PictogramasSeleccionados = pictosSeleccionados;

            // Si aún quieres guardar el string como respaldo, puedes hacerlo así:
            datosActual.ImagenPictograma = string.Join(";", pictosSeleccionados);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NumPictogramas_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)NumPictogramas.Value;

            List<ComboBox> comboList = new List<ComboBox> { cmbPictograma1, cmbPictograma2, cmbPictograma3, cmbPictograma4 };

            for (int i = 0; i < comboList.Count; i++)
            {
                comboList[i].Visible = i < count;
            }
        }

        private void cmbPictograma2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruta2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", cmbPictograma2.SelectedItem.ToString());
            pictureBox1.Image = Image.FromFile(ruta2);
        }

        private void cmbPictograma3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruta3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", cmbPictograma3.SelectedItem.ToString());
            pictureBox2.Image = Image.FromFile(ruta3);
        }

        private void cmbPictograma4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruta4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictogramas", cmbPictograma4.SelectedItem.ToString());
            pictureBox3.Image = Image.FromFile(ruta4);
        }
    }
}
