module DayTwo

open System
open System.IO

let input = File.ReadLines "../../../DayTwo/input.txt"

let colors = [|"red"; "green"; "blue";|]

let findDigit value = value |> Seq.find(fun (s : char) -> Char.IsDigit(s)) |> Char.ToString

let getFirstSemicolonIndex (value : string) = value |> Seq.findIndex(fun (s : char) -> Char.ToString(s).Equals(";")) |> int

let getFirstValue (input : string) = input.Remove(0, 3) |> findDigit

//let getFirstColorValue

let result = input
           |> Seq.removeManyAt 0 5
           |> Seq.fold(fun a s -> a + (getFirstSemicolonIndex(s) |> int) ) 0

printfn "%A" result