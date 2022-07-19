open System
open System.IO
open FSharp.Control
open EventStore.Client
open FsToolkit.ErrorHandling
open System.Text.Json

type MemberEnrollmentDto =
    {
        id: int
        name: string
    }

type Dto =
    | MemberEnrollmentDto of MemberEnrollmentDto

let deserialize<'dto> (e: ResolvedEvent) =
    try
        Some (JsonSerializer.Deserialize<'dto>(utf8Json = e.Event.Data.Span))
    with
    | e ->
    // Log.Error(ex, "Failed to deserialize {@eventType} event {@eventId} {@ex}",
    //           e.Event.EventType,
    //           e.Event.EventNumber.ToString())
    None
// open Saturn

let toDto (e: ResolvedEvent) : Dto option =
    e
    |> deserialize<MemberEnrollmentDto>
    |> Option.map MemberEnrollmentDto

let readStreamBackwards (conn: string) (streamId: string) =
    let settings = EventStoreClientSettings.Create(conn)
    let client = new EventStoreClient(settings)
    client.ReadStreamAsync(
        direction = Direction.Backwards,
        streamName = streamId,
        revision = StreamPosition.End)

let readStream (conn: string) (streamId: string) =
    let settings = EventStoreClientSettings.Create(conn)
    let client = new EventStoreClient(settings)
    
    client.ReadStreamAsync(direction = Direction.Forwards,
                           streamName = streamId,
                           revision = StreamPosition.Start,
                           configureOperationOptions = (fun a ->
                               a.TimeoutAfter <- TimeSpan.FromSeconds(60)))
    |> AsyncSeq.ofAsyncEnum
    |> AsyncSeq.toListAsync

let readEventsBackwards (connectionString: string) (horizon: DateTime) =
    async {
        try
            let stream = readStreamBackwards connectionString "happy-test-two"
            let! events =
                stream
                |> AsyncSeq.ofAsyncEnum
                |> AsyncSeq.takeWhile (fun e -> e.Event.Created >= horizon)
                |> AsyncSeq.toListAsync
            return List.rev events
        with e ->
            // Log.Error(e, "Failed to read from Event Store")
            printfn "Failed to read from Event Store"
            return []
    }
    
let horizon = DateTime.MinValue

let doTheThing (thing: string) =
    async {
        let! allEvents = readEventsBackwards "esdb://admin:changeit@localhost:2113?tls=false" (DateTime.MinValue)

        let events = allEvents |> List.map toDto |> List.choose id
        
        // (fun e ->
        //     e
        //     |> deserialize<MemberEnrollmentDto>

        //     return e
        // )

        printfn $"Events: {allEvents} {events}"

        // return events
    }


// Perform an asynchronous read of a file using 'async'
let printTotalFileBytesUsingAsync (path: string) =
    async {
        if File.Exists(path) then
            let! bytes = File.ReadAllBytesAsync(path) |> Async.AwaitTask
            let fileName = Path.GetFileName(path)
            let! events = doTheThing("Hello")
            printfn $"events: {events}"
            printfn $"File {fileName} has %d{bytes.Length} bytes"
    }

[<EntryPoint>]
let main argv =
    printTotalFileBytesUsingAsync "assets/path-to-file.txt"
    |> Async.RunSynchronously

    Console.Read() |> ignore
    0