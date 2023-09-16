using System;
using System.Windows.Forms;
using System.Diagnostics;

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
                fSharpClientProcess.StandardInput.WriteLine(message);
                txtCancion.Clear();
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }
    }
}
