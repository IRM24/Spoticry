
namespace GUI
{
    partial class Form1
    {
        // Declaraciones de componentes generadas automáticamente por Visual Studio
        private System.ComponentModel.IContainer components = null;

        // Reemplaza este método con tu propio código de inicialización de componentes si es necesario
        private void InitializeComponent()
        {
            this.txtCancion = new System.Windows.Forms.TextBox();
            this.btnReproducir = new System.Windows.Forms.Button();
            this.txtServerResponse = new System.Windows.Forms.TextBox();
            this.btnParar = new System.Windows.Forms.Button();
            this.btnAdelantar = new System.Windows.Forms.Button();
            this.btnRetrasar = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.CPlaylist = new System.Windows.Forms.Button();
            this.PP = new System.Windows.Forms.Button();
            this.btnCrearPlaylist = new System.Windows.Forms.Button();
            this.btnEliminarCancion = new System.Windows.Forms.Button();
            this.btnActualizarPlaylist = new System.Windows.Forms.Button();
            
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.criterioEspanol = new System.Windows.Forms.ToolStripMenuItem();
            this.criterioH = new System.Windows.Forms.ToolStripMenuItem();
            this.criterio4 = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // txtCancion
            // 
            this.txtCancion.Location = new System.Drawing.Point(320, 35);
            this.txtCancion.Name = "txtCancion";
            this.txtCancion.Size = new System.Drawing.Size(300, 20);
            this.txtCancion.TabIndex = 0; 

            // 
            // botones
            // 
            this.btnReproducir.Location = new System.Drawing.Point(320, 70);
            this.btnReproducir.Name = "Reproducir";
            this.btnReproducir.Size = new System.Drawing.Size(90, 40);
            this.btnReproducir.TabIndex = 1;
            this.btnReproducir.Text = "Reproducir";
            this.btnReproducir.UseVisualStyleBackColor = true;
            this.btnReproducir.Click += new System.EventHandler(this.btnReproducir_Click);

            this.btnParar.Location = new System.Drawing.Point(420, 70);
            this.btnParar.Name = "Parar";
            this.btnParar.Size = new System.Drawing.Size(90, 40);
            this.btnParar.TabIndex = 1;
            this.btnParar.Text = "Parar";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);

            this.btnAdelantar.Location = new System.Drawing.Point(520, 70);
            this.btnAdelantar.Name = "Adelantar";
            this.btnAdelantar.Size = new System.Drawing.Size(90, 40);
            this.btnAdelantar.TabIndex = 1;
            this.btnAdelantar.Text = "Adelantar";
            this.btnAdelantar.UseVisualStyleBackColor = true;
            this.btnAdelantar.Click += new System.EventHandler(this.btnAdelantar_Click);

            this.btnRetrasar.Location = new System.Drawing.Point(620, 70);
            this.btnRetrasar.Name = "Retrasar";
            this.btnRetrasar.Size = new System.Drawing.Size(90, 40);
            this.btnRetrasar.TabIndex = 1;
            this.btnRetrasar.Text = "Retrasar";
            this.btnRetrasar.UseVisualStyleBackColor = true;
            this.btnRetrasar.Click += new System.EventHandler(this.btnRetrasar_Click);

            this.btnClear.Location = new System.Drawing.Point(320, 650);
            this.btnClear.Name = "Clear";
            this.btnClear.Size = new System.Drawing.Size(90, 40);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.CPlaylist.Location = new System.Drawing.Point(320, 120);
            this.CPlaylist.Name = "Enter";
            this.CPlaylist.Size = new System.Drawing.Size(90, 40);
            this.CPlaylist.TabIndex = 1;
            this.CPlaylist.Text = "Enter";
            this.CPlaylist.UseVisualStyleBackColor = true;
            this.CPlaylist.Click += new System.EventHandler(this.CPlaylist_Click);

            this.PP.Location = new System.Drawing.Point(420, 120);
            this.PP.Name = "PP";
            this.PP.Size = new System.Drawing.Size(90, 40);
            this.PP.TabIndex = 1;
            this.PP.Text = "Pause/Play";
            this.PP.UseVisualStyleBackColor = true;
            this.PP.Click += new System.EventHandler(this.PP_Click);

            this.btnCrearPlaylist.Location = new System.Drawing.Point(320, 170);
            this.btnCrearPlaylist.Name = "CrearPlaylist";
            this.btnCrearPlaylist.Size = new System.Drawing.Size(90, 40);
            this.btnCrearPlaylist.TabIndex = 1;
            this.btnCrearPlaylist.Text = "Crear Playlist";
            this.btnCrearPlaylist.UseVisualStyleBackColor = true;
            this.btnCrearPlaylist.Click += new System.EventHandler(this.btnCrearPlaylist_Click);

            // Botón "Eliminar Canción de Playlist"
            this.btnEliminarCancion.Location = new System.Drawing.Point(420, 170);
            this.btnEliminarCancion.Name = "EliminarCancion";
            this.btnEliminarCancion.Size = new System.Drawing.Size(90, 40);
            this.btnEliminarCancion.TabIndex = 1;
            this.btnEliminarCancion.Text = "Eliminar Canción de Playlist";
            this.btnEliminarCancion.UseVisualStyleBackColor = true;
            this.btnEliminarCancion.Click += new System.EventHandler(this.btnEliminarCancion_Click);

            // Botón "Actualizar Playlist"
            this.btnActualizarPlaylist.Location = new System.Drawing.Point(520, 170);
            this.btnActualizarPlaylist.Name = "ActualizarPlaylist";
            this.btnActualizarPlaylist.Size = new System.Drawing.Size(90, 40);
            this.btnActualizarPlaylist.TabIndex = 1;
            this.btnActualizarPlaylist.Text = "Actualizar Playlist";
            this.btnActualizarPlaylist.UseVisualStyleBackColor = true;
            this.btnActualizarPlaylist.Click += new System.EventHandler(this.btnActualizarPlaylist_Click);

            // 
            // txtServerResponse
            // 
            this.txtServerResponse.Location = new System.Drawing.Point(1, 30);
            this.txtServerResponse.Multiline = true;
            this.txtServerResponse.Name = "txtServerResponse";
            this.txtServerResponse.ReadOnly = true;
            this.txtServerResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerResponse.Size = new System.Drawing.Size(300, 700);
            this.txtServerResponse.TabIndex = 2;
            //
            //Menu
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";

            // Configurar las opciones del menú desplegable
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.criterioEspanol,
                this.criterioH,
                this.criterio4
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "Criterios";
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            //this.criterioEspanol.Click += new System.EventHandler(this.criterioEspanol_Click);


            this.criterioEspanol.Name = "criterioEspanol";
            this.criterioEspanol.Size = new System.Drawing.Size(152, 22);
            this.criterioEspanol.Text = "Canciones Español";
            this.criterioEspanol.Click += new System.EventHandler(this.criterioEspanol_Click);

            
            // Opción Open
            this.criterio4.Name = "4minutos";
            this.criterio4.Size = new System.Drawing.Size(152, 22);
            this.criterio4.Text = "Menores a 4 minutos";
            this.criterio4.Click += new System.EventHandler(this.criterio4_Click);
            
            // Opción Save
            this.criterioH.Name = "EmpiezaH";
            this.criterioH.Size = new System.Drawing.Size(152, 22);
            this.criterioH.Text = "Empieza con H";
            this.criterioH.Click += new System.EventHandler(this.criterioH_Click);

            //this.AutoScaleDimensions = new System.Drawing.SizeF(3F, 5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.txtServerResponse);
            this.Controls.Add(this.btnReproducir);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdelantar);
            this.Controls.Add(this.btnRetrasar);
            this.Controls.Add(this.txtCancion);
            this.Controls.Add(this.CPlaylist);
            this.Controls.Add(this.PP);
            this.Controls.Add(this.btnCrearPlaylist);
            this.Controls.Add(this.btnEliminarCancion);
            this.Controls.Add(this.btnActualizarPlaylist);
            this.Name = "Form1";
            this.Text = "Server Client App";
            this.BackColor = System.Drawing.Color.FromArgb(38,55, 50); // Rojo (255, 0, 0)
            this.Controls.Add(this.menuStrip1); // Agregar el menú desplegable al formulario
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(107, 179, 156);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Debes agregar estos campos privados para representar los componentes
        private System.Windows.Forms.TextBox txtCancion;
        private System.Windows.Forms.Button btnReproducir;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.TextBox txtServerResponse;
        private System.Windows.Forms.Button btnAdelantar;
        private System.Windows.Forms.Button btnRetrasar;
        private System.Windows.Forms.Button btnClear;

        
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem criterioEspanol;
        private System.Windows.Forms.ToolStripMenuItem criterioH;
        private System.Windows.Forms.ToolStripMenuItem criterio4;
        private System.Windows.Forms.Button CPlaylist;
        private System.Windows.Forms.Button PP;
        private System.Windows.Forms.Button btnCrearPlaylist;
        private System.Windows.Forms.Button btnEliminarCancion;
        private System.Windows.Forms.Button btnActualizarPlaylist;

    }
}
