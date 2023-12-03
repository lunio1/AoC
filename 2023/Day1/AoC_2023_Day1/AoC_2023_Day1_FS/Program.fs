open System
open System.IO

open System
open System.IO

let input = File.ReadLines "../../../DayTwo/input.txt"

let a = "axc"

let result = input
           |> Seq.map(fun input -> input.Remove(0, 5) )
           |> 

printfn "%A" result
