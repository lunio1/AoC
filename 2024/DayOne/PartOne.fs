module DayOnePartOne

open System.IO

let input = File.ReadLines "./input.txt" |> Seq.map(fun s -> s.Split "   ")

type valueTuple = {firstValue : int; secondValue : int;}

let numberColumns = input |> Seq.map(fun s -> s |> Array.map(fun s -> int s))

let firstColumn = numberColumns |> Seq.map(fun s -> s[0]) |> Seq.sort
let secondColumn = numberColumns |> Seq.map(fun s -> s[1]) |> Seq.sort

let test = Seq.zip firstColumn secondColumn

let calcDifference (numberOne : int, numberTwo : int) : int =
    if numberOne > numberTwo then
        numberOne - numberTwo
    else
        numberTwo - numberOne

let differences = test |> Seq.map(fun s -> calcDifference(s))

let result = differences |> Seq.sum