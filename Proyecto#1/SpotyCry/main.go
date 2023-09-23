//version funcional

package main

import (
	"bytes"
	"os"
	"strings"

	"github.com/hajimehoshi/go-mp3"

	_ "net/http/pprof"

	"bufio"
	"fmt"
	"io/ioutil"
	"log"
	"net"
)

// Se define la estructura de la cancion
type cancion struct {
	nombre   string
	artista  string
	genero   string
	direcion string
}

type listaCanciones []cancion

var lCanciones listaCanciones

// Funcion para agregar canciones a la lista de canciones
func (l *listaCanciones) agregarCancion(nombre string, artista string, genero string, direccion string) {
	if l.buscarCancionIndice(nombre) == -1 {
		*l = append(*l, cancion{nombre: nombre, artista: artista, genero: genero, direcion: direccion})
	}
}

// Funcion que elimina una cancion de una lista
func (l *listaCanciones) EliminarCancion(nombre string) {
	var prod, err = l.buscarCancion(nombre)
	var indice = l.buscarCancionIndice(nombre)

	if err != -1 || (*prod).nombre == nombre {
		lCanciones = append(lCanciones[:indice], lCanciones[indice+1:]...)

		fmt.Printf("Se eliminara la cancion %s de la lista de reproduccion", nombre)
		fmt.Println(" ")
	}

}

// Direccion relativa de las canciones, los archivos mp3 deben estar dentro de la misma carpeta que el archivo main.go
var direccionR = "./"

// Funcion que llena de canciones llamando a agrega cancion
func llenarDatos() {
	lCanciones.agregarCancion("Hasta Que Me Olvides", "Luis Miguel", "Bolero", direccionR+"HastaQueMeOlvides.mp3")
	lCanciones.agregarCancion("Te Necesito", "Luis Miguel", "Bolero", direccionR+"TeNecesito.mp3")
	lCanciones.agregarCancion("Burning Love", "Elvis Presley", "Rock Clasico", direccionR+"Burninglove.mp3")
	lCanciones.agregarCancion("Querida", "Juan Gabriel ft. Juanes", "Pop Latino", direccionR+"Querida.mp3")
	lCanciones.agregarCancion("How Deep Is Your Love", "Bee Gees", "Soft Rock", direccionR+"HowDeepIsYourLove.mp3")
	lCanciones.agregarCancion("Que Me Des Tu Carinno", "Juan Luis Guerra", "Bachata", direccionR+"QueMeDesTuCarinno.mp3")
	lCanciones.agregarCancion("Human Nature", " Michael Jackson", "Pop", direccionR+"HumanNature.mp3")
}

// Envio de lista de canciones al cliente
func EnvioListaCanciones(conn net.Conn) {
	var buffer bytes.Buffer
	buffer.WriteString("Lista de Canciones:\n")
	for _, cancion := range lCanciones {
		buffer.WriteString(fmt.Sprintf("Nombre: %s\nArtista: %s\nGénero: %s\nDirección: %s\n\n \n",
			cancion.nombre, cancion.artista, cancion.genero, cancion.direcion))
	}
	// Enviar la lista de canciones al cliente
	_, err := conn.Write(buffer.Bytes())
	if err != nil {
		fmt.Println("Error al enviar la lista de canciones al cliente:", err)
		return
	}
	fmt.Println("Lista de canciones enviada al cliente.")
}

// el retorno es el índice del producto encontrado y -1 si no existe
func (l *listaCanciones) buscarCancion(nombre string) (*cancion, int) {
	var error = -1
	var c *cancion = nil
	var i int
	for i = 0; i < len(*l); i++ {
		if (*l)[i].nombre == nombre {
			error = 0
			c = &(*l)[i]
		}
	}
	return c, error
}

// Busca y agrega la direccion de un archivo mp3
func (l *listaCanciones) buscarDireccionCancion(nombre string) string {
	var direccion string
	var i int
	for i = 0; i < len(*l); i++ {
		if (*l)[i].nombre == nombre {
			direccion = (*l)[i].direcion
		}
	}
	return direccion
}

// Busca una cancion por indice y retorna validar =! -1 en caso de encontrarla
func (l *listaCanciones) buscarCancionIndice(nombre string) int {
	var validar = -1
	var i int
	for i = 0; i < len(*l); i++ {
		if (*l)[i].nombre == nombre {
			validar = i
		}
	}
	return validar
}

// Determina la duracion de una cancion
func DuracionCancion(cancion string) float64 {
	file, err := os.Open(cancion)
	// Abre el archivo MP3
	if err != nil {
		fmt.Println("Error al abrir el archivo:", err)
		return 0
	}
	defer file.Close()

	// Decodifica el archivo MP3
	decoder, err := mp3.NewDecoder(file)
	if err != nil {
		fmt.Println("Error al decodificar el archivo:", err)
		return 0
	}

	duracion := float64(decoder.Length()) / float64(decoder.SampleRate())
	minutos := (duracion / 60) / 4.12

	return minutos
}

// Criterio 1 Canciones con una duracion menor a 4min
func (l *listaCanciones) DuracionTodasCanciones() listaCanciones {
	var cancionesFiltradas listaCanciones
	for _, cancion := range *l {
		duracion := DuracionCancion(cancion.direcion)
		if duracion < 4.0 {
			cancionesFiltradas = append(cancionesFiltradas, cancion)
		}
	}
	return cancionesFiltradas
}

// Detecta si la cancion esta en espannol segun articulos y palabras claves
func CancionEnEspanol(titulo string) int {
	palabrasClaveEspanol := []string{"la", "te", "que", "me", "si", "ya", "querida"}

	// Convierte el título a minúsculas para hacer la comparación sin distinción entre mayúsculas y minúsculas
	titulo = strings.ToLower(titulo)

	// Verifica si el título contiene palabras clave en español
	for _, palabraClave := range palabrasClaveEspanol {
		if strings.Contains(titulo, palabraClave) {
			return 1
		}
	}
	return -1
}

// Retorna canciones en espannol
func (l *listaCanciones) CancionesEspannol() listaCanciones {
	var cancionesFiltradas listaCanciones
	for _, cancion := range *l {
		espannol := CancionEnEspanol(cancion.nombre)
		if espannol == 1 {
			cancionesFiltradas = append(cancionesFiltradas, cancion)
		}
	}
	return cancionesFiltradas
}

// Criterio Canciones que empiezan con la letra h
func (l *listaCanciones) CancionesEmpiezaConH() listaCanciones {
	var cancionesFiltradas listaCanciones
	for _, cancion := range *l {
		if strings.HasPrefix(strings.ToLower(cancion.nombre), "h") {
			cancionesFiltradas = append(cancionesFiltradas, cancion)
		}
	}
	return cancionesFiltradas
}

// Esta funcion no se utiliza activamente, pero permite annadir canciones desde consola
func AgregarCancionConsola() {
	fmt.Println("Ingresa el nombre de la cancion: ")
	reader := bufio.NewReader(os.Stdin) // permite leer una cadena de chars desde consola
	nombre, _ := reader.ReadString('\n')
	nombre = strings.TrimSpace(nombre)
	fmt.Scanln(&nombre)

	fmt.Println("Ingrese el nombre del artista o artistas: ")
	artista, _ := reader.ReadString('\n')
	artista = strings.TrimSpace(artista)
	fmt.Scanln(&artista)

	fmt.Println("Ingrese el genero: ")
	genero, _ := reader.ReadString('\n')
	genero = strings.TrimSpace(genero)
	fmt.Scanln(&genero)

	fmt.Println("Ingrese la direcion de la cancion: ")
	var direccion string
	fmt.Scanln(&direccion)

	lCanciones.agregarCancion(nombre, artista, genero, direccion)
}

// Esta funcion no se utiliza activamente, pero permite eliminar canciones desde consola
func EliminarCancionConsola() {
	fmt.Println("Ingresa el nombre de la cancion a eliminar: ")
	reader := bufio.NewReader(os.Stdin) // permite leer una cadena de chars desde consola
	nombre, _ := reader.ReadString('\n')
	nombre = strings.TrimSpace(nombre)
	fmt.Scanln(&nombre)

	lCanciones.EliminarCancion(nombre)
}

// Esta funcion no se utiliza activamente, pero permite imprimir canciones en consola
func ImpresionListaCanciones() {
	fmt.Println("Lista de Canciones:")
	for _, cancion := range lCanciones {
		fmt.Printf("Nombre: %s\nArtista: %s\nGénero: %s\nDirección: %s\n\n",
			cancion.nombre, cancion.artista, cancion.genero, cancion.direcion)
	}
}

// Envia canciones disponibles para reproduccion al cliente
func sendSongListToClient(conn net.Conn) {
	fmt.Println("Enviando lista de canciones al cliente...")

	// Itera sobre la lista de canciones y envía los detalles de cada canción al cliente
	for _, cancion := range lCanciones {
		// Formatea los detalles de la canción en una cadena con un separador, por ejemplo, una coma
		songInfo := fmt.Sprintf("%s,%s", cancion.nombre, cancion.artista)

		// Agrega un carácter de nueva línea al final de cada canción para separarlas
		songInfo += "\n \n"

		_, err := conn.Write([]byte(songInfo))
		if err != nil {
			fmt.Println("Error al enviar datos al cliente:", err)
			return
		}
	}
}

// Envia canciones que duran menos de 4 minutos al cliente
func EnviarCancionesDuracion(conn net.Conn) {
	fmt.Println("Enviando lista de canciones al cliente con duración menor a 4 minutos...")
	cancionesFiltradas := lCanciones.DuracionTodasCanciones()
	fmt.Println("Lista de Canciones con Duración < 4.0 minutos:")
	for _, cancion := range cancionesFiltradas {
		songInfo := fmt.Sprintf("Nombre: %s\nArtista: %s\nGénero: %s\n\n \n",
			cancion.nombre, cancion.artista, cancion.genero)
		_, err := conn.Write([]byte(songInfo))
		if err != nil {
			fmt.Println("Error al enviar datos al cliente:", err)
			return
		}
	}
	_, err := conn.Write([]byte("Lista de canciones enviada."))
	if err != nil {
		fmt.Println("Error al enviar datos al cliente:", err)
		return
	}
}

// Envia canciones que empiecen con la letra H al cliente
func EnviarCancionesH(conn net.Conn) {
	fmt.Println("Enviando lista de canciones al cliente que empiezan con 'H'...")
	cancionesFiltradas2 := lCanciones.CancionesEmpiezaConH()
	fmt.Println("Lista de Canciones con todas las canciones empiezan con h:")
	for _, cancion := range cancionesFiltradas2 {
		songInfo := fmt.Sprintf("Nombre: %s\nArtista: %s\nGénero: %s\n\n \n",
			cancion.nombre, cancion.artista, cancion.genero)
		_, err := conn.Write([]byte(songInfo))
		if err != nil {
			fmt.Println("Error al enviar datos al cliente:", err)
			return
		}
	}
	_, err := conn.Write([]byte("Lista de canciones enviada."))
	if err != nil {
		fmt.Println("Error al enviar datos al cliente:", err)
		return
	}
}

// Envia canciones que esten en espannol al cliente
func EnviarCancionesEspannol(conn net.Conn) {
	fmt.Println("Enviando lista de canciones al cliente que son en Espannol'...")
	cancionesFiltradas2 := lCanciones.CancionesEspannol()
	fmt.Println("Lista de Canciones con todas las canciones son en Espannol:")
	for _, cancion := range cancionesFiltradas2 {
		songInfo := fmt.Sprintf("Nombre: %s\nArtista: %s\nGénero: %s\n\n \n",
			cancion.nombre, cancion.artista, cancion.genero)
		_, err := conn.Write([]byte(songInfo))
		if err != nil {
			fmt.Println("Error al enviar datos al cliente:", err)
			return
		}
	}
	_, err := conn.Write([]byte("Lista de canciones enviada."))
	if err != nil {
		fmt.Println("Error al enviar datos al cliente:", err)
		return
	}
}

// Esta funcion no se utiliza de manera activa, pero permite detectar si una cancion existe o no en el servidor
func CompruebaExisteCancion(conn net.Conn) {
	// Leer el nombre de la canción del cliente.
	buffer := make([]byte, 1024)
	bytesRead, err := conn.Read(buffer)
	if err != nil {
		fmt.Println("Error al leer los datos del cliente:", err)
		return
	}
	nombreCancion := string(buffer[:bytesRead])

	// Verificar si la canción existe en la base de datos (sustituye esto con tu lógica real).
	songExists := lCanciones.buscarCancionIndice(nombreCancion)

	var message string
	// Preparar la respuesta del servidor.
	if songExists != -1 {
		message = "SongExists"
	} else {
		message = "SongNotFound"
	}

	// Enviar la respuesta al cliente.
	_, err = conn.Write([]byte(message))
	if err != nil {
		fmt.Println("Error al enviar la respuesta al cliente:", err)
	}
}

// Funcion que se encarga de manejar conexion con el cliente
func handleConnection(conn net.Conn) {
	defer conn.Close()

	// Después de recibir la instruccion del cliente
	buffer := make([]byte, 1)
	n, err := conn.Read(buffer)
	if err != nil {
		fmt.Println("Error al leer los datos:", err)
		return
	}
	letra := string(buffer[0])

	switch letra {
	case "a": //Reproduccion de musica, ya sea una cancion o playlist

		buffer := make([]byte, 1)
		n, err = conn.Read(buffer)
		if err != nil {
			fmt.Println("Error al leer los datos:", err)
			return
		}
		letra3 := string(buffer[0])

		if letra3 == "a" { //Reproduccion de una cancion singular
			// Recibir el nombre de la canción del cliente
			buffer = make([]byte, 1024)
			n, err = conn.Read(buffer)
			if err != nil {
				fmt.Println("Error al leer los datos:", err)
				return
			}
			songName := string(buffer[:n])

			filePath := lCanciones.buscarDireccionCancion(songName)
			songData, err := os.ReadFile(filePath)
			if err != nil {
				fmt.Println("Error al leer el archivo de la canción:", err)
				return
			}

			// Enviar los datos de la canción al cliente como bytes
			_, err = conn.Write(songData)
			if err != nil {
				fmt.Println("Error al enviar los datos al cliente:", err)
				return
			}

			fmt.Println("Canción enviada:", songName)
		}
		if letra3 == "b" { //Reproduccion de una playlist
			// Recibir el nombre de la canción del cliente
			buffer = make([]byte, 1024)
			n, err = conn.Read(buffer)
			if err != nil {
				fmt.Println("Error al leer los datos:", err)
				return
			}
			songName := string(buffer[:n])

			filePath := lCanciones.buscarDireccionCancion(songName)
			songData, err := os.ReadFile(filePath)
			if err != nil {
				fmt.Println("Error al leer el archivo de la canción:", err)
				return
			}

			// Enviar los datos de la canción al cliente como bytes
			_, err = conn.Write(songData)
			if err != nil {
				fmt.Println("Error al enviar los datos al cliente:", err)
				return
			}

			fmt.Println("Canción enviada:", songName)
		}

	case "b": //Criterios de busqueda
		handleConnectionCriterios(conn) //Funcion que maneja los criterios de busqueda

	case "c": //Actividades relacionadas con playlists
		buffer = make([]byte, 1024)
		n, err = conn.Read(buffer)
		if err != nil {
			fmt.Println("Error al leer los datos:", err)
			return
		}
		opcion := string(buffer[:n])
		if opcion == "a" { //Crear una nueva playlist
			fmt.Println("Cliente está creando una nueva playlist.")
		}
		if opcion == "b" { //Borrar una cancion de una playlist
			fmt.Println("Cliente está borrando una cancion.")
		}
		if opcion == "c" { //Actualizar playlist
			fmt.Println("Cliente está actualizando una playlist.")
			EnvioListaCanciones(conn)
		}
	case "l": //Enviar catalogo de canciones
		sendSongListToClient(conn)
	case "q": //Salir del menu
		// El cliente quiere salir del menú
		fmt.Println("Cliente ha salido del menú.")
		break

	default:
		// Opción no válida seleccionada por el cliente
		fmt.Println("Opción no válida seleccionada por el cliente.")
	}
}

// Funcion que se encarga de manejar casos de criterios de busqueda
func handleConnectionCriterios(conn net.Conn) {
	// Después de recibir el letra del cliente
	buffer := make([]byte, 1)
	_, err := conn.Read(buffer)
	if err != nil {
		fmt.Println("Error al leer los datos:", err)
		return
	}
	letra := string(buffer[0])

	if letra == "a" {
		//Canciones en espannol
		EnviarCancionesEspannol(conn)
	}
	if letra == "b" {
		//Canciones Menores a 4 minuts
		EnviarCancionesDuracion(conn)
	}
	if letra == "c" {
		//Canciones que empiezan con la letra H
		EnviarCancionesH(conn)
	}
}

// Funcion principal de entrada Main
func main() {
	dir, err := os.Getwd() //Detecta e imprime el directorio actual, funcionalidad meramente de verificacion
	if err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("Current directory:", dir)
	}

	files, err := ioutil.ReadDir(".") //Lee los archivos que se encuentran en el mismo directorio en el que esta el proyecot main.go
	if err != nil {                   //Aqui deben estar los archivos mp3, ya que la ruta relativa busca en este mismo directorio las canciones
		log.Fatal(err)
	}

	// Imprime los nombres de los archivos para verificar que las canciones esten en el mismo directorio
	for _, file := range files {
		fmt.Println(file.Name())
	}

	llenarDatos() //Llena los datos con las canciones

	port := "8081" //Puerto elegido
	listener, err := net.Listen("tcp", ":"+port)
	if err != nil {
		fmt.Println("Error al iniciar el servidor:", err)
		os.Exit(1)
	}
	defer listener.Close()

	fmt.Println("Servidor escuchando en el puerto", port) //Confirma el puerto
	for {
		conn, err := listener.Accept()
		if err != nil {
			fmt.Println("Error al aceptar la conexión:", err)
			continue
		}

		go handleConnection(conn) //Espera la conexion del cliente
	}
}
