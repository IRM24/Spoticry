﻿
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
            this.SuspendLayout();
            // 
            // txtCancion
            // 
            this.txtCancion.Location = new System.Drawing.Point(320, 20);
            this.txtCancion.Name = "txtCancion";
            this.txtCancion.Size = new System.Drawing.Size(300, 20);
            this.txtCancion.TabIndex = 0; 

            // 
            // btnReproducir
            // 
            this.btnReproducir.Location = new System.Drawing.Point(320, 60);
            this.btnReproducir.Name = "Reproducir";
            this.btnReproducir.Size = new System.Drawing.Size(90, 40);
            this.btnReproducir.TabIndex = 1;
            this.btnReproducir.Text = "Reproducir";
            this.btnReproducir.UseVisualStyleBackColor = true;
            this.btnReproducir.Click += new System.EventHandler(this.btnReproducir_Click);
            // 
            // txtServerResponse
            // 
            this.txtServerResponse.Location = new System.Drawing.Point(1, 1);
            this.txtServerResponse.Multiline = true;
            this.txtServerResponse.Name = "txtServerResponse";
            this.txtServerResponse.ReadOnly = true;
            this.txtServerResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerResponse.Size = new System.Drawing.Size(300, 600);
            this.txtServerResponse.TabIndex = 2;
            // 
            // Form1
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(3F, 5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.txtServerResponse);
            this.Controls.Add(this.btnReproducir);
            this.Controls.Add(this.txtCancion);
            this.Name = "Form1";
            this.Text = "Server Client App";
            this.BackColor = System.Drawing.Color.FromArgb(38,55, 50); // Rojo (255, 0, 0)
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Debes agregar estos campos privados para representar los componentes
        private System.Windows.Forms.TextBox txtCancion;
        private System.Windows.Forms.Button btnReproducir;
        private System.Windows.Forms.TextBox txtServerResponse;
    }
}