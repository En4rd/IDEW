using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using PdfiumViewer;
using System.IO;



namespace IDIEW
{
    public partial class ImgPoints : Form
    {
        //variables boton mover
        private bool modoMover = false;
        private int puntoSeleccionadoIndex = -1;
        private bool estaMoviendoPunto = false;

        private Panel _panelContenedor;
        private Image originalImage;
        private List<Tuple<PointF, int>> points = new List<Tuple<PointF, int>>();
        private float zoom = 1.0f;
        private const float ZoomFactor = 1.1f;
        private Point lastMousePos;
        private PointF panOffset = PointF.Empty;
        private bool isPanning = false;
        private bool modoEliminar = false;
        private bool modoEditarNumero = false;
        private Color ElipseColor = Color.Blue;
        private Font Elipsefont = new Font("Arial", 3);
        private Color colorfontdialogs = Color.White;
        private PdfiumViewer.PdfDocument pdfDocument;


        public ImgPoints(Panel panelContenedor)
        {
            InitializeComponent();
        }

        private Bitmap RenderPdfPageAsImage(PdfiumViewer.PdfDocument doc, int page, int dpi)
        {
            return (Bitmap)doc.Render(page, dpi, dpi, true);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            pictureBox1.Focus(); // Asegura que reciba el scroll
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (originalImage != null)
            {
                Graphics g = e.Graphics;

                // Aplicar zoom y desplazamiento (pan)
                g.TranslateTransform(panOffset.X, panOffset.Y);
                g.ScaleTransform(zoom, zoom);
                g.DrawImage(originalImage, Point.Empty);


                SolidBrush brush = new SolidBrush(colorfontdialogs);
                using (Brush redBrush = new SolidBrush(ElipseColor))
                    foreach (var item in points)
                    {
                        PointF point = item.Item1;
                        int index = item.Item2;

                        g.FillEllipse(redBrush, point.X - 5, point.Y - 5, 10,10);
                        g.DrawString(index.ToString(), Elipsefont, brush, point.X-4, point.Y - 3);
                    }
                
            }

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;

            if (modoMover) return;

            // Ajustamos coordenadas con pan y zoom
            float adjustedX = (e.X - panOffset.X) / zoom;
            float adjustedY = (e.Y - panOffset.Y) / zoom;
            PointF clickedPoint = new PointF(adjustedX, adjustedY);

            // 1. Editar número
            if (e.Button == MouseButtons.Left && modoEditarNumero)
            {
                int indexToEdit = -1;
                float radio = 10 / zoom;

                for (int i = 0; i < points.Count; i++)
                {
                    PointF pt = points[i].Item1;
                    float dx = pt.X - clickedPoint.X;
                    float dy = pt.Y - clickedPoint.Y;
                    if (Math.Sqrt(dx * dx + dy * dy) <= radio)
                    {
                        indexToEdit = i;
                        break;
                    }
                }

                if (indexToEdit >= 0)
                {
                    string input = Interaction.InputBox("Nuevo número para este punto:", "Editar número", points[indexToEdit].Item2.ToString());

                    int nuevoNumero;
                    if (int.TryParse(input, out nuevoNumero))
                    {
                        var puntoEditado = Tuple.Create(points[indexToEdit].Item1, nuevoNumero);
                        points[indexToEdit] = puntoEditado;
                        pictureBox1.Invalidate();
                    }
                }
            }

            // 2. Eliminar
            else if (e.Button == MouseButtons.Left && modoEliminar)
            {
                int indexToRemove = -1;
                float radio = 10 / zoom;

                for (int i = 0; i < points.Count; i++)
                {
                    PointF pt = points[i].Item1;
                    float dx = pt.X - clickedPoint.X;
                    float dy = pt.Y - clickedPoint.Y;
                    if (Math.Sqrt(dx * dx + dy * dy) <= radio)
                    {
                        indexToRemove = i;
                        break;
                    }
                }

                if (indexToRemove >= 0)
                {
                    points.RemoveAt(indexToRemove);

                    // Renumerar
                    for (int i = 0; i < points.Count; i++)
                    {
                        var newTuple = Tuple.Create(points[i].Item1, i + 1);
                        points[i] = newTuple;
                    }

                    pictureBox1.Invalidate();
                }
            }

            // 3. Agregar punto
            else if (e.Button == MouseButtons.Left && !modoEliminar && !modoEditarNumero)
            {
                points.Add(Tuple.Create(clickedPoint, points.Count + 1));
                pictureBox1.Invalidate();
            }
        }

        private void Btn_subir_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos PDF|*.pdf|Imágenes|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(ofd.FileName).ToLower();

                    if (ext == ".pdf")
                    {
                        pdfDocument = PdfiumViewer.PdfDocument.Load(ofd.FileName);
                        originalImage = RenderPdfPageAsImage(pdfDocument, 0, 1000); // Página 0, DPI 300
                    }
                    else
                    {
                        pdfDocument = null;
                        originalImage = Image.FromFile(ofd.FileName);
                    }

                    zoom = 1.0f;
                    points.Clear();
                    pictureBox1.Invalidate();
                    BtnEliminarPunto.Enabled = true;
                    BtnEditarNumero.Enabled = true;
                    guna2CircleButton3.Enabled = true;
                    guna2CircleButton1.Enabled = true;
                    guna2CircleButton4.Enabled = true;
                    guna2CircleButton2.Enabled = true;
                    Btn_Mover.Enabled = true;
                }
            }

        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Delta > 0)
                    zoom *= ZoomFactor;
                else
                    zoom /= ZoomFactor;

                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && modoMover)
            {
                float adjustedX = (e.X - panOffset.X) / zoom;
                float adjustedY = (e.Y - panOffset.Y) / zoom;
                PointF clickedPoint = new PointF(adjustedX, adjustedY);

                float radio = 10 / zoom;

                for (int i = 0; i < points.Count; i++)
                {
                    PointF pt = points[i].Item1;
                    float dx = pt.X - clickedPoint.X;
                    float dy = pt.Y - clickedPoint.Y;

                    if (Math.Sqrt(dx * dx + dy * dy) <= radio)
                    {
                        puntoSeleccionadoIndex = i;
                        estaMoviendoPunto = true;
                        break;
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                isPanning = true;
                lastMousePos = e.Location;
                pictureBox1.Cursor = Cursors.Hand;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (modoMover && estaMoviendoPunto && puntoSeleccionadoIndex >= 0)
            {
                float adjustedX = (e.X - panOffset.X) / zoom;
                float adjustedY = (e.Y - panOffset.Y) / zoom;

                var puntoActual = points[puntoSeleccionadoIndex];
                points[puntoSeleccionadoIndex] = Tuple.Create(new PointF(adjustedX, adjustedY), puntoActual.Item2);

                pictureBox1.Invalidate();
            }

            if (isPanning)
            {
                Point delta = new Point(e.X - lastMousePos.X, e.Y - lastMousePos.Y);
                panOffset.X += delta.X;
                panOffset.Y += delta.Y;
                lastMousePos = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && modoMover)
            {
                estaMoviendoPunto = false;
                puntoSeleccionadoIndex = -1;
            }

            if (e.Button == MouseButtons.Right)
            {
                isPanning = false;
                pictureBox1.Cursor = Cursors.Default;
            }
        }

        private void BtnEliminarPunto_Click(object sender, EventArgs e)
        {
            modoEliminar = !modoEliminar;

            BtnEliminarPunto.FillColor = modoEliminar ? Color.FromArgb(192, 0, 0) : Color.FromArgb(255, 128, 128);
            BtnEliminarPunto.Text = modoEliminar ? "Salir del modo" : "Eliminar";
            pictureBox1.Cursor = modoEliminar ? Cursors.Cross : Cursors.Default;
        }

        private void BtnEditarNumero_Click(object sender, EventArgs e)
        {
            modoEditarNumero = !modoEditarNumero;
            BtnEditarNumero.Text = modoEditarNumero ? "salir del modo" : "Editar";
            pictureBox1.Cursor = modoEditarNumero ? Cursors.IBeam : Cursors.Default;
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            Bitmap finalImage;

            if (pdfDocument != null)
            {
                finalImage = RenderPdfPageAsImage(pdfDocument, 0, 300); // Alta resolución
            }
            else
            {
                finalImage = new Bitmap(originalImage.Width, originalImage.Height);
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.DrawImage(originalImage, Point.Empty);
                }
            }

            using (Graphics g = Graphics.FromImage(finalImage))
            {
                SolidBrush brush = new SolidBrush(colorfontdialogs);
                using (Brush redBrush = new SolidBrush(ElipseColor))
                    foreach (var item in points)
                    {
                        PointF point = item.Item1;
                        int index = item.Item2;

                        g.FillEllipse(redBrush, point.X - 5, point.Y - 5, 10, 10);
                        g.DrawString(index.ToString(), Elipsefont, brush, point.X - 4, point.Y - 3);
                    }


            }
           

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    finalImage.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            using (ColorDialog ElipseColorDialog = new ColorDialog())
            {
                if (ElipseColorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Por ejemplo, cambiar el color del botón
                    guna2CircleButton1.FillColor = ElipseColorDialog.Color;
                    ElipseColor = ElipseColorDialog.Color;
                }
            }
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            using (FontDialog FontDialog = new FontDialog())
            {
                if (FontDialog.ShowDialog() == DialogResult.OK)
                {
                    Elipsefont = FontDialog.Font;
                }
            }
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            using (ColorDialog ColorfontDialog = new ColorDialog())
            {
                if (ColorfontDialog.ShowDialog() == DialogResult.OK)
                {
                    guna2CircleButton4.FillColor = ColorfontDialog.Color;
                    colorfontdialogs = ColorfontDialog.Color;
                }
            }
        }

        private void Btn_Mover_Click(object sender, EventArgs e)
        {
            modoMover = !modoMover;
            Btn_Mover.Text = modoMover ? "Salir del modo" : "Mover";
            pictureBox1.Cursor = modoMover ? Cursors.SizeAll : Cursors.Default;

            // Desactivar otros modos para evitar conflictos
            modoEliminar = false;
            modoEditarNumero = false;

            BtnEliminarPunto.Text = "Eliminar";
            BtnEditarNumero.Text = "Editar";
            BtnEliminarPunto.FillColor = Color.FromArgb(255, 128, 128);
        }

    }
}
