using System;
using System.Windows.Forms;
using System.Diagnostics;
using NAudio.Wave;


namespace GUI
{
    public partial class Form1 : Form
    {
        private Process fSharpClientProcess; //Proceso que inicia el ejecutable del cliente (Proyecto F#)

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

        private void StartFSharpClient() //Metodo que inicia el cliente F#
        {
            fSharpClientProcess = new Process();
            fSharpClientProcess.StartInfo.FileName = "../Proyecto F#/bin/Debug/net7.0/Proyecto F#.exe"; //Ruta relativa que indica donde encontrar el ejecutable del cliente
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

        private void btnReproducir_Click(object sender, EventArgs e) //boton que reproduce musica
        {
            string message = txtCancion.Text; //lee el nombre de la cancion


            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine("a");
                fSharpClientProcess.StandardInput.WriteLine("a");
                fSharpClientProcess.StandardInput.WriteLine(message); //entra en modo reproduccion musical/cancion y manda el nombre de la cancion
                txtCancion.Clear();
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

         private void btnParar_Click(object sender, EventArgs e) //para la reproduccion musical
        {
            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                fSharpClientProcess.StandardInput.WriteLine("q"); //Sale al menu principal
                txtCancion.Clear();
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }

        private void btnAdelantar_Click(object sender, EventArgs e) //adelanta la cancion
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

        private void btnRetrasar_Click(object sender, EventArgs e) //retrasa la cancion
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

        private void criterioEspanol_Click(object sender, EventArgs e) //retorna la lista de canciones en espannol con el fin de agregar una a un playlist
        {   
            MessageBox.Show("1.Seleccione una cancion perteneciente al criterio seleccionado previamente (En la seccion 'Playlists')\n \n2.Seleccione la playlist a la que desea añadir la cancion\n \nLas acciones se confirman con 'Enter'");
            fSharpClientProcess.StandardInput.WriteLine("b");
            fSharpClientProcess.StandardInput.WriteLine("a");
        }

        private void criterio4_Click(object sender, EventArgs e) //retorna la lista de canciones con duracion menor a 4 minutos con el fin de agregar una a un playlist
        {
            MessageBox.Show("1.Seleccione una cancion perteneciente al criterio seleccionado previamente (En la seccion 'Playlists')\n \n2.Seleccione la playlist a la que desea añadir la cancion\n \nLas acciones se confirman con 'Enter'");
            fSharpClientProcess.StandardInput.WriteLine("b");
            fSharpClientProcess.StandardInput.WriteLine("b");
        }

        private void criterioH_Click(object sender, EventArgs e) //retorna la lista de canciones que empiezan con la letra H con el fin de agregar una a un playlist
        {
            MessageBox.Show("1.Seleccione una cancion perteneciente al criterio seleccionado previamente (En la seccion 'Playlists')\n \n2.Seleccione la playlist a la que desea añadir la cancion\n \nLas acciones se confirman con 'Enter'");
            fSharpClientProcess.StandardInput.WriteLine("b");
            fSharpClientProcess.StandardInput.WriteLine("c");

        }

        private void btnClear_Click(object sender, EventArgs e) //Limpia la bandeja de entrada
        {

            if (fSharpClientProcess != null && !fSharpClientProcess.HasExited)
            {
                txtServerResponse.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("El cliente F# no está en ejecución.");
            }
        }
        
        private void CPlaylist_Click(object sender, EventArgs e){ //Cumple la funcion de un enter, toma el texto en la entrada de text para playlists y lo manda al servidor
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

        private void PP_Click(object sender, EventArgs e){ //pausa y reproduce una cancion en reproduccion
            fSharpClientProcess.StandardInput.WriteLine("p");
        }

        private void btnCrearPlaylist_Click(object sender, EventArgs e){ //crea una playlist
            
            MessageBox.Show("1.Ingresar el nombre de la playlist que desea crear (En la seccion 'Playlists')\n \n2.Ingrese la cancion deseada\n \n3.Confirme el nombre de la playlist\n \nLas acciones se confirman con 'Enter'");
            fSharpClientProcess.StandardInput.WriteLine("c");
            fSharpClientProcess.StandardInput.WriteLine("a");
        }
        private void btnEliminarCancion_Click(object sender, EventArgs e){ //elimina una cancion de un playlist
            MessageBox.Show("1.Ingresar el nombre de la playlist que desea Eliminar una cancion (En la seccion 'Playlists')\n \n2.Ingrese la cancion deseada a eliminar\n \nLas acciones se confirman con 'Enter'");
            fSharpClientProcess.StandardInput.WriteLine("c");
            fSharpClientProcess.StandardInput.WriteLine("b");
        }
        private void btnActualizarPlaylist_Click(object sender, EventArgs e){ //actualiza un playlist
            MessageBox.Show("1.Ingresar el nombre de la playlist que Actualizar (En la seccion 'Playlists')\n \nLas acciones se confirman con 'Enter'");
            fSharpClientProcess.StandardInput.WriteLine("c");
            fSharpClientProcess.StandardInput.WriteLine("c");
        }
        private void btnLista_Click(object sender, EventArgs e){ //retorna el catalogo de canciones
            fSharpClientProcess.StandardInput.WriteLine("l");
        }
        private void btnPLista_Click(object sender, EventArgs e){ //retorna el catalogo de playlists
            fSharpClientProcess.StandardInput.WriteLine("y");
        }   

    }
}



    