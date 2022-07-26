open System
open FSharp.Control
open FsToolkit.ErrorHandling
open ReadEvents
open Domain

// let horizon = DateTime.Now

let doTheThing (thing: DateTime) =
    async {
        // Get all the events to a specified date
        let! allEvents = readEventsBackwards "esdb://admin:changeit@localhost:2113?tls=false" (thing)

        // Map over the events and de-serialize them to a dto and then their domain.
        let events =
            allEvents
            |> List.map toDto
            |> List.choose id
            |> List.map toDomain

        events |> List.iter (fun e ->
            printfn $"{e}")

        return events
    }


let printTotalFileBytesUsingAsync (path: DateTime) =
    async {
        let! events = doTheThing(path)
        printfn $"events length: {events |> List.length}"
        return ()
    }

let parseDate (date: string) =
    let success, date = DateTime.TryParse(date)
    match success, date with
    | true, _ -> Some date
    | _, _ -> None

let getHorizon argv =
    if Array.length argv = 1 then
        let horizon = parseDate argv[0]

        horizon
    else
        // If no horizon is specified, use the current time
        Some DateTime.Now


[<EntryPoint>]
let main argv =
    let date = getHorizon argv
    
    // when you have a function that returns unit (doesn't return anything)
    // generally the style is to say do callTheFunction
    do match date with
        | Some date ->
            printTotalFileBytesUsingAsync date
            |> Async.RunSynchronously
        | None ->
            printfn "Your date sucks lol git gud"
    0