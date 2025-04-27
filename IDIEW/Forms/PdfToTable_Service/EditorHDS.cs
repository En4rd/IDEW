using Guna.UI2.WinForms;
using IDIEW.Classes;
using Newtonsoft.Json;
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
        private string ArchivoRuta = HDSExcelExporter.CarpetaSeleccionada;
        private Classes.JsonConDatos datosActualClonado;
        private Classes.JsonConDatos datosEstadoOriginal;





        public EditorHDS(List<Classes.JsonConDatos> datosList, List<string> pictogramasDisponibles)
        {
            InitializeComponent();
            this.datosList = datosList;
            this.pictogramasDisponibles = pictogramasDisponibles;
            indiceActual = 0;
            guna2ShadowForm1.SetShadowForm(this);  // Aplica la sombra al formulario

            // Configurar parámetros de la sombra

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
            if (datosList == null || datosList.Count == 0) return;

            var datos = datosList[indiceActual];

            // Actualizar datos en memoria
            datos.Datos.NombreDelProducto = txtNombreProducto.Text;
            datos.Datos.Area = txtArea.Text;
            datos.Datos.Cantidad = txtCantidad.Text;
            datos.Datos.Uso = txtUso.Text;
            datos.Datos.FechaDeActualizacionDeHDS = txtFecha.Text;
            datos.Datos.TemperaturaDeInflamacion = txtTDI.Text;
            datos.Datos.Fabricante = txtFabricante.Text;
            datos.Datos.HDSEnEspanol = txtHDEspanol.Text;
            datos.Datos.NoCAS = txtNoCAS.Text;
            datos.Datos.EquipoDeProteccionPersonal = txtEPP.Text;
            datos.Datos.Descripcion = txtDescripcion.Text;
            datos.Datos.Incompatibilidad = txtIncompatibilidad.Text;
            datos.Datos.CondicionesAEvitar = txtCondicionesAEvitar.Text;
            datos.Datos.CaracteristicasFisicoQuimicas = txtCaracteristicasFisicoQuimicas.Text;
            datos.Datos.LimiteDeExposicion = txtLimiteExposicion.Text;
            datos.Datos.PrimerosAuxilios = txtPrimerosAuxilios.Text;
            datos.Datos.MedidasSanitarias = txtMedidasSanitarias.Text;
            datos.Datos.OrganosAfectados = txtOrganosAfectados.Text;
            datos.Datos.Sintomas = txtSintomas.Text;

            // Actualizar pictogramas
            if (datos.PictogramasSeleccionados == null)
                datos.PictogramasSeleccionados = new List<string>();
            else
                datos.PictogramasSeleccionados.Clear();

            var comboList = new List<Guna2ComboBox> { cmbPictograma1, cmbPictograma2, cmbPictograma3, cmbPictograma4 };
            foreach (var combo in comboList)
            {
                if (combo.Visible && combo.SelectedItem != null)
                {
                    datos.PictogramasSeleccionados.Add(combo.SelectedItem.ToString());
                }
            }

            try
            {
                string jsonActualizado = JsonConvert.SerializeObject(datos, Formatting.Indented);

                if (!string.IsNullOrEmpty(datos.Archivo))
                {
                    File.WriteAllText(datos.Archivo, jsonActualizado);
                }
                datosList[indiceActual] = datos;
                //  Aquí importante: después de guardar, actualizamos el "original"
                ClonarEstadoActualComoOriginal();
                

                MessageBox.Show("Datos guardados correctamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    ImageAlign= System.Windows.Forms.HorizontalAlignment.Right,
                    TextAlign = System.Windows.Forms.HorizontalAlignment.Left,
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
                    //  Antes de cambiar, preguntamos si hay cambios
                    if (HayCambiosSinGuardar())
                    {
                        var confirmar = MessageBox.Show(
                            "Hay cambios sin guardar. ¿Seguro que deseas cambiar de químico?",
                            "Cambios sin guardar",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                        if (confirmar != DialogResult.Yes)
                            return; // No cambiar si no confirma
                    }

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

            txtNombreProducto.Text = datosList[indiceActual].Datos.NombreDelProducto;
            txtUso.Text = datosList[indiceActual].Datos.Uso;
            txtFecha.Text = datosList[indiceActual].Datos.FechaDeActualizacionDeHDS;
            txtTDI.Text = datosList[indiceActual].Datos.TemperaturaDeInflamacion;
            txtDescripcion.Text = datosList[indiceActual].Datos.Descripcion;
            txtEPP.Text = datosList[indiceActual].Datos.EquipoDeProteccionPersonal;
            txtIncompatibilidad.Text = datosList[indiceActual].Datos.Incompatibilidad;
            txtCondicionesAEvitar.Text = datosList[indiceActual].Datos.CondicionesAEvitar;
            txtNoCAS.Text = datosList[indiceActual].Datos.NoCAS;
            txtLimiteExposicion.Text = datosList[indiceActual].Datos.LimiteDeExposicion;
            txtCaracteristicasFisicoQuimicas.Text = datosList[indiceActual].Datos.CaracteristicasFisicoQuimicas;
            txtMedidasSanitarias.Text = datosList[indiceActual].Datos.MedidasSanitarias;
            txtSintomas.Text = datosList[indiceActual].Datos.Sintomas;
            txtPrimerosAuxilios.Text = datosList[indiceActual].Datos.PrimerosAuxilios;
            txtOrganosAfectados.Text = datosList[indiceActual].Datos.OrganosAfectados;
            txtArea.Text = datosList[indiceActual].Datos.Area;
            txtCantidad.Text = datosList[indiceActual].Datos.Cantidad;
            txtFabricante.Text = datosList[indiceActual].Datos.Fabricante;
            txtHDEspanol.Text = datosList[indiceActual].Datos.HDSEnEspanol;

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

            datosActualClonado = ClonarDatos(datosList[indiceActual]);
        }
        private void Btn_siguiente_Click(object sender, EventArgs e)
        {
            btnGuardar_Click(sender, e); // Primero guardar los cambios actuales
            this.DialogResult = DialogResult.OK; // Decirle al ExportarDesdeCarpeta que sí queremos exportar
            this.Close(); // Cerrar el formulario
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

        private Classes.JsonConDatos ClonarDatos(Classes.JsonConDatos original)
        {
            string json = JsonConvert.SerializeObject(original);
            return JsonConvert.DeserializeObject<Classes.JsonConDatos>(json);
        }

        private bool HayCambiosSinGuardar()
        {
            if (datosActualClonado == null)
                return false;

            var datosActual = datosList[indiceActual];

            // Comparar campos de texto
            if (txtNombreProducto.Text != datosActualClonado.Datos.NombreDelProducto) return true;
            if (txtArea.Text != datosActualClonado.Datos.Area) return true;
            if (txtCantidad.Text != datosActualClonado.Datos.Cantidad) return true;
            if (txtUso.Text != datosActualClonado.Datos.Uso) return true;
            if (txtFecha.Text != datosActualClonado.Datos.FechaDeActualizacionDeHDS) return true;
            if (txtTDI.Text != datosActualClonado.Datos.TemperaturaDeInflamacion) return true;
            if (txtFabricante.Text != datosActualClonado.Datos.Fabricante) return true;
            if (txtHDEspanol.Text != datosActualClonado.Datos.HDSEnEspanol) return true;
            if (txtNoCAS.Text != datosActualClonado.Datos.NoCAS) return true;
            if (txtEPP.Text != datosActualClonado.Datos.EquipoDeProteccionPersonal) return true;
            if (txtDescripcion.Text != datosActualClonado.Datos.Descripcion) return true;
            if (txtIncompatibilidad.Text != datosActualClonado.Datos.Incompatibilidad) return true;
            if (txtCondicionesAEvitar.Text != datosActualClonado.Datos.CondicionesAEvitar) return true;
            if (txtCaracteristicasFisicoQuimicas.Text != datosActualClonado.Datos.CaracteristicasFisicoQuimicas) return true;
            if (txtLimiteExposicion.Text != datosActualClonado.Datos.LimiteDeExposicion) return true;
            if (txtPrimerosAuxilios.Text != datosActualClonado.Datos.PrimerosAuxilios) return true;
            if (txtMedidasSanitarias.Text != datosActualClonado.Datos.MedidasSanitarias) return true;
            if (txtOrganosAfectados.Text != datosActualClonado.Datos.OrganosAfectados) return true;
            if (txtSintomas.Text != datosActualClonado.Datos.Sintomas) return true;

            // Comparar pictogramas
            var pictogramasActuales = new List<string>();
            var comboList = new List<ComboBox> { cmbPictograma1, cmbPictograma2, cmbPictograma3, cmbPictograma4 };

            foreach (var combo in comboList)
            {
                if (combo.Visible && combo.SelectedItem != null)
                {
                    pictogramasActuales.Add(combo.SelectedItem.ToString());
                }
            }

            var pictogramasOriginales = datosActualClonado.PictogramasSeleccionados ?? new List<string>();

            if (!pictogramasActuales.SequenceEqual(pictogramasOriginales))
                return true;

            return false;
        }

        private void ClonarEstadoActualComoOriginal()
        {
            if (datosList != null && datosList.Count > indiceActual)
            {
                datosActualClonado = ClonarDatos(datosList[indiceActual]);
                datosEstadoOriginal = JsonConvert.DeserializeObject<Classes.JsonConDatos>(
                    JsonConvert.SerializeObject(datosList[indiceActual])
                    
                );
            }
        }

        private void ActualizarIconoBotonGuardar(bool cambiosGuardados)
        {
            if (cambiosGuardados)
            {
                // Cambiar al ícono de "Guardado"
                btnGuardar.Image = Properties.Resources.file_success; // Este es el ícono de "Guardado"
            }
            else
            {
                // Cambiar al ícono de "Pendiente" o "Editar"
                btnGuardar.Image = Properties.Resources.file_tips_one; // Este es el ícono de "Editar"
            }
        }

    }
}
