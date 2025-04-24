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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelEditor = new System.Windows.Forms.Panel();
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
            this.Btn_Enviar.Location = new System.Drawing.Point(260, 269);
            this.Btn_Enviar.Name = "Btn_Enviar";
            this.Btn_Enviar.ShadowDecoration.Parent = this.Btn_Enviar;
            this.Btn_Enviar.Size = new System.Drawing.Size(430, 153);
            this.Btn_Enviar.TabIndex = 21;
            this.Btn_Enviar.Text = "Iniciar";
            this.Btn_Enviar.Click += new System.EventHandler(this.Btn_Enviar_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 550);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(943, 23);
            this.progressBar.TabIndex = 23;
            // 
            // panelEditor
            // 
            this.panelEditor.Location = new System.Drawing.Point(84, 85);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(70, 55);
            this.panelEditor.TabIndex = 24;
            // 
            // PdfToTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(34)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(943, 573);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Btn_Enviar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PdfToTable";
            this.Text = "PdfToTable";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientButton Btn_Enviar;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panelEditor;
    }
}