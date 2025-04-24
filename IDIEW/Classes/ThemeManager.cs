using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace IDIEW.Classes
{
    public static class ThemeManager
    {
        public static bool ModoClaroActivo = false;
        // Si es un Panel, marcamos el flag
       
        private static Color claroFondo = Color.Gainsboro;
        private static Color claroTexto = Color.Black;
        private static Color ClaroButton = Color.White;
        private static Color Claropanel = Color.White;

        private static Color oscuroFondo = Color.FromArgb(35, 34, 51);
        private static Color oscuroTexto = Color.White;
        private static Color OscuroButton = Color.FromArgb(46, 44, 77);
        private static Color Oscuropanel = Color.FromArgb(46, 44, 77);

        public static void AplicarTema(Control control)
        {
            if (control == null) return;

            Color fondo = ModoClaroActivo ? oscuroFondo : claroFondo;
            Color texto = ModoClaroActivo ? oscuroTexto : claroTexto;
            Color button = ModoClaroActivo ? OscuroButton : ClaroButton;
            Color panel = ModoClaroActivo ? Oscuropanel : Claropanel;

            
            control.ForeColor = texto;
            if (control is MenuStrip)
            {
                var msr = (MenuStrip)control;
                msr.BackColor = fondo;
            }
            if (control is Panel)
            {
                var pnll = (Panel)control;
                if (pnll.Tag?.ToString() == "f")
                {
                    pnll.BackColor = button;
                }
            }

            if (control is Form)
            {
                var frm = (Form)control;
                frm.BackColor = fondo;
            }

            if (control is Guna2GradientButton)
            {
                var btn = (Guna2GradientButton)control;
                btn.FillColor = button;
                btn.FillColor2 = button;
                btn.ForeColor = texto;

                if (btn.Tag?.ToString() == "Dentro de panel")
                {
                    btn.FillColor = fondo;
                    btn.FillColor2 = fondo;
                }

                if (btn.Tag?.ToString() == "Pictem")
                {
                    btn.Image = ThemeManager.ModoClaroActivo
            ? Properties.Resources.sunny
            : Properties.Resources.moon__1_;
                }

                if (btn.Tag?.ToString() == "picjs")
                {
                    btn.Image = ThemeManager.ModoClaroActivo
            ? Properties.Resources.json__1_
            : Properties.Resources.json;
                }

                if (btn.Tag?.ToString() == "picpoi")
                {
                    btn.Image = ThemeManager.ModoClaroActivo
            ? Properties.Resources.control_point
            : Properties.Resources.control_point__1_;
                }

                if (btn.Tag?.ToString() == "picimg")
                {
                    btn.Image = ThemeManager.ModoClaroActivo
            ? Properties.Resources.excel__1_
            : Properties.Resources.excel;
                }

                if (btn.Tag?.ToString() == "picwod")
                {
                    btn.Image = ThemeManager.ModoClaroActivo
            ? Properties.Resources.file_word__1_
            : Properties.Resources.file_word;
                }
            }

            if (control is Guna2ShadowPanel)
            {
                var pnl = (Guna2ShadowPanel)control;
                pnl.FillColor = button;
            }

            if (control is Guna2ComboBox)
            {
                var cmb = (Guna2ComboBox)control;
                cmb.FillColor = fondo;
            }

            if (control is Guna2TextBox)
            {
                var txt = (Guna2TextBox)control;
                txt.FillColor = fondo;
            }

            if (control is Guna2ControlBox)
            {
                var ctb = (Guna2ControlBox)control;
                ctb.FillColor = fondo;
                ctb.IconColor = texto;
            }

            if (control is DataGridView)
            {
                var dgv = (DataGridView)control;
                dgv.BackgroundColor = panel;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = fondo;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = texto;
                dgv.RowsDefaultCellStyle.BackColor = panel;
                dgv.RowHeadersDefaultCellStyle.BackColor = panel;
            }

            if (control is PictureBox)
            {
                var pict = (PictureBox)control;
                if (pict.Tag?.ToString() == "PicT")
                {
                    pict.Image = ThemeManager.ModoClaroActivo
            ? Properties.Resources.menu__2_
            : Properties.Resources.menu__1_;
                }

            }

            foreach (Control hijo in control.Controls)
            {
                AplicarTema(hijo);
            }

        }

        public static void CambiarTema(bool claro)
        {
            ModoClaroActivo = claro;
        }
    }
}


