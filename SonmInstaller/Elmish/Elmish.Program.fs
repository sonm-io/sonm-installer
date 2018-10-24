(**
Program
---------
Core abstractions for creating and running the dispatch loop.

*)

namespace Elmish

/// Program type captures various aspects of program behavior
type Program<'arg, 'model, 'msg, 'view> = {
    init      : 'arg -> 'model * Cmd<'msg>
    update    : 'model -> 'msg -> 'model * Cmd<'msg> 
    subscribe : 'model -> Cmd<'msg>
    view      : 'model option -> 'model -> Dispatch<'msg> -> 'msg option -> 'view
    setState  : 'model option -> 'model -> Dispatch<'msg> -> 'msg option -> unit
    onError   : (string*exn) -> unit
}

/// Program module - functions to manipulate program instances
[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Program =
    /// Typical program, new commands are produced by `init` and `update` along with the new state.
    let mkProgram 
        (init : 'arg -> 'model * Cmd<'msg>) 
        (update : 'model -> 'msg -> 'model * Cmd<'msg>)
        (view : 'model option -> 'model -> Dispatch<'msg> -> 'msg option -> 'view) =
        { init = init
          update = update
          view = view
          setState = fun prev next d m -> view prev next d m |> ignore
          subscribe = fun _ -> Cmd.none
          onError = Log.onError }

    /// Simple program that produces only new state with `init` and `update`.
    let mkSimple 
        (init : 'arg -> 'model) 
        (update : 'model -> 'msg -> 'model)
        (view : 'model option -> 'model -> Dispatch<'msg> -> 'msg option -> 'view) =
        { init = init >> fun state -> state, Cmd.none
          update = fun model msg -> update model msg |> fun s -> s, Cmd.none
          view = view
          setState = fun prev next d m -> view prev next d m |> ignore
          subscribe = fun _ -> Cmd.none
          onError = Log.onError }

    /// Subscribe to external source of events.
    /// The subscription is called once - with the initial model, but can dispatch new messages at any time.
    let withSubscription (subscribe : 'model -> Cmd<'msg>) (program: Program<'arg, 'model, 'msg, 'view>) =
        let sub model =
            Cmd.batch [ program.subscribe model
                        subscribe model ]
        { program with subscribe = sub }

    /// Trace all the updates to the console
    let withConsoleTrace (program: Program<'arg, 'model, 'msg, 'view>) =
        let traceInit (arg:'arg) =
            let initModel, cmd = program.init arg
            Log.toConsole ("Initial state:", initModel)
            initModel, cmd

        let traceUpdate state msg =
            Log.toConsole ("New message:", msg)
            let newModel, cmd = program.update state msg
            Log.toConsole ("Updated state:", newModel)
            newModel,cmd

        { program with
            init = traceInit 
            update = traceUpdate }

    /// Trace all the messages as they update the model
    let withTrace trace (program: Program<'arg, 'model, 'msg, 'view>) =
        { program
            with update = fun msg model -> trace msg model; program.update msg model}

    /// Handle dispatch loop exceptions
    let withErrorHandler onError (program: Program<'arg, 'model, 'msg, 'view>) =
        { program
            with onError = onError }

    /// Start the program loop.
    /// arg: argument to pass to the init() function.
    /// program: program created with 'mkSimple' or 'mkProgram'.
    let runWith (arg: 'arg) (program: Program<'arg, 'model, 'msg, 'view>) =
        let (model,cmd) = program.init arg
        let inbox = MailboxProcessor.Start(fun (mb:MailboxProcessor<'msg>) ->
            let rec loop (state:'model) =
                async {
                    let! msg = mb.Receive()
                    let newState =
                        try
                            let (model', cmd') = program.update state msg
                            program.setState (Some state) model' mb.Post (Some msg)
                            cmd' |> List.iter (fun sub -> sub mb.Post)
                            model'
                        with ex ->
                            program.onError ("Unable to process a message:", ex)
                            state
                    return! loop newState
                }
            loop model
        )
        program.setState None model inbox.Post None
        let sub = 
            try 
                program.subscribe model 
            with ex ->
                program.onError ("Unable to subscribe:", ex)
                Cmd.none
        sub @ cmd |> List.iter (fun sub -> sub inbox.Post)

    /// Start the dispatch loop with `unit` for the init() function.
    let run (program: Program<unit, 'model, 'msg, 'view>) = runWith () program
