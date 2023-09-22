using System;
using System.Windows.Forms;
using System.Diagnostics;
using NAudio.Wave;


namespace GUI
{
    public partial class Form1 : Form
    {
        private Process fSharpClientProcess;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Iniciar el cliente F# al cargar el formulario
            StartFSharpClient();
            this.WindowState = FormWindowState.Maximized;
        }

        private void StartFSharpClient()
        {
            fSharpClientProcess = new Process();
            fSharpClientProcess.StartInfo.FileName = "C:/Users/Ian Calvo/Desktop/Spoticry/AvanceCami/Proyecto F#/bin/Debug/net7.0/Proyecto F#.exe"; // Reemplaza con la ruta de tu ejecutable de F#
            fSharpClientProcess.StartInfo.UseShellExecute = false;
            fSharpClientProcess.StartInfo.RedirectStandardInput = true;
            fSharpClientProcess.StartInfo.RedirectStandardOutput = true;
            fSharpClientProcess.StartInfo.CreateNoWindow = true;

            fSharpClientProcess.OutputDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    // Actualizar el cuadro de texto con la respuesta del servidor
                    UpdateServerResponse(e.Data);
                }
            };

            fSharpClientProcess.Start();
            fSharpClientProcess.BeginOutputReadLine();
        }

        private void UpdateServerResponse(string response)
        {
            if (txtServerResponse.InvokeRequired)
            {
                txtServerResponse.Invoke(new Action(() =>
                {
                    txtServerResponse.AppendText(response + Environment.NewLine);
                }));
            }
            else
            {
                txtServerResponse.AppendText(response + Environment.NewLine);
            }
        }

        private void btnReproducir_Click(object sender, EventArgs e)
        {
            string message = txtCancion.Text;


            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine("a");
                fSharpClientProcess.StandardInput.WriteLine("a");
                fSharpClientProcess.StandardInput.WriteLine(message);
                txtCancion.Clear();
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

         private void btnParar_Click(object sender, EventArgs e)
        {
            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine("q");
                txtCancion.Clear();
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

        private void btnAdelantar_Click(object sender, EventArgs e)
        {
            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine("f");
                txtCancion.Clear();
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

        private void btnRetrasar_Click(object sender, EventArgs e)
        {
            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine("r");
                txtCancion.Clear();
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

        private void criterioEspanol_Click(object sender, EventArgs e)
        {   
            fSharpClientProcess.StandardInput.WriteLine("b");
            fSharpClientProcess.StandardInput.WriteLine("a");
        }

        private void criterio4_Click(object sender, EventArgs e)
        {
            fSharpClientProcess.StandardInput.WriteLine("b");
            fSharpClientProcess.StandardInput.WriteLine("b");
        }

        private void criterioH_Click(object sender, EventArgs e)
        {
            fSharpClientProcess.StandardInput.WriteLine("b");
            fSharpClientProcess.StandardInput.WriteLine("c");

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                txtServerResponse.Text = string.Empty;
                //fSharpClientProcess.StandardInput.WriteLine("clear");
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }
        
        private void CPlaylist_Click(object sender, EventArgs e){
            string message = txtCancionP.Text;
            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine(message);
                txtCancionP.Clear();
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

        private void PP_Click(object sender, EventArgs e){
            fSharpClientProcess.StandardInput.WriteLine("p");
        }

        private void btnCrearPlaylist_Click(object sender, EventArgs e){
            
            txtServerResponse.Text = "1.Ingresar el nombre de la playlist que desea crear\n2.Ingrese la cancion deseada\n                               \n3.Confirme el nombre de la playlist                              \nLas acciones se confirman con 'Enter'";
            fSharpClientProcess.StandardInput.WriteLine("c");
            fSharpClientProcess.StandardInput.WriteLine("a");
        }
        private void btnEliminarCancion_Click(object sender, EventArgs e){
            txtServerResponse.Text = "1.Ingresar el nombre de la playlist que desea eliminar una cancion\n                                             2.Ingrese la cancion desea eliminar\n                              Las acciones se confirman con 'Enter' ";
            fSharpClientProcess.StandardInput.WriteLine("c");
            fSharpClientProcess.StandardInput.WriteLine("b");
        }
        private void btnActualizarPlaylist_Click(object sender, EventArgs e){
            fSharpClientProcess.StandardInput.WriteLine("c");
            fSharpClientProcess.StandardInput.WriteLine("c");
        }
        private void btnLista_Click(object sender, EventArgs e){
            fSharpClientProcess.StandardInput.WriteLine("l");
        }
        private void btnPLista_Click(object sender, EventArgs e){
            fSharpClientProcess.StandardInput.WriteLine("y");
        }   

    }
}



    