//version antigua funcional

open System //abre el sistema para poder usar el resto de open que  vienen abajo que basicamente son funciones para conectar servidores
open System.IO
open System.Net
open System.Net.Sockets
open System.Threading

let serverAddress = IPAddress.Parse("127.0.0.1")//Ip
let serverPort = 8081//Puerto

//Obviamente el servidor e IP de tanto el cliente como servidor deben coincidir 
//*El server usa el puerto 8080 y de ip tiene localhost, que creo es lo mismo que: 127.0.0.1

let sendMessage (client : TcpClient) (message : string) = //esta cosa toma el cliente tcp y un mensaje string y lo manda 
    let writer = new StreamWriter(client.GetStream()) //clase de F# que facilita el flujo de datos de salida
    writer.WriteLine(message)//escribe el mensaje en el flujo de salida
    writer.Flush()//Flush es como cuando uno baja la cadena del toilet, basicamente vacia el buffer y manda los datos

let rec handleServerMessages (client : TcpClient) = //funcion que maneja el flujo de mensajes para el servidor, rec indica que es recursiva :O
    try
        let reader = new StreamReader(client.GetStream()) //lee datos de flujo de entrada, lee las respuestas del servidor
        while true do //ciclo que espera mensajes del servidor
            let response = reader.ReadLine() //captura la respuesta del servidor 
            printfn "Server response: %s" response
    with //excepcion si el try falla
    | :? IOException -> printfn "Connection to server closed"

let main() = //punto de entrada
    try
        let client = new TcpClient() //instancia de TcpClient, es el que va a conectarse con el server 
        client.Connect(serverAddress, serverPort)// establece la conexion
        
        printfn "Connected to server at %s:%d" (serverAddress.ToString()) serverPort

        let serverMessageHandler = new Thread(fun () -> handleServerMessages client)//crea un thread para e inicia la funcion que maneja los mensajes del server 
        serverMessageHandler.Start()//este subproceso maneja respuestas del servidor

        while true do
            let message = Console.ReadLine()//captura el mensaje escrito en consola
            sendMessage client message//envia el mensaje capturado en message

    with
    | ex -> printfn "Error: %s" ex.Message //manejo de errores

main()
