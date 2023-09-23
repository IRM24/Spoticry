//version funcional
open System
open System.IO
open System.Net.Sockets
open NAudio.Wave
open System.Text 
open System.Collections.Generic
open System.Threading.Tasks

//Diccionario que almacena las playlists
let playlists = new Dictionary<string, List<string>>() 

//ip a la que se conecta el cliente
let serverAddr = "127.0.0.1"
//Puerto al que se conecta el cliente
let port = 8081

//Funcion que crea playlists
let crearPlaylist () =
    let playlistName = Console.ReadLine()
    
    // Verificar si la lista de reproducción ya existe
    if playlists.ContainsKey(playlistName) then
        Console.WriteLine("La lista de reproduccion ya existe")
    else
        // Crear una nueva lista de reproducción vacía
        let newPlaylist = new List<string>()
        playlists.Add(playlistName, newPlaylist)
        //Console.WriteLine("Lista de reproducción creada: " + playlistName)

//Objeto playlist individual que se almacenara en el diccionario 
let mutable playlist : Dictionary<string, List<string>> = Dictionary()

//Funcion que agrega canciones a la playlist
let annadirPlaylist2 (songName: string) : string=
    // Preguntar al usuario el nombre del playlist
    //Console.Write("Ingresa el nombre del playlist en el que deseas agregar la canción: ")
    let playlistName = Console.ReadLine()

    // Preguntar al usuario el nombre de la canción que desea agregar
    //Console.Write("Ingresa el nombre de la canción que deseas agregar: ")
    //let songName = Console.ReadLine()

    // Verificar si el playlist ya existe en el cliente
    if playlist.ContainsKey(playlistName) then
        // Agregar la canción al playlist existente
        let songsInPlaylist = playlist.[playlistName]
        songsInPlaylist.Add(songName)
    else
        // Crear un nuevo playlist y agregar la canción
        let newPlaylist = List([songName]) // Utiliza List para crear una lista mutable
        playlist.Add(playlistName, newPlaylist)

    sprintf "" 

//Funcion que agrega canciones a la playlist
let annadirPlaylist3(playlistName: string) (songName: string) =

    if playlist.ContainsKey(playlistName) then
        // Agregar la canción al playlist existente
        let songsInPlaylist = playlist.[playlistName]
        songsInPlaylist.Add(songName)
    else
        // Crear un nuevo playlist y agregar la canción
        let newPlaylist = List([songName]) // Utiliza List para crear una lista mutable
        playlist.Add(playlistName, newPlaylist)

//Funcion que remueve canciones de un playlist
let eliminarDePlaylist () =
    // Preguntar al usuario el nombre del playlist
    //Console.Write("Ingresa el nombre del playlist del que deseas eliminar la canción: ")
    let playlistName = Console.ReadLine()

    // Verificar si el playlist existe
    if playlist.ContainsKey(playlistName) then
        // Preguntar al usuario el nombre de la canción que desea eliminar
        //Console.Write("Ingresa el nombre de la canción que deseas eliminar: ")
        let songName = Console.ReadLine()

        // Verificar si la canción está en el playlist
        let songsInPlaylist = playlist.[playlistName]
        if songsInPlaylist.Contains(songName) then
            // Eliminar la canción del playlist
            songsInPlaylist.Remove(songName)
            Console.WriteLine("")
        else
            Console.WriteLine("La canción no está en el playlist.")
    else
        Console.WriteLine("El playlist no existe.")

//Imprime playlists
let imprimirPlaylist (playlists: Dictionary<string, List<string>>) =
    printfn "Playlists:"
    for kvp in playlists do
        let playlistName, songs = kvp.Key, kvp.Value
        printfn "Playlist: %s" playlistName
        printf " "
        printfn "Canciones:"
        Console.Write(" ")
        for song in songs do
            Console.Write(" ")
            printfn "  %s" song

//Recibe una lista de canciones provenientes del servidor
let rec recibeListaCanciones (songListBuilder: StringBuilder) (stream: NetworkStream) =
    let buffer = Array.zeroCreate 1024
    let bytesRead = stream.Read(buffer, 0, buffer.Length)
    if bytesRead > 0 then
        let songInfo = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead)
        songListBuilder.Append(songInfo)
        recibeListaCanciones songListBuilder stream
    else
        ()
 
//Reproduce una cancion y maneja todos los eventos que se puede realizar con la misma 
let reproduceCancion (stream: NetworkStream) = //version buena 1.0
    //Console.Write("Ingresa el nombre de la canción: ")
    let songName = Console.ReadLine()

    // Enviar el nombre de la canción al servidor
    let songNameBytes = System.Text.Encoding.UTF8.GetBytes(songName)
    stream.Write(songNameBytes, 0, songNameBytes.Length)

    use memoryStream = new MemoryStream()
    stream.CopyTo(memoryStream)
    memoryStream.Seek(0L, SeekOrigin.Begin)

    // Crear un archivo temporal para escribir los bytes
    let tempFilePath = Path.GetTempFileName() + ".mp3"
    use fileStream = new FileStream(tempFilePath, FileMode.Create)
    memoryStream.WriteTo(fileStream)

    // Reproducir la canción
    use mp3Reader = new Mp3FileReader(memoryStream)
    use outputDevice = new WaveOutEvent()
    
    outputDevice.Init(mp3Reader)
    outputDevice.Play()
    //Console.WriteLine("Reproduciendo la canción...")
    
    let audioFileLength = mp3Reader.TotalTime.TotalSeconds
    let mutable currentPosition = 0.0
    let mutable isPlaying = true

    try
        // Opciones para la canción
        while isPlaying do
            //Console.WriteLine("Opciones de la canción:")
            //Console.WriteLine("f. Adelantar canción")
            //Console.WriteLine("r. Retroceder canción")
            //Console.WriteLine("p. Pausar/Reanudar canción")
            //Console.WriteLine("q. Volver al menú principal")
            //Console.Write("Selecciona una opción: ")

            let songOption = Console.ReadLine()

            match songOption.ToLower() with
            | "f" -> // El cliente quiere adelantar la canción
                mp3Reader.Skip(10) // Adelanta 10 segundos

            | "r" -> // El cliente quiere retroceder la canción
                let currentPosition = mp3Reader.CurrentTime
                let newPosition = currentPosition.Add(TimeSpan.FromSeconds(-10.0))
                mp3Reader.CurrentTime <- newPosition

            | "p" -> // El cliente quiere pausar/reanudar la canción
                if outputDevice.PlaybackState = PlaybackState.Playing then
                    outputDevice.Pause()
                    //Console.WriteLine("Canción pausada.")
                    //isPlaying <- false
                else
                    outputDevice.Play()
                    //Console.WriteLine("Reanudando la canción.")
                    isPlaying <- true

            | "q" -> // El cliente quiere volver al menú principal
                //Console.WriteLine("Volviendo al menú principal...")
                isPlaying <- false

            | _ -> 
                Console.WriteLine("Opción no válida")

    finally
        // Ensure proper cleanup even if an exception is thrown
        outputDevice.Stop()
        outputDevice.Dispose() // Dispose of the output device
        mp3Reader.Dispose()
        memoryStream.Dispose() // Dispose of the memory stream
        fileStream.Close()
        File.Delete(tempFilePath)
    //Console.WriteLine("Canción terminada.")

//Reproduce canciones de un playlist 
let reproduceCancionPlaylist (stream: NetworkStream) (songName: string) = // Versión actualizada
    // Enviar el nombre de la canción al servidor
    let songNameBytes = System.Text.Encoding.UTF8.GetBytes(songName)
    stream.Write(songNameBytes, 0, songNameBytes.Length)

    use memoryStream = new MemoryStream()
    stream.CopyTo(memoryStream)
    memoryStream.Seek(0L, SeekOrigin.Begin)

    // Crear un archivo temporal para escribir los bytes
    let tempFilePath = Path.GetTempFileName() + ".mp3"
    use fileStream = new FileStream(tempFilePath, FileMode.Create)
    memoryStream.WriteTo(fileStream)

    // Reproducir la canción
    use mp3Reader = new Mp3FileReader(memoryStream)
    use outputDevice = new WaveOutEvent()
    
    outputDevice.Init(mp3Reader)
    outputDevice.Play()
    Console.WriteLine("Reproduciendo la canción...")
    
    let audioFileLength = mp3Reader.TotalTime.TotalSeconds
    let mutable currentPosition = 0.0
    let mutable isPlaying = true

    try
        // Opciones para la canción
        while isPlaying do
            //Console.WriteLine("Opciones de la canción:")
            //Console.WriteLine("f. Adelantar canción")
            //Console.WriteLine("r. Retroceder canción")
            //Console.WriteLine("p. Pausar/Reanudar canción")
            //Console.WriteLine("q. Volver al menú principal")
            //Console.Write("Selecciona una opción: ")

            let songOption = Console.ReadLine()

            match songOption.ToLower() with
            | "f" -> // El cliente quiere adelantar la canción
                mp3Reader.Skip(10) // Adelanta 10 segundos

            | "r" -> // El cliente quiere retroceder la canción
                let currentPosition = mp3Reader.CurrentTime
                let newPosition = currentPosition.Add(TimeSpan.FromSeconds(-10.0))
                mp3Reader.CurrentTime <- newPosition

            | "p" -> // El cliente quiere pausar/reanudar la canción
                if outputDevice.PlaybackState = PlaybackState.Playing then
                    outputDevice.Pause()
                    Console.WriteLine("Canción pausada.")
                    //isPlaying <- false
                else
                    outputDevice.Play()
                    Console.WriteLine("Reanudando la canción.")
                    isPlaying <- true

            | "q" -> // El cliente quiere volver al menú principal
                Console.WriteLine("Volviendo al menú principal...")
                isPlaying <- false

            | _ -> 
                Console.WriteLine("Opción no válida")
    finally
        // Ensure proper cleanup even if an exception is thrown
        outputDevice.Stop()
        outputDevice.Dispose() // Dispose of the output device
        mp3Reader.Dispose()
        memoryStream.Dispose() // Dispose of the memory stream
        fileStream.Close()
        File.Delete(tempFilePath)
    //Console.WriteLine("Canción terminada.")

//Acciones de actualizacion de playlist
let actualizarPlaylist (playlistName: string) (cancionesDesdeServidor: string) =
    // Verificar si la playlist existe
    if playlists.ContainsKey(playlistName) then
        let playlist = playlists.[playlistName]
        // Dividir la cadena de canciones desde el servidor en una lista de canciones
        let cancionesEnServidor = cancionesDesdeServidor.Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries) |> Set.ofArray
        let playlistComoStringList = List.ofSeq<string> playlist

        let playlistActualizada = List.filter (fun cancion -> cancionesEnServidor.Contains(cancion)) playlistComoStringList
        
        // Eliminar la playlist existente
        playlists.Remove(playlistName)

        // Agregar la nueva playlist actualizada
        for cancion in playlistActualizada do
            annadirPlaylist3 playlistName cancion

        printfn "Playlist '%s' actualizada." playlistName
    else
        printfn "Playlist '%s' no encontrada." playlistName

//Funcion de entrada principal
let main =
    try
        use client = new TcpClient(serverAddr, port)
        use stream = client.GetStream()

        // Bucle principal para mantener al cliente esperando nuevas solicitudes
        let mutable continueRunning = true
        while continueRunning do
            use client = new TcpClient(serverAddr, port)
            use stream = client.GetStream()
            //Console.WriteLine("a. Reproducir Musica")
            //Console.WriteLine("b. Criterios de Busqueda")
            //Console.WriteLine("c. Crear Playlist")
            //Console.WriteLine("q. Salir")
            //Console.Write("Selecciona una opción: ")

            // Recibir una letra desde la consola
            let letter = Console.ReadLine()

            // Enviar la letra al servidor
            let letterBytes = System.Text.Encoding.UTF8.GetBytes(letter)
            stream.Write(letterBytes, 0, letterBytes.Length)

            match letter with
            | "a" -> // El cliente quiere reproducir una canción
                // Recibir los datos de la canción del servidor
                
                //Console.WriteLine("a. Reproducir una cancion")
                //Console.WriteLine("b. Reproducir un playlist")
                //Console.Write("Selecciona una opción: ")

                // Recibir una letra desde la consola
                let letter3 = Console.ReadLine()

                // Enviar la letra al servidor
                let letterBytes = System.Text.Encoding.UTF8.GetBytes(letter3)
                stream.Write(letterBytes, 0, letterBytes.Length)

                match letter3 with
                | "a" -> //Reproducir cancion
                    reproduceCancion stream
                    client.Close()
                | "b" -> //Reproducir playlist
                    let selectedPlaylist = Console.ReadLine()
                    if playlists.ContainsKey(selectedPlaylist) then
                        // Llama a la función para reproducir la playlist seleccionada
                        match playlists.TryGetValue(selectedPlaylist) with
                            | true, songList ->
        
                                for songName in songList do
                                    use client = new TcpClient(serverAddr, port)
                                    use stream = client.GetStream()
                                    reproduceCancionPlaylist stream songName
                                    client.Close()
                                
                            | false, _ ->
                                Console.WriteLine("Playlist no encontrada.")
                    else
                        Console.WriteLine("Playlist no encontrada.")
                | _ ->  ()

            | "b" -> //Acciones relacionadas con criterios de busqueda
                
                //Console.WriteLine("a. Canciones en Espannol")
                //Console.WriteLine("b. Canciones menores a 4 minutos")
                //Console.WriteLine("c. Canciones que empiecen con la letra H")
                //Console.Write("Selecciona una opción: ")
                
                // Recibir una letra desde la consola
                let letter2 = Console.ReadLine()

                // Enviar la letra al servidor
                let letterBytes = System.Text.Encoding.UTF8.GetBytes(letter2)
                stream.Write(letterBytes, 0, letterBytes.Length)

                match letter2 with
                | "a" | "b" | "c" -> 
                    let songListBuilder = new StringBuilder()
                    recibeListaCanciones songListBuilder stream
                    let songList = songListBuilder.ToString()
                    //Console.WriteLine("Lista de Canciones:")
                    Console.WriteLine(songList) //Imprime la lista de canciones 

                    //Console.WriteLine("Seleccione una canción para agregar al playlist:")
                    let selectedSong = Console.ReadLine()

                    // Verificar si la canción seleccionada existe en la lista de canciones disponibles
                    if songList.Contains(selectedSong) then
                        let result = annadirPlaylist2 selectedSong
                        Console.WriteLine(result)
                    else
                        Console.WriteLine("La canción seleccionada no está en la lista de canciones disponibles.")

                    imprimirPlaylist playlist
                | _ ->  ()

            | "c" -> //Acciones relacionadas con playlists
                    
                    // Recibir una letra desde la consola
                    let letter2 = Console.ReadLine()

                    // Enviar la letra al servidor
                    let letterBytes = System.Text.Encoding.UTF8.GetBytes(letter2)
                    stream.Write(letterBytes, 0, letterBytes.Length)
                    
                    match letter2 with
                        | "a" -> //Crear una playlist
                            crearPlaylist ()
                            //Console.WriteLine("Ingrese una cancion para agregar a la playlist: ")
                            let cancion = Console.ReadLine()
                            let result = annadirPlaylist2 cancion
                            //Console.WriteLine(result)
                            imprimirPlaylist playlist
                        | "b" -> //Eliminar cancion de playlist
                            eliminarDePlaylist ()
                            imprimirPlaylist playlist
                        | "c" -> //Actualizar playlist
                            imprimirPlaylist playlist

                            let songListBuilder = new StringBuilder()
                            recibeListaCanciones songListBuilder stream
                            let cancionesDisponiblesEnElServidor = songListBuilder.ToString()

                            //Console.Write("Ingresa el nombre de la playlist que desea actualizar: ")
                            let playlistName = Console.ReadLine()

                            actualizarPlaylist playlistName cancionesDisponiblesEnElServidor
                            imprimirPlaylist playlist
                        | _ ->  ()
            
            | "q" -> // El cliente quiere salir
                continueRunning <- false
            | "l" -> //Instruccion de mandar catalogo de canciones
                let songListBuilder = new StringBuilder()
                recibeListaCanciones songListBuilder stream
                let songList = songListBuilder.ToString()
                Console.WriteLine(songList)
            | "y" -> //Catalogo de playlists actuales
                imprimirPlaylist playlist
            | _ -> ()
        client.Close()

    with
        | ex -> 
            printfn "Error: %s" ex.Message