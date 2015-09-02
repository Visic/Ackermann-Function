open System
open System.Diagnostics

type ResultOrContinuation<'T> =
    | Result of 'T
    | Continuation of (unit -> ResultOrContinuation<'T>)

type ContinuationMonad<'T>(value:ResultOrContinuation<'T>) =
    member x.Value with get() = value
    member x.Apply (func:'T->'T) =
        match x.Value with
        | Result(r) -> ContinuationMonad (ResultOrContinuation.Result (func r))
        | Continuation(c) -> ContinuationMonad (ResultOrContinuation.Continuation c)
        

[<EntryPoint>]
let main argv = 
//    let rec getResult (x:ResultOrContinuation<'T>) =
//        match x with
//        | Result(r) -> r
//        | Continuation(c) -> getResult (c())

    //A function which allows you to "step through" some algorithm
    //  e.g. it allows you to do something before/after each step in the algorithm
    let stepThrough (work:unit->ResultOrContinuation<'T>) (beforeStep:uint64->unit) =
        let rec step (work:unit->ResultOrContinuation<'T>) (stepNumber:uint64) =
            beforeStep stepNumber
            match work() with
            | Result(r) -> r
            | Continuation(c) -> step c (stepNumber + 1UL)
        step work 1UL

    let reportStepNumber (threshold:uint64) (stepNumber:uint64) =
        if stepNumber % threshold = 0UL then printfn "%s- Step: %A" (DateTime.Now.ToString("H:mm:ss:fff")) stepNumber

//    let rec work x =
//        match x with
//        | 0 -> ResultOrContinuation<int>.Result 0
//        | _ -> ResultOrContinuation<int>.Continuation (fun () -> (work (x - 1)))
//
//    (stepThrough (fun () -> (work 2147483647)) (reportStepNumber 250000000UL)) |> printfn "Result: %A"

    let ackermann (m:int64) (n:int64) =
        let rec ack (m:int64) (n:ResultOrContinuation<int64>) =
            match m, n with
            | 0L, n -> ResultOrContinuation.Result (n + 1L)
            | m, 0L -> ResultOrContinuation.Continuation (fun () -> ack (m - 1L) 1L)
            | m, n -> ResultOrContinuation.Continuation (fun () -> (ack (m - 1L) (ack m (n - 1L))))
        ack m n

    0 // return an integer exit code
