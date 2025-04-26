using IDIEW.Classes;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
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
        private object openFileDialog;

        public PdfToTable(Panel panelContenedor)
        {
            InitializeComponent();
            ThemeManager.AplicarTema(this);
        }

        private void Btn_Enviar_Click(object sender, EventArgs e)
        {
           Btn_Enviar.Enabled = false;
            Classes.HDSExcelExporter.ExportarDesdeCarpeta(progressBar, this , panelEditor);
            Btn_Enviar.Enabled = true;
            
        }

        private void btnExaminarExcel_Click(object sender, EventArgs e)
        {
            
        }

    }
}
