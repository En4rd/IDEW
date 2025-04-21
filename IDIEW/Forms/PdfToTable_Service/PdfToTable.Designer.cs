namespace IDIEW
{
    partial class PdfToTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Enviar = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.Ruta_txt = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnExaminarExcel = new Guna.UI2.WinForms.Guna2GradientButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.guna2ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Enviar
            // 
            this.Btn_Enviar.BorderRadius = 5;
            this.Btn_Enviar.CheckedState.Parent = this.Btn_Enviar;
            this.Btn_Enviar.CustomImages.Parent = this.Btn_Enviar;
            this.Btn_Enviar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(44)))), ((int)(((byte)(77)))));
            this.Btn_Enviar.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(44)))), ((int)(((byte)(77)))));
            this.Btn_Enviar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_Enviar.ForeColor = System.Drawing.Color.White;
            this.Btn_Enviar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_Enviar.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_Enviar.HoverState.Parent = this.Btn_Enviar;
            this.Btn_Enviar.Location = new System.Drawing.Point(337, 460);
            this.Btn_Enviar.Name = "Btn_Enviar";
            this.Btn_Enviar.ShadowDecoration.Parent = this.Btn_Enviar;
            this.Btn_Enviar.Size = new System.Drawing.Size(131, 54);
            this.Btn_Enviar.TabIndex = 21;
            this.Btn_Enviar.Text = "Exportar";
            this.Btn_Enviar.Click += new System.EventHandler(this.Btn_Enviar_Click);
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.CausesValidation = false;
            this.guna2ShadowPanel1.Controls.Add(this.btnExaminarExcel);
            this.guna2ShadowPanel1.Controls.Add(this.Ruta_txt);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(44)))), ((int)(((byte)(77)))));
            this.guna2ShadowPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(269, 194);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 5;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(291, 68);
            this.guna2ShadowPanel1.TabIndex = 22;
            // 
            // Ruta_txt
            // 
            this.Ruta_txt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Ruta_txt.Cursor = System.Windows.Forms.Cursors.Default;
            this.Ruta_txt.DefaultText = "";
            this.Ruta_txt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Ruta_txt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Ruta_txt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Ruta_txt.DisabledState.Parent = this.Ruta_txt;
            this.Ruta_txt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Ruta_txt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(34)))), ((int)(((byte)(51)))));
            this.Ruta_txt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Ruta_txt.FocusedState.Parent = this.Ruta_txt;
            this.Ruta_txt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ruta_txt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Ruta_txt.HoverState.Parent = this.Ruta_txt;
            this.Ruta_txt.Location = new System.Drawing.Point(24, 19);
            this.Ruta_txt.Margin = new System.Windows.Forms.Padding(6);
            this.Ruta_txt.Name = "Ruta_txt";
            this.Ruta_txt.PasswordChar = '\0';
            this.Ruta_txt.PlaceholderText = "";
            this.Ruta_txt.SelectedText = "";
            this.Ruta_txt.ShadowDecoration.Parent = this.Ruta_txt;
            this.Ruta_txt.Size = new System.Drawing.Size(166, 33);
            this.Ruta_txt.TabIndex = 23;
            // 
            // btnExaminarExcel
            // 
            this.btnExaminarExcel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnExaminarExcel.BorderRadius = 5;
            this.btnExaminarExcel.BorderThickness = 1;
            this.btnExaminarExcel.CheckedState.Parent = this.btnExaminarExcel;
            this.btnExaminarExcel.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnExaminarExcel.CustomImages.Parent = this.btnExaminarExcel;
            this.btnExaminarExcel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(34)))), ((int)(((byte)(51)))));
            this.btnExaminarExcel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(34)))), ((int)(((byte)(51)))));
            this.btnExaminarExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExaminarExcel.ForeColor = System.Drawing.Color.White;
            this.btnExaminarExcel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnExaminarExcel.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExaminarExcel.HoverState.Parent = this.btnExaminarExcel;
            this.btnExaminarExcel.Location = new System.Drawing.Point(210, 19);
            this.btnExaminarExcel.Name = "btnExaminarExcel";
            this.btnExaminarExcel.ShadowDecoration.Parent = this.btnExaminarExcel;
            this.btnExaminarExcel.Size = new System.Drawing.Size(46, 33);
            this.btnExaminarExcel.TabIndex = 24;
            this.btnExaminarExcel.Text = "...";
            this.btnExaminarExcel.Click += new System.EventHandler(this.btnExaminarExcel_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 550);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(943, 23);
            this.progressBar.TabIndex = 23;
            // 
            // PdfToTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(34)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(943, 573);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Controls.Add(this.Btn_Enviar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PdfToTable";
            this.Text = "PdfToTable";
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientButton Btn_Enviar;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2TextBox Ruta_txt;
        private Guna.UI2.WinForms.Guna2GradientButton btnExaminarExcel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}