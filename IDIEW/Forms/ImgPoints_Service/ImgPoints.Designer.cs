namespace IDIEW
{
    partial class ImgPoints
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImgPoints));
            this.Pnl_Visualizador = new System.Windows.Forms.PictureBox();
            this.Btn_subir = new Guna.UI2.WinForms.Guna2CircleButton();
            this.Btn_Guardar = new Guna.UI2.WinForms.Guna2CircleButton();
            this.Btn_EliminarPunto = new Guna.UI2.WinForms.Guna2CircleButton();
            this.Btn_EditarNumero = new Guna.UI2.WinForms.Guna2CircleButton();
            this.Btn_ElipseColor = new Guna.UI2.WinForms.Guna2CircleButton();
            this.ElipseColorDialog = new System.Windows.Forms.ColorDialog();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.Btn_Mover = new Guna.UI2.WinForms.Guna2CircleButton();
            this.Btn_FontColor = new Guna.UI2.WinForms.Guna2CircleButton();
            this.Btn_ElipseFont = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarProgresoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarProgresoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_Visualizador)).BeginInit();
            this.guna2ShadowPanel1.SuspendLayout();
            this.guna2ShadowPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Visualizador
            // 
            this.Pnl_Visualizador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Visualizador.ErrorImage = null;
            this.Pnl_Visualizador.Location = new System.Drawing.Point(75, 47);
            this.Pnl_Visualizador.Name = "Pnl_Visualizador";
            this.Pnl_Visualizador.Size = new System.Drawing.Size(889, 515);
            this.Pnl_Visualizador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pnl_Visualizador.TabIndex = 2;
            this.Pnl_Visualizador.TabStop = false;
            this.Pnl_Visualizador.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Pnl_Visualizador.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.Pnl_Visualizador.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.Pnl_Visualizador.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.Pnl_Visualizador.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Btn_subir
            // 
            this.Btn_subir.CheckedState.Parent = this.Btn_subir;
            this.Btn_subir.CustomImages.Parent = this.Btn_subir;
            this.Btn_subir.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_subir.ForeColor = System.Drawing.Color.White;
            this.Btn_subir.HoverState.Parent = this.Btn_subir;
            this.Btn_subir.Image = ((System.Drawing.Image)(resources.GetObject("Btn_subir.Image")));
            this.Btn_subir.ImageSize = new System.Drawing.Size(30, 30);
            this.Btn_subir.Location = new System.Drawing.Point(29, 20);
            this.Btn_subir.Name = "Btn_subir";
            this.Btn_subir.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_subir.ShadowDecoration.Parent = this.Btn_subir;
            this.Btn_subir.Size = new System.Drawing.Size(71, 72);
            this.Btn_subir.TabIndex = 3;
            this.Btn_subir.Click += new System.EventHandler(this.Btn_subir_Click);
            // 
            // Btn_Guardar
            // 
            this.Btn_Guardar.CheckedState.Parent = this.Btn_Guardar;
            this.Btn_Guardar.CustomImages.Parent = this.Btn_Guardar;
            this.Btn_Guardar.Enabled = false;
            this.Btn_Guardar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_Guardar.ForeColor = System.Drawing.Color.White;
            this.Btn_Guardar.HoverState.Parent = this.Btn_Guardar;
            this.Btn_Guardar.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Guardar.Image")));
            this.Btn_Guardar.ImageSize = new System.Drawing.Size(30, 30);
            this.Btn_Guardar.Location = new System.Drawing.Point(129, 20);
            this.Btn_Guardar.Name = "Btn_Guardar";
            this.Btn_Guardar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_Guardar.ShadowDecoration.Parent = this.Btn_Guardar;
            this.Btn_Guardar.Size = new System.Drawing.Size(73, 72);
            this.Btn_Guardar.TabIndex = 4;
            this.Btn_Guardar.Click += new System.EventHandler(this.guna2CircleButton2_Click);
            // 
            // Btn_EliminarPunto
            // 
            this.Btn_EliminarPunto.CheckedState.Parent = this.Btn_EliminarPunto;
            this.Btn_EliminarPunto.CustomImages.Parent = this.Btn_EliminarPunto;
            this.Btn_EliminarPunto.Enabled = false;
            this.Btn_EliminarPunto.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Btn_EliminarPunto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_EliminarPunto.ForeColor = System.Drawing.Color.White;
            this.Btn_EliminarPunto.HoverState.Parent = this.Btn_EliminarPunto;
            this.Btn_EliminarPunto.Image = ((System.Drawing.Image)(resources.GetObject("Btn_EliminarPunto.Image")));
            this.Btn_EliminarPunto.Location = new System.Drawing.Point(21, 19);
            this.Btn_EliminarPunto.Name = "Btn_EliminarPunto";
            this.Btn_EliminarPunto.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_EliminarPunto.ShadowDecoration.Parent = this.Btn_EliminarPunto;
            this.Btn_EliminarPunto.Size = new System.Drawing.Size(71, 72);
            this.Btn_EliminarPunto.TabIndex = 5;
            this.Btn_EliminarPunto.Text = "Eliminar";
            this.Btn_EliminarPunto.Click += new System.EventHandler(this.BtnEliminarPunto_Click);
            // 
            // Btn_EditarNumero
            // 
            this.Btn_EditarNumero.CheckedState.Parent = this.Btn_EditarNumero;
            this.Btn_EditarNumero.CustomImages.Parent = this.Btn_EditarNumero;
            this.Btn_EditarNumero.Enabled = false;
            this.Btn_EditarNumero.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_EditarNumero.ForeColor = System.Drawing.Color.White;
            this.Btn_EditarNumero.HoverState.Parent = this.Btn_EditarNumero;
            this.Btn_EditarNumero.Image = ((System.Drawing.Image)(resources.GetObject("Btn_EditarNumero.Image")));
            this.Btn_EditarNumero.Location = new System.Drawing.Point(108, 19);
            this.Btn_EditarNumero.Name = "Btn_EditarNumero";
            this.Btn_EditarNumero.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_EditarNumero.ShadowDecoration.Parent = this.Btn_EditarNumero;
            this.Btn_EditarNumero.Size = new System.Drawing.Size(71, 72);
            this.Btn_EditarNumero.TabIndex = 6;
            this.Btn_EditarNumero.Text = "Editar";
            this.Btn_EditarNumero.Click += new System.EventHandler(this.BtnEditarNumero_Click);
            // 
            // Btn_ElipseColor
            // 
            this.Btn_ElipseColor.CheckedState.Parent = this.Btn_ElipseColor;
            this.Btn_ElipseColor.CustomImages.Parent = this.Btn_ElipseColor;
            this.Btn_ElipseColor.Enabled = false;
            this.Btn_ElipseColor.FillColor = System.Drawing.Color.Blue;
            this.Btn_ElipseColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_ElipseColor.ForeColor = System.Drawing.Color.White;
            this.Btn_ElipseColor.HoverState.Parent = this.Btn_ElipseColor;
            this.Btn_ElipseColor.Location = new System.Drawing.Point(287, 19);
            this.Btn_ElipseColor.Name = "Btn_ElipseColor";
            this.Btn_ElipseColor.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_ElipseColor.ShadowDecoration.Parent = this.Btn_ElipseColor;
            this.Btn_ElipseColor.Size = new System.Drawing.Size(74, 72);
            this.Btn_ElipseColor.TabIndex = 7;
            this.Btn_ElipseColor.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.Btn_Mover);
            this.guna2ShadowPanel1.Controls.Add(this.Btn_FontColor);
            this.guna2ShadowPanel1.Controls.Add(this.Btn_ElipseFont);
            this.guna2ShadowPanel1.Controls.Add(this.Btn_EliminarPunto);
            this.guna2ShadowPanel1.Controls.Add(this.Btn_ElipseColor);
            this.guna2ShadowPanel1.Controls.Add(this.Btn_EditarNumero);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(44)))), ((int)(((byte)(77)))));
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(59, 568);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 5;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(590, 118);
            this.guna2ShadowPanel1.TabIndex = 8;
            // 
            // Btn_Mover
            // 
            this.Btn_Mover.CheckedState.Parent = this.Btn_Mover;
            this.Btn_Mover.CustomImages.Parent = this.Btn_Mover;
            this.Btn_Mover.Enabled = false;
            this.Btn_Mover.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Btn_Mover.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_Mover.ForeColor = System.Drawing.Color.White;
            this.Btn_Mover.HoverState.Parent = this.Btn_Mover;
            this.Btn_Mover.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Mover.Image")));
            this.Btn_Mover.Location = new System.Drawing.Point(480, 20);
            this.Btn_Mover.Name = "Btn_Mover";
            this.Btn_Mover.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_Mover.ShadowDecoration.Parent = this.Btn_Mover;
            this.Btn_Mover.Size = new System.Drawing.Size(74, 72);
            this.Btn_Mover.TabIndex = 10;
            this.Btn_Mover.Text = "Mover";
            this.Btn_Mover.Click += new System.EventHandler(this.Btn_Mover_Click);
            // 
            // Btn_FontColor
            // 
            this.Btn_FontColor.CheckedState.Parent = this.Btn_FontColor;
            this.Btn_FontColor.CustomImages.Parent = this.Btn_FontColor;
            this.Btn_FontColor.Enabled = false;
            this.Btn_FontColor.FillColor = System.Drawing.Color.White;
            this.Btn_FontColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_FontColor.ForeColor = System.Drawing.Color.White;
            this.Btn_FontColor.HoverState.Parent = this.Btn_FontColor;
            this.Btn_FontColor.Location = new System.Drawing.Point(384, 19);
            this.Btn_FontColor.Name = "Btn_FontColor";
            this.Btn_FontColor.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_FontColor.ShadowDecoration.Parent = this.Btn_FontColor;
            this.Btn_FontColor.Size = new System.Drawing.Size(74, 72);
            this.Btn_FontColor.TabIndex = 9;
            this.Btn_FontColor.Click += new System.EventHandler(this.guna2CircleButton4_Click);
            // 
            // Btn_ElipseFont
            // 
            this.Btn_ElipseFont.CheckedState.Parent = this.Btn_ElipseFont;
            this.Btn_ElipseFont.CustomImages.Parent = this.Btn_ElipseFont;
            this.Btn_ElipseFont.Enabled = false;
            this.Btn_ElipseFont.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_ElipseFont.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_ElipseFont.ForeColor = System.Drawing.Color.White;
            this.Btn_ElipseFont.HoverState.Parent = this.Btn_ElipseFont;
            this.Btn_ElipseFont.Image = ((System.Drawing.Image)(resources.GetObject("Btn_ElipseFont.Image")));
            this.Btn_ElipseFont.Location = new System.Drawing.Point(196, 19);
            this.Btn_ElipseFont.Name = "Btn_ElipseFont";
            this.Btn_ElipseFont.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_ElipseFont.ShadowDecoration.Parent = this.Btn_ElipseFont;
            this.Btn_ElipseFont.Size = new System.Drawing.Size(71, 72);
            this.Btn_ElipseFont.TabIndex = 8;
            this.Btn_ElipseFont.Text = "Fuente";
            this.Btn_ElipseFont.Click += new System.EventHandler(this.guna2CircleButton3_Click);
            // 
            // guna2ShadowPanel2
            // 
            this.guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel2.Controls.Add(this.Btn_subir);
            this.guna2ShadowPanel2.Controls.Add(this.Btn_Guardar);
            this.guna2ShadowPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(44)))), ((int)(((byte)(77)))));
            this.guna2ShadowPanel2.Location = new System.Drawing.Point(734, 568);
            this.guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            this.guna2ShadowPanel2.Radius = 5;
            this.guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel2.Size = new System.Drawing.Size(230, 109);
            this.guna2ShadowPanel2.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(34)))), ((int)(((byte)(51)))));
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1019, 29);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guardarProgresoToolStripMenuItem,
            this.cargarProgresoToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(75, 25);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // guardarProgresoToolStripMenuItem
            // 
            this.guardarProgresoToolStripMenuItem.Name = "guardarProgresoToolStripMenuItem";
            this.guardarProgresoToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.guardarProgresoToolStripMenuItem.Text = "Guardar progreso";
            this.guardarProgresoToolStripMenuItem.Click += new System.EventHandler(this.guardarProgresoToolStripMenuItem_Click);
            // 
            // cargarProgresoToolStripMenuItem
            // 
            this.cargarProgresoToolStripMenuItem.Name = "cargarProgresoToolStripMenuItem";
            this.cargarProgresoToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.cargarProgresoToolStripMenuItem.Text = "Cargar progreso";
            this.cargarProgresoToolStripMenuItem.Click += new System.EventHandler(this.cargarProgresoToolStripMenuItem_Click);
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(12, 24);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(995, 17);
            this.guna2Separator1.TabIndex = 11;
            // 
            // ImgPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(34)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1019, 714);
            this.Controls.Add(this.guna2ShadowPanel2);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Controls.Add(this.Pnl_Visualizador);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.guna2Separator1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ImgPoints";
            this.Text = "ImgPoints";
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_Visualizador)).EndInit();
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Pnl_Visualizador;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_subir;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_Guardar;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_EliminarPunto;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_EditarNumero;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_ElipseColor;
        private System.Windows.Forms.ColorDialog ElipseColorDialog;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_ElipseFont;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_FontColor;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_Mover;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarProgresoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargarProgresoToolStripMenuItem;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
    }
}