module PartTwo

open System.IO

let input = File.ReadLines "./input.txt" |> Seq.map(fun s -> s.Split "   ")

type valueTuple = {firstValue : int; secondValue : int;}

let numberColumns = input |> Seq.map(fun s -> s |> Array.map(fun s -> int s))

let firstColumn = numberColumns |> Seq.map(fun s -> s[0]) |> Seq.sort
let secondColumn = numberColumns |> Seq.map(fun s -> s[1]) |> Seq.sort

let calcDifference (input : int) : int =
      let amount = Seq.filter(fun s -> s = input) secondColumn
      input * Seq.length amount

let result = firstColumn |> Seq.map(fun s -> calcDifference(s)) |> Seq.sum