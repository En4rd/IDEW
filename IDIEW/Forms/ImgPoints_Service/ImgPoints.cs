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
using IDIEW.Classes;

namespace IDIEW
{
    public partial class ImgPoints : Form
    {
        // === Variables de estado para los modos ===
        private bool modoMover = false;
        private int puntoSeleccionadoIndex = -1;
        private bool estaMoviendoPunto = false;
        private bool modoEliminar = false;
        private bool modoEditarNumero = false;

        // === Elementos de visualización ===
        private PdfiumViewer.PdfDocument pdfDocument;
        private Panel _panelContenedor;
        private Image originalImage;

        // === Lista de puntos con su índice asociado ===
        private List<Tuple<PointF, int>> points = new List<Tuple<PointF, int>>();


        // === Parámetros de visualización ===
        private float zoom = 1.0f;
        private const float ZoomFactor = 1.1f;
        private Point lastMousePos;
        private PointF panOffset = PointF.Empty;
        private bool isPanning = false;


        // === Configuración de elipses y texto ===
        private Color ElipseColor = Color.Blue;
        private Font Elipsefont = new Font("Arial", 3);
        private Color colorfontdialogs = Color.White;


        // === Constructor ===
        public ImgPoints(Panel panelContenedor)
        {
            InitializeComponent();
            ThemeManager.AplicarTema(this);
        }


        // === Renderizado de página PDF como imagen ===
        private Bitmap RenderPdfPageAsImage(PdfiumViewer.PdfDocument doc, int page, int dpi)
        {
            return (Bitmap)doc.Render(page, dpi, dpi, true);
        }


        // === Al cargar el formulario ===
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Pnl_Visualizador.MouseWheel += pictureBox1_MouseWheel;
            Pnl_Visualizador.Focus(); // Asegura que reciba el scroll
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (originalImage != null)
            {
                Graphics g = e.Graphics;

                // Aplicar zoom y desplazamiento
                g.TranslateTransform(panOffset.X, panOffset.Y);
                g.ScaleTransform(zoom, zoom);
                g.DrawImage(originalImage, Point.Empty);

                // Dibujar puntos con texto
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



        // === Evento clic del mouse ===
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
                        Pnl_Visualizador.Invalidate();
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

                    Pnl_Visualizador.Invalidate();
                }
            }

            // 3. Agregar punto
            else if (e.Button == MouseButtons.Left && !modoEliminar && !modoEditarNumero)
            {
                points.Add(Tuple.Create(clickedPoint, points.Count + 1));
                Pnl_Visualizador.Invalidate();
            }
        }



        // === Cargar archivo PDF o imagen ===
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
                        try
                        {
                            pdfDocument = PdfiumViewer.PdfDocument.Load(ofd.FileName);

                            int dpi = 300;
                            int page = 0;
                            var size = pdfDocument.PageSizes[page];
                            int width = (int)(size.Width * dpi / 72);
                            int height = (int)(size.Height * dpi / 72);

                            // Render directo a Bitmap con alta resolución
                            originalImage = pdfDocument.Render(page, width, height, dpi, dpi, PdfiumViewer.PdfRenderFlags.Annotations);
                            Pnl_Visualizador.Image = (Image)originalImage.Clone();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al cargar el PDF: " + ex.Message);
                            return;
                        }
                    }
                    else
                    {
                        pdfDocument = null;
                        originalImage = (Bitmap)Image.FromFile(ofd.FileName);
                        Pnl_Visualizador.Image = (Image)originalImage.Clone();
                    }

                    // Reset de estados
                    zoom = 1.0f;
                    panOffset = PointF.Empty;
                    points.Clear();
                    Pnl_Visualizador.Invalidate();

                    // Habilitar botones
                    Btn_EliminarPunto.Enabled = true;
                    Btn_EditarNumero.Enabled = true;
                    Btn_ElipseFont.Enabled = true;
                    Btn_ElipseColor.Enabled = true;
                    Btn_FontColor.Enabled = true;
                    Btn_Guardar.Enabled = true;
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

                Pnl_Visualizador.Invalidate();
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
                Pnl_Visualizador.Cursor = Cursors.Hand;
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

                Pnl_Visualizador.Invalidate();
            }

            if (isPanning)
            {
                Point delta = new Point(e.X - lastMousePos.X, e.Y - lastMousePos.Y);
                panOffset.X += delta.X;
                panOffset.Y += delta.Y;
                lastMousePos = e.Location;
                Pnl_Visualizador.Invalidate();
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
                Pnl_Visualizador.Cursor = Cursors.Default;
            }
        }

        private void BtnEliminarPunto_Click(object sender, EventArgs e)
        {
            modoEliminar = !modoEliminar;

            Btn_EliminarPunto.FillColor = modoEliminar ? Color.FromArgb(192, 0, 0) : Color.FromArgb(255, 128, 128);
            Btn_EliminarPunto.Text = modoEliminar ? "Salir del modo" : "Eliminar";

            Btn_EditarNumero.Enabled = modoEliminar ? false : true;
            Btn_Mover.Enabled = modoEliminar ? false : true;
            Pnl_Visualizador.Cursor = modoEliminar ? Cursors.Cross : Cursors.Default;
        }

        private void BtnEditarNumero_Click(object sender, EventArgs e)
        {
            modoEditarNumero = !modoEditarNumero;
            Btn_EditarNumero.Text = modoEditarNumero ? "salir del modo" : "Editar";

            Btn_Mover.Enabled = modoEditarNumero ? false : true;
            Btn_EliminarPunto.Enabled = modoEditarNumero ? false : true;
            Pnl_Visualizador.Cursor = modoEditarNumero ? Cursors.IBeam : Cursors.Default;
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            Bitmap finalImage;
            float scaleFactor = 1f;

            int baseDpi = 96; // DPI común en pantallas
            int exportDpi = 500; // Alta resolución deseada

            if (pdfDocument != null)
            {
                int page = 0;

                var size = pdfDocument.PageSizes[page];
                int renderWidth = (int)(size.Width * exportDpi / 72);
                int renderHeight = (int)(size.Height * exportDpi / 72);

                finalImage = (Bitmap)pdfDocument.Render(page, renderWidth, renderHeight, exportDpi, exportDpi, PdfiumViewer.PdfRenderFlags.Annotations);

                scaleFactor = (float)exportDpi / baseDpi;
            }
            else
            {
                finalImage = new Bitmap(originalImage.Width * exportDpi / baseDpi, originalImage.Height * exportDpi / baseDpi);
                scaleFactor = (float)exportDpi / baseDpi;

                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.DrawImage(originalImage, new Rectangle(0, 0, finalImage.Width, finalImage.Height));
                }
            }

            using (Graphics g = Graphics.FromImage(finalImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (Brush ellipseBrush = new SolidBrush(ElipseColor))
                using (Brush textBrush = new SolidBrush(colorfontdialogs))
                {
                    foreach (var item in points)
                    {
                        PointF originalPoint = item.Item1;
                        int index = item.Item2;

                        // Escalar punto
                        PointF scaledPoint = new PointF(originalPoint.X * scaleFactor, originalPoint.Y * scaleFactor);

                        float ellipseRadius = 5f * scaleFactor;
                        g.FillEllipse(ellipseBrush, scaledPoint.X - ellipseRadius, scaledPoint.Y - ellipseRadius, ellipseRadius * 2, ellipseRadius * 2);

                        // Escalar fuente
                        float scaledFontSize = Elipsefont.Size * scaleFactor * 0.2f;
                        using (Font scaledFont = new Font(Elipsefont.FontFamily, scaledFontSize, Elipsefont.Style))
                        {
                            g.DrawString(index.ToString(), scaledFont, textBrush, scaledPoint.X - (4 * scaleFactor), scaledPoint.Y - (2.5f * scaleFactor));
                        }
                    }
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
                    Btn_ElipseColor.FillColor = ElipseColorDialog.Color;
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
                    Btn_FontColor.FillColor = ColorfontDialog.Color;
                    colorfontdialogs = ColorfontDialog.Color;
                }
            }
        }

        private void Btn_Mover_Click(object sender, EventArgs e)
        {
            modoMover = !modoMover;
            Btn_Mover.Text = modoMover ? "Salir del modo" : "Mover";
            Pnl_Visualizador.Cursor = modoMover ? Cursors.SizeAll : Cursors.Default;

            Btn_EliminarPunto.Enabled = modoMover ? false : true;
            Btn_EditarNumero.Enabled = modoMover ? false : true;

            Btn_EliminarPunto.Text = "Eliminar";
            Btn_EditarNumero.Text = "Editar";
            Btn_EliminarPunto.FillColor = Color.FromArgb(255, 128, 128);
        }

    }
}
