module ReadEvents

open System
open FSharp.Control
open EventStore.Client
open FsToolkit.ErrorHandling
open System.Text.Json
open Dtos

let deserialize<'dto> (e: ResolvedEvent) =
    try
        Some (JsonSerializer.Deserialize<'dto>(utf8Json = e.Event.Data.Span))
    with
    | e ->
        // printfn $"Failed to deserialize {e.Event.EventType} event {e.Event.EventNumber.ToString()}"
        None

let toDto (e: ResolvedEvent) : Dto option =
    match e.Event.EventType with
    | "New Division" ->
        e
        |> deserialize<NewDivisionDto>
        |> Option.map NewDivisionDto
    | "Updated Division" ->
        e
        |> deserialize<UpdatedDivisionDto>
        |> Option.map UpdatedDivisionDto
    | "Deleted Division" ->
        e
        |> deserialize<DeletedDivisionDto>
        |> Option.map DeletedDivisionDto
    | "employee-created" ->
        e
        |> deserialize<EmployeeCreatedDto>
        |> Option.map EmployeeCreatedDto
    | "employee-updated" ->
        e
        |> deserialize<EmployeeUpdatedDto>
        |> Option.map EmployeeUpdatedDto
    | "employee-deleted" ->
        e
        |> deserialize<EmployeeDeletedDto>
        |> Option.map EmployeeDeletedDto
    | "employment-updated" ->
        e
        |> deserialize<EmploymentUpdatedDto>
        |> Option.map EmploymentUpdatedDto
    | "employment-deleted" ->
        e
        |> deserialize<EmploymentDeletedDto>
        |> Option.map EmploymentDeletedDto
    | "employment-terminated" ->
        e
        |> deserialize<EmploymentTerminatedDto>
        |> Option.map EmploymentTerminatedDto
    | "employment-created" ->
        e
        |> deserialize<EmploymentCreatedDto>
        |> Option.map EmploymentCreatedDto
    | "payroll-created" ->
        e
        |> deserialize<PayrollCreatedDto>
        |> Option.map PayrollCreatedDto
    | "payroll-updated" ->
        e
        |> deserialize<PayrollUpdatedDto>
        |> Option.map PayrollUpdatedDto
    |_ ->
        None

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
            let stream = readStreamBackwards connectionString "oh-brother"
            let! events =
                stream
                |> AsyncSeq.ofAsyncEnum
                |> AsyncSeq.takeWhile (fun e -> e.Event.Created <= horizon)
                |> AsyncSeq.toListAsync
            return List.rev events
        with e ->
            printfn "Failed to read from Event Store"
            return []
    }