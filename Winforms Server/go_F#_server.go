package main

import (
	"bufio"
	"fmt"
	"io"
	"log"
	"math/rand"
	"net"
	"time"
)

func main() {
	listener, err := net.Listen("tcp", "localhost:8081") //crea un listener y define el tipo de servidor y la ip y puerto de conexion
	if err != nil {
		log.Fatalln(err) //corta el programa en caso de error
	}
	defer listener.Close() //hasta que no se termine la funcion main no se cae el servidor, defer postpone la ejecucion de la instruccion

	fmt.Println("*** Server STARTED ***")
	connectionNumber := 1
	for { //ciclo infinito que esta a la espera de clientes para conectarse
		fmt.Println("Wainting for connections...")
		con, err := listener.Accept() //accept congela la ejecucion hasta que la conexion con un cliente se realiza de forma exitosa

		if err != nil {
			log.Println(err)
			continue //en caso de que la conexion con haya tenido un error el programa sigue esperando por conexiones, no se cae
		}
		fmt.Println("Connection#", connectionNumber, " started!")

		go handleClientRequest(con, connectionNumber) //manejo de conexiones y todo lo que el servidor hace con el cliente (go es una instruccion para crear go routines, varios clientes se pueden conectar al mismo tiempo)
		connectionNumber += 1
	}
}

func handleClientRequest(con net.Conn, cn int) { //esta funcion recibe una conexion con y un int
	defer con.Close() //postpone que se cierren las conexiones hasta que se ejecute main

	clientReader := bufio.NewReader(con) //crea un buffer que lee datos proveniente del cliente (en este caso un texto)

	for { //ciclo infinito que espera infinitos requests del cliente
		clientRequest, err := clientReader.ReadString('\n') //leer el buffer "clientReader" lee el string

		switch err {
		case nil: //si no error
			if clientRequest == ":QUIT" { //si la data que el cliente manda es quit se cierra la conexion
				log.Println("client requested server to close the connection so closing")
				return
			} else {
				log.Println("CLIENT#", cn, " says: ", clientRequest) //en otro caso se imprime el mensaje del clietne

				rand.Seed(time.Now().UnixNano()) //esto no se que es, pero creo que es un tiempo de respuesta/como un sleep
				n := rand.Intn(5000)
				time.Sleep(time.Duration(n) * time.Millisecond)

				if _, err = con.Write([]byte("GOT IT!\n")); err != nil { //con.write escribe y manda al cliente got it, para indicar que el mensaje fue recibido
					log.Printf("failed to respond to client: %v\n", err)
				}
			}
		case io.EOF:
			log.Println("client closed the connection by terminating the process") //esto son unos manejos de errores que no se muy bien que son :)
			return
		default:
			log.Printf("error: %v\n", err)
			return
		}
	}
}
