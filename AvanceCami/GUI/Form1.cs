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
            // Obtener el mensaje del cuadro de texto
            string message = txtCancion.Text;


            // Enviar el mensaje al cliente F#
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
            // Obtener el mensaje del cuadro de texto
            //string message = txtCancion.Text;


            // Enviar el mensaje al cliente F#
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
            // Obtener el mensaje del cuadro de texto
            //string message = txtCancion.Text;


            // Enviar el mensaje al cliente F#
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
            // Obtener el mensaje del cuadro de texto
            //string message = txtCancion.Text;


            // Enviar el mensaje al cliente F#
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            String folderPath = "C:/Users/Ian Calvo/Desktop/Musica";
            if (Directory.Exists(folderPath))
            {
        // Obtener los nombres de todos los archivos .mp3 en la carpeta
        string[] mp3Files = Directory.GetFiles(folderPath, "*.mp3");

        foreach (string mp3File in mp3Files)
        {
            // Obtener el nombre del archivo sin la extensión
            string fileName = Path.GetFileNameWithoutExtension(mp3File);

            // Aquí puedes agregar tus restricciones. Por ejemplo, podrías verificar si el nombre del archivo contiene una cierta palabra:
            if (fileName.Contains("La")||fileName.Contains("Te")||fileName.Contains("Que")||fileName.Contains("Me")||fileName.Contains("Si")||fileName.Contains("Ya")||fileName.Contains("Querida"))
            {
                txtServerResponse.AppendText(fileName + Environment.NewLine);
            }
        }
    }
    else
    {
        txtServerResponse.AppendText("La carpeta no existe." + Environment.NewLine);
    } 
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String folderPath = "C:/Users/Ian Calvo/Desktop/Musica";
            if (Directory.Exists(folderPath))
    {
        // Obtener los nombres de todos los archivos .mp3 en la carpeta
        string[] mp3Files = Directory.GetFiles(folderPath, "*.mp3");

        foreach (string mp3File in mp3Files)
        {
            // Obtener el nombre del archivo sin la extensión
            string fileName = Path.GetFileNameWithoutExtension(mp3File);

            // Aquí puedes agregar tus restricciones. En este caso, verificamos si el nombre del archivo comienza con "H":
            if (fileName.StartsWith("H"))
            {
                txtServerResponse.AppendText(fileName + Environment.NewLine);
            }
        }
    }
    else
    {
        txtServerResponse.AppendText("La carpeta no existe." + Environment.NewLine);
    } 
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String folderPath = "C:/Users/Ian Calvo/Desktop/Musica";
        if (Directory.Exists(folderPath))
        {
        // Obtener los nombres de todos los archivos .mp3 en la carpeta
        string[] mp3Files = Directory.GetFiles(folderPath, "*.mp3");

        foreach (string mp3File in mp3Files)
        {
            // Obtener el nombre del archivo sin la extensión
            string fileName = Path.GetFileNameWithoutExtension(mp3File);

            // Obtener la duración del archivo .mp3
            using (Mp3FileReader reader = new Mp3FileReader(mp3File))
            {
                TimeSpan duration = reader.TotalTime;

                // Verificar si la duración es menor a 4 minutos
                if (duration.TotalMinutes < 4)
                {
                    txtServerResponse.AppendText(fileName + Environment.NewLine);
                }
            }
        }
    }
    else
    {
        txtServerResponse.AppendText("La carpeta no existe." + Environment.NewLine);
    } 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                txtServerResponse.Text = string.Empty;
                fSharpClientProcess.StandardInput.WriteLine("clear");
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }
        private void btnOutLista_Click(object sender, EventArgs e)
        {
               if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine("stop");

            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

        private void btnCLista_Click(object sender, EventArgs e)
        {
            string message = txtServerList.Text;
               if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine("c");
                fSharpClientProcess.StandardInput.WriteLine("a");
                fSharpClientProcess.StandardInput.WriteLine(message);
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }
        private void btnACLista_Click(object sender, EventArgs e)
        {
            string message = txtServerList.Text;
               if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine(message);
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

        private void btnPlayPlaylist_Click(object sender, EventArgs e)
        {
            //fSharpClientProcess.StandardInput.WriteLine("a");
            //fSharpClientProcess.StandardInput.WriteLine("a");
            //cuando le doy click a este boton que llame a una funcion que imprima solo los nombres de las canciones en cierta playlist, luego capturar esa salida y con eso
            //verificar nombre por nombre que este (eso iria a buscarlo en la ruta/maybe no implementar) y uno por uno a a reproducir, pasa que no se como iterar cancion por 
            //cancion...

        }
        

    }
}



    