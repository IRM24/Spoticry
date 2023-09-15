//version funcional
open System
open System.IO
open System.Net.Sockets
open NAudio.Wave
open System.Text 
open System.Collections.Generic
open System.Threading.Tasks


let playlists = new Dictionary<string, List<string>>() 


let serverAddr = "127.0.0.1"
let port = 8081

let createPlaylist () =
    Console.Write("Ingresa el nombre de la lista de reproduccion: ")
    let playlistName = Console.ReadLine()
    
    // Verificar si la lista de reproducción ya existe
    if playlists.ContainsKey(playlistName) then
        Console.WriteLine("La lista de reproduccion ya existe")
    else
        // Crear una nueva lista de reproducción vacía
        let newPlaylist = new List<string>()
        playlists.Add(playlistName, newPlaylist)
        Console.WriteLine("Lista de reproducción creada: " + playlistName)

// Función para agregar una canción a una lista de reproducción
let mutable playlist : Dictionary<string, List<string>> = Dictionary()

let addToPlaylist (stream: NetworkStream) =
    // Preguntar al usuario el nombre del playlist
    Console.Write("Ingresa el nombre del playlist en el que deseas agregar la canción: ")
    let playlistName = Console.ReadLine()

    // Preguntar al usuario el nombre de la canción que desea agregar
    Console.Write("Ingresa el nombre de la canción que deseas agregar: ")
    let songName = Console.ReadLine()

    // Enviar el nombre de la canción para verificar si existen
    let songNameBytes = System.Text.Encoding.UTF8.GetBytes(songName)
    stream.Write(songNameBytes, 0, songNameBytes.Length)

    // Recibir la respuesta del servidor
    let responseBytes = Array.zeroCreate 1024
    let mutable bytesRead = stream.Read(responseBytes, 0, responseBytes.Length)
    let response = System.Text.Encoding.UTF8.GetString(responseBytes, 0, bytesRead)

    // Verificar la respuesta del servidor
    if response = "SongExists" then
        Console.WriteLine("La canción existe y se agregará al playlist: " + playlistName)

        // Verificar si el playlist ya existe en el cliente
        if playlist.ContainsKey(playlistName) then
            // Agregar la canción al playlist existente
            let songsInPlaylist = playlist.[playlistName]
            songsInPlaylist.Add(songName)
        else
            // Crear un nuevo playlist y agregar la canción
            let newPlaylist = List([songName]) // Utiliza List para crear una lista mutable
            playlist.Add(playlistName, newPlaylist)

        Console.WriteLine("Canción agregada al playlist.")

    elif response = "SongNotFound" then
        Console.WriteLine("La canción no existe en la base de datos.")
    else
        Console.WriteLine("Respuesta desconocida del servidor: " + response)

let addToPlaylist2 (songName: string) : string=
    // Preguntar al usuario el nombre del playlist
    Console.Write("Ingresa el nombre del playlist en el que deseas agregar la canción: ")
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

    sprintf "Canción '%s' agregada al playlist." songName

// Función para filtrar una playlist por nombre y canciones disponibles (lista genérica)
//let filtrarPlaylist (playlistName: string) (cancionesDisponibles: List<string>) =
//    if playlists.ContainsKey(playlistName) then
//        let originalSongs = playlists.[playlistName]
//        let songsFiltradas = List.filter (fun song -> List.contains song cancionesDisponibles) originalSongs
//        let playlistFiltrada = new Dictionary<string, List<string>>()
//        playlistFiltrada.Add(playlistName, songsFiltradas)
//        playlistFiltrada
//    else
//        new Dictionary<string, List<string>>()

let removeFromPlaylist () =
    // Preguntar al usuario el nombre del playlist
    Console.Write("Ingresa el nombre del playlist del que deseas eliminar la canción: ")
    let playlistName = Console.ReadLine()

    // Verificar si el playlist existe
    if playlist.ContainsKey(playlistName) then
        // Preguntar al usuario el nombre de la canción que desea eliminar
        Console.Write("Ingresa el nombre de la canción que deseas eliminar: ")
        let songName = Console.ReadLine()

        // Verificar si la canción está en el playlist
        let songsInPlaylist = playlist.[playlistName]
        if songsInPlaylist.Contains(songName) then
            // Eliminar la canción del playlist
            songsInPlaylist.Remove(songName)
            Console.WriteLine("Canción eliminada del playlist.")
        else
            Console.WriteLine("La canción no está en el playlist.")
    else
        Console.WriteLine("El playlist no existe.")


let imprimirPlaylist (playlists: Dictionary<string, List<string>>) =
    printfn "Playlists:"
    for kvp in playlists do
        let playlistName, songs = kvp.Key, kvp.Value
        printfn "Playlist: %s" playlistName
        printfn "Canciones:"
        for song in songs do
            printfn "  %s" song


let rec receiveSongList (songListBuilder: StringBuilder) (stream: NetworkStream) =
    let buffer = Array.zeroCreate 1024
    let bytesRead = stream.Read(buffer, 0, buffer.Length)
    if bytesRead > 0 then
        let songInfo = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead)
        songListBuilder.Append(songInfo)
        receiveSongList songListBuilder stream
    else
        ()
 
let choosePlaylist () =
    Console.WriteLine("Playlists disponibles:")
    for playlistName in playlists.Keys do
        Console.WriteLine(playlistName)
    Console.Write("Ingresa el nombre de la playlist: ")
    let selectedPlaylist = Console.ReadLine()
    if playlists.ContainsKey(selectedPlaylist) then
        Some playlists.[selectedPlaylist]
    else
        None

let playSong2 (stream: NetworkStream) =
    match choosePlaylist() with // en el momento que devuelve se cae 
    | Some playlist ->
        for songName in playlist do
        printfn "  - %s" songName
        for songName in playlist do
            printfn "Nombre de la canción: %s" songName

            Console.WriteLine($"Reproduciendo {songName}...")

            // Enviar el nombre de la canción al servidor
            let songNameBytes = System.Text.Encoding.UTF8.GetBytes(songName)
            stream.Write(songNameBytes, 0, songNameBytes.Length)

            use memoryStream = new MemoryStream()
            stream.CopyTo(memoryStream)
            memoryStream.Seek(0L, SeekOrigin.Begin)

            // Crear un archivo temporal para escribir los bytes recibidos
            let tempFilePath = Path.GetTempFileName() + ".mp3"
            use fileStream = new FileStream(tempFilePath, FileMode.Create)
            memoryStream.WriteTo(fileStream)

            use mp3Reader = new Mp3FileReader(memoryStream)
            use outputDevice = new WaveOutEvent()
    
            outputDevice.Init(mp3Reader)
            outputDevice.Play()
            Console.WriteLine("Reproduciendo la canción...")
    
            let audioFileLength = mp3Reader.TotalTime.TotalSeconds
            let mutable currentPosition = 0.0
            let mutable isPlaying = true
            // Opciones para la canción
            while isPlaying do
                Console.WriteLine("Opciones de la canción:")
                Console.WriteLine("f. Adelantar canción")
                Console.WriteLine("r. Retroceder canción")
                Console.WriteLine("p. Pausar/Reanudar canción")
                Console.WriteLine("q. Volver al menú principal")
                Console.Write("Selecciona una opción: ")

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
                        isPlaying <- false
                    else
                        outputDevice.Play()
                        Console.WriteLine("Reanudando la canción.")
                        isPlaying <- true

                | "q" -> // El cliente quiere volver al menú principal
                    Console.WriteLine("Volviendo al menú principal...")
                    isPlaying <- false

                | _ -> 
                    Console.WriteLine("Opción no válida")

            // Eliminar el archivo temporal después de reproducir la canción
            File.Delete(tempFilePath)
            System.Threading.Thread.Sleep(2000) // 2 segundos de pausa entre canciones
    | None ->
        Console.WriteLine("Playlist no encontrada.")


let playSong (stream: NetworkStream) = //version buena 1.0
    Console.Write("Ingresa el nombre de la canción: ")
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
    Console.WriteLine("Reproduciendo la canción...")
    
    let audioFileLength = mp3Reader.TotalTime.TotalSeconds
    let mutable currentPosition = 0.0
    let mutable isPlaying = true

    try
        // Opciones para la canción
        while isPlaying do
            Console.WriteLine("Opciones de la canción:")
            Console.WriteLine("f. Adelantar canción")
            Console.WriteLine("r. Retroceder canción")
            Console.WriteLine("p. Pausar/Reanudar canción")
            Console.WriteLine("q. Volver al menú principal")
            Console.Write("Selecciona una opción: ")

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
                    isPlaying <- false
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


    Console.WriteLine("Canción terminada.")

let main =
    try
        use client = new TcpClient(serverAddr, port)
        use stream = client.GetStream()

        // Bucle principal para mantener al cliente esperando nuevas solicitudes
        let mutable continueRunning = true
        while continueRunning do
            use client = new TcpClient(serverAddr, port)
            use stream = client.GetStream()
            Console.WriteLine("a. Reproducir Musica")
            Console.WriteLine("b. Criterios de Busqueda")
            Console.WriteLine("c. Crear Playlist")
            Console.WriteLine("q. Salir")
            Console.Write("Selecciona una opción: ")

            // Recibir una letra desde la consola
            let letter = Console.ReadLine()

            // Enviar la letra al servidor
            let letterBytes = System.Text.Encoding.UTF8.GetBytes(letter)
            stream.Write(letterBytes, 0, letterBytes.Length)

            match letter with
            | "a" -> // El cliente quiere reproducir una canción
                // Recibir los datos de la canción del servidor
                Console.WriteLine("a. Reproducir una cancion")
                Console.WriteLine("b. Reproducir un playlist")
                Console.Write("Selecciona una opción: ")

                // Recibir una letra desde la consola
                let letter3 = Console.ReadLine()

                // Enviar la letra al servidor
                let letterBytes = System.Text.Encoding.UTF8.GetBytes(letter3)
                stream.Write(letterBytes, 0, letterBytes.Length)

                match letter3 with
                | "a" -> 
                    playSong stream
                    client.Close()
                | "b" ->    
                    createPlaylist ()
                    playSong2 stream 
                | _ ->  ()

            | "b" -> // El cliente quiere la lista de canciones
                Console.WriteLine("a. Canciones en Espannol")
                Console.WriteLine("b. Canciones menores a 4 minutos")
                Console.WriteLine("c. Canciones que empiecen con la letra H")
                Console.Write("Selecciona una opción: ")
                
                // Recibir una letra desde la consola
                let letter2 = Console.ReadLine()

                // Enviar la letra al servidor
                let letterBytes = System.Text.Encoding.UTF8.GetBytes(letter2)
                stream.Write(letterBytes, 0, letterBytes.Length)

                match letter2 with
                | "a" | "b" | "c" -> 
                    let songListBuilder = new StringBuilder()
                    receiveSongList songListBuilder stream
                    let songList = songListBuilder.ToString()
                    Console.WriteLine("Lista de Canciones:")
                    Console.WriteLine(songList)

                    Console.WriteLine("Seleccione una canción para agregar al playlist:")
                    let selectedSong = Console.ReadLine()

                    // Verificar si la canción seleccionada existe en la lista de canciones disponibles
                    if songList.Contains(selectedSong) then
                        let result = addToPlaylist2 selectedSong
                        Console.WriteLine(result)
                    else
                        Console.WriteLine("La canción seleccionada no está en la lista de canciones disponibles.")

                    imprimirPlaylist playlist
                | _ ->  ()

            | "c" -> 
                    Console.WriteLine("a. Crear una playlist")
                    Console.WriteLine("b. Eliminar la cancion de una playlist")
                    Console.WriteLine("c. Actualizar Playlist")
                    Console.Write("Selecciona una opción: ")
                    
                    // Recibir una letra desde la consola
                    let letter2 = Console.ReadLine()

                    // Enviar la letra al servidor
                    let letterBytes = System.Text.Encoding.UTF8.GetBytes(letter2)
                    stream.Write(letterBytes, 0, letterBytes.Length)
                    
                    match letter2 with
                        | "a" -> 
                            createPlaylist ()
                        | "b" -> 
                            removeFromPlaylist ()
                        | "c" -> 
                            createPlaylist ()
                            // aqui va la funcion de filter
                            //let result = addToPlaylist2 "Querida"
                            //imprimirPlaylist playlist

                            //let songListBuilder = new StringBuilder()
                            //receiveSongList songListBuilder stream
                            //let cancionesDisponiblesEnElServidor = songListBuilder.ToString()

                            //Console.Write("Ingresa el nombre de la playlist: ")
                            //let actualizarPlaylist = Console.ReadLine()

                            //let filtrarPlaylist = actualizarPlaylist cancionesDisponiblesEnElServidor
                            //imprimirPlaylist playlist
                        | _ ->  ()
            
            | "q" -> // El cliente quiere salir
                continueRunning <- false
            | _ -> ()
        client.Close()

    with
        | ex -> 
            printfn "Error: %s" ex.Message