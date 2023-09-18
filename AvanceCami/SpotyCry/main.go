//version funcional

package main

import (
	//"encoding/json"
	"bytes"
	"os"
	"strings"

	"github.com/hajimehoshi/go-mp3"

	_ "net/http/pprof"

	"bufio"
	"fmt"
	"net"
)

type cancion struct {
	nombre   string
	artista  string
	genero   string
	direcion string
}

type listaCanciones []cancion

var lCanciones listaCanciones

func (l *listaCanciones) agregarCancion(nombre string, artista string, genero string, direccion string) {
	if l.buscarCancionIndice(nombre) == -1 {
		*l = append(*l, cancion{nombre: nombre, artista: artista, genero: genero, direcion: direccion})
	}
}

func (l *listaCanciones) EliminarCancion(nombre string) {
	var prod, err = l.buscarCancion(nombre)
	var indice = l.buscarCancionIndice(nombre)

	if err != -1 || (*prod).nombre == nombre {
		lCanciones = append(lCanciones[:indice], lCanciones[indice+1:]...)

		fmt.Printf("Se eliminara la cancion %s de la lista de reproduccion", nombre)
		fmt.Println(" ")
	}

}

var direccionCami = "C:/Users/camiu/Downloads/"

var direccionIan = "C:/Users/Ian Calvo/Desktop/Musica/"

func llenarDatos() {
	lCanciones.agregarCancion("Hasta Que Me Olvides", "Luis Miguel", "Bolero", direccionIan+"HastaQueMeOlvides.mp3")
	lCanciones.agregarCancion("Te Necesito", "Luis Miguel", "Bolero", direccionIan+"TeNecesito.mp3")
	lCanciones.agregarCancion("Burning Love", "Elvis Presley", "Rock Clasico", direccionIan+"Burninglove.mp3")
	lCanciones.agregarCancion("Querida", "Juan Gabriel ft. Juanes", "Pop Latino", direccionIan+"Querida.mp3")
	lCanciones.agregarCancion("How Deep Is Your Love", "Bee Gees", "Soft Rock", direccionIan+"HowDeepIsYourLove.mp3")
	lCanciones.agregarCancion("Que Me Des Tu Carinno", "Juan Luis Guerra", "Bachata", direccionIan+"QueMeDesTuCarinno.mp3")
	lCanciones.agregarCancion("Human Nature", " Michael Jackson", "Pop", direccionIan+"HumanNature.mp3")
}

func (l *listaCanciones) buscarCancion(nombre string) (*cancion, int) { //el retorno es el índice del producto encontrado y -1 si no existe
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

func (l *listaCanciones) buscarCancionIndice(nombre string) int {
	var validar = -1
	var i int
	for i = 0; i < len(*l); i++ {
		if (*l)[i].nombre == nombre { //&& (*l)[i].artista == artista sera necesario validar tambien el artista?
			validar = i
		}
	}
	return validar
}

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

func EliminarCancionConsola() {
	fmt.Println("Ingresa el nombre de la cancion a eliminar: ")
	reader := bufio.NewReader(os.Stdin) // permite leer una cadena de chars desde consola
	nombre, _ := reader.ReadString('\n')
	nombre = strings.TrimSpace(nombre)
	fmt.Scanln(&nombre)

	lCanciones.EliminarCancion(nombre)
}

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

func sendSongListToClient(conn net.Conn) {
	fmt.Println("Enviando lista de canciones al cliente...")

	// Itera sobre la lista de canciones y envía los detalles de cada canción al cliente
	for _, cancion := range lCanciones {
		songInfo := fmt.Sprintf(cancion.nombre)
		fmt.Printf("")
		_, err := conn.Write([]byte(songInfo))
		if err != nil {
			fmt.Println("Error al enviar datos al cliente:", err)
			return
		}
	}

	// Indica al cliente que la lista de canciones ha sido enviada
	_, err := conn.Write([]byte("Lista de canciones enviada."))
	if err != nil {
		fmt.Println("Error al enviar datos al cliente:", err)
		return
	}
}

func EnviarCancionesDuracion(conn net.Conn) {
	fmt.Println("Enviando lista de canciones al cliente con duración menor a 4 minutos...")
	cancionesFiltradas := lCanciones.DuracionTodasCanciones()
	fmt.Println("Lista de Canciones con Duración < 4.0 minutos:")
	for _, cancion := range cancionesFiltradas {
		songInfo := fmt.Sprintf("Nombre: %s\nArtista: %s\nGénero: %s\n\n",
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

func EnviarCancionesH(conn net.Conn) {
	fmt.Println("Enviando lista de canciones al cliente que empiezan con 'H'...")
	cancionesFiltradas2 := lCanciones.CancionesEmpiezaConH()
	fmt.Println("Lista de Canciones con todas las canciones empiezan con h:")
	for _, cancion := range cancionesFiltradas2 {
		songInfo := fmt.Sprintf("Nombre: %s\nArtista: %s\nGénero: %s\n\n",
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

func EnviarCancionesEspannol(conn net.Conn) {
	fmt.Println("Enviando lista de canciones al cliente que son en Espannol'...")
	cancionesFiltradas2 := lCanciones.CancionesEspannol()
	fmt.Println("Lista de Canciones con todas las canciones son en Espannol:")
	for _, cancion := range cancionesFiltradas2 {
		songInfo := fmt.Sprintf("Nombre: %s\nArtista: %s\nGénero: %s\n\n",
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

func handleConnection(conn net.Conn) {
	defer conn.Close()
	// Después de recibir la letra del cliente
	EnvioListaCanciones(conn)
	buffer := make([]byte, 1)
	n, err := conn.Read(buffer)
	if err != nil {
		fmt.Println("Error al leer los datos:", err)
		return
	}
	letra := string(buffer[0])

	switch letra {
	case "a":

		buffer := make([]byte, 1)
		n, err = conn.Read(buffer)
		if err != nil {
			fmt.Println("Error al leer los datos:", err)
			return
		}
		letra3 := string(buffer[0])

		if letra3 == "a" {
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

		if letra3 == "b" {
			// Recibir el nombre de la canción del cliente
			buffer = make([]byte, 1024)
			n, err = conn.Read(buffer)
			if err != nil {
				fmt.Println("Error al leer los datos:", err)
				return
			}
			songName := string(buffer[:n])

			if direccion := lCanciones.buscarDireccionCancion(songName); direccion != "" {
				// Leer los datos de la canción desde el archivo
				songData, err := os.ReadFile(direccion)
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
			} else {
				fmt.Println("No existe esa cancion en la lista de reproduccion")
			}
		}

	case "b":
		handleConnectionCriterios(conn)

	case "c":
		buffer = make([]byte, 1024)
		n, err = conn.Read(buffer)
		if err != nil {
			fmt.Println("Error al leer los datos:", err)
			return
		}
		opcion := string(buffer[:n])
		if opcion == "a" {
			fmt.Println("Cliente está creando una nueva playlist.")
		}
		if opcion == "b" {
			fmt.Println("Cliente está borrando una cancion.")
		}
		if opcion == "c" {
			EliminarCancionConsola()
			fmt.Println("Cliente está actualizando una playlist.")
			sendSongListToClient(conn)
		}
		//CompruebaExisteCancion(conn)

	case "q":
		// El cliente quiere salir del menú
		fmt.Println("Cliente ha salido del menú.")
		break

	default:
		// Opción no válida seleccionada por el cliente
		fmt.Println("Opción no válida seleccionada por el cliente.")
	}
}

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
		EnviarCancionesEspannol(conn) // lista de reproduccion
	}
	if letra == "b" {
		EnviarCancionesDuracion(conn)
	}
	if letra == "c" {
		EnviarCancionesH(conn)
	}
}

func main() {
	llenarDatos()

	//AgregarCancionConsola()
	//EliminarCancionConsola()
	port := "8081"
	listener, err := net.Listen("tcp", ":"+port)
	if err != nil {
		fmt.Println("Error al iniciar el servidor:", err)
		os.Exit(1)
	}
	defer listener.Close()

	fmt.Println("Servidor escuchando en el puerto", port)
	for {
		conn, err := listener.Accept()
		if err != nil {
			fmt.Println("Error al aceptar la conexión:", err)
			continue
		}
		go handleConnection(conn)
	}
}
