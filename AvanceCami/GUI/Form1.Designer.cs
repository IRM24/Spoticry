
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
            this.btnLista = new System.Windows.Forms.Button();
            this.btnACLista = new System.Windows.Forms.Button();
            this.btnPlayPlaylist = new System.Windows.Forms.Button();
            this.btnCLista = new System.Windows.Forms.Button();
            this.txtServerList = new System.Windows.Forms.TextBox();
            
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // txtCancion
            // 
            this.txtCancion.Location = new System.Drawing.Point(320, 35);
            this.txtCancion.Name = "txtCancion";
            this.txtCancion.Size = new System.Drawing.Size(300, 20);
            this.txtCancion.TabIndex = 0; 

            this.txtServerList.Location = new System.Drawing.Point(320, 150);
            this.txtServerList.Name = "txtServerList";
            this.txtServerList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerList.Size = new System.Drawing.Size(300, 350);
            this.txtServerList.TabIndex = 2;

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

            this.btnLista.Location = new System.Drawing.Point(320, 520);
            this.btnLista.Name = "OutLista";
            this.btnLista.Size = new System.Drawing.Size(90, 40);
            this.btnLista.TabIndex = 1;
            this.btnLista.Text = "OutLista";
            this.btnLista.UseVisualStyleBackColor = true;
            this.btnLista.Click += new System.EventHandler(this.btnOutLista_Click);

            this.btnCLista.Location = new System.Drawing.Point(420, 520);
            this.btnCLista.Name = "CrearLista";
            this.btnCLista.Size = new System.Drawing.Size(90, 40);
            this.btnCLista.TabIndex = 1;
            this.btnCLista.Text = "Crear Lista";
            this.btnCLista.UseVisualStyleBackColor = true;
            this.btnCLista.Click += new System.EventHandler(this.btnCLista_Click);

            this.btnACLista.Location = new System.Drawing.Point(520, 520);
            this.btnACLista.Name = "AddCancion";
            this.btnACLista.Size = new System.Drawing.Size(90, 40);
            this.btnACLista.TabIndex = 1;
            this.btnACLista.Text = "Add Cancion";
            this.btnACLista.UseVisualStyleBackColor = true;
            this.btnACLista.Click += new System.EventHandler(this.btnACLista_Click);

            this.btnPlayPlaylist.Location = new System.Drawing.Point(620, 520);
            this.btnPlayPlaylist.Name = "PlayPlaylist";
            this.btnPlayPlaylist.Size = new System.Drawing.Size(90, 40);
            this.btnPlayPlaylist.TabIndex = 1;
            this.btnPlayPlaylist.Text = "PlayPlaylist";
            this.btnPlayPlaylist.UseVisualStyleBackColor = true;
            this.btnPlayPlaylist.Click += new System.EventHandler(this.btnPlayPlaylist_Click);
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
            // Form1
            // 

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
                this.newToolStripMenuItem,
                this.openToolStripMenuItem,
                this.saveToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "Criterios";
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);


            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "Canciones Español";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);

            
            // Opción Open
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Empieza con H";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            
            // Opción Save
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Menores a 4 minutos";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);

            //this.AutoScaleDimensions = new System.Drawing.SizeF(3F, 5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.txtServerResponse);
            this.Controls.Add(this.btnReproducir);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdelantar);
            this.Controls.Add(this.btnRetrasar);
            this.Controls.Add(this.btnLista);
            this.Controls.Add(this.btnCLista);
            this.Controls.Add(this.btnACLista);
            this.Controls.Add(this.btnPlayPlaylist);
            this.Controls.Add(this.txtCancion);
            this.Controls.Add(this.txtServerList);
            this.Name = "Form1";
            this.Text = "Server Client App";
            this.BackColor = System.Drawing.Color.FromArgb(38,55, 50); // Rojo (255, 0, 0)
            this.Controls.Add(this.menuStrip1); // Agregar el menú desplegable al formulario
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(107, 179, 156);
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Debes agregar estos campos privados para representar los componentes
        private System.Windows.Forms.TextBox txtCancion;
        private System.Windows.Forms.TextBox txtServerList;
        private System.Windows.Forms.Button btnReproducir;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.TextBox txtServerResponse;
        private System.Windows.Forms.Button btnAdelantar;
        private System.Windows.Forms.Button btnRetrasar;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLista;
        private System.Windows.Forms.Button btnCLista;
        private System.Windows.Forms.Button btnACLista;
        private System.Windows.Forms.Button btnPlayPlaylist;
        
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;

    }
}
