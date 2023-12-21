module DayFourPartOne

open System
open System.IO

let input =
    File.ReadLines "C:\\repos\\AoC\\2023\\Day1\\AoC_2023_Day1\\AoC_2023_Day1_FS\\dayfour\\Input.txt"
    |> Seq.map (fun s -> s.Trim())
    |> List.ofSeq


type card = {winningNumbers : int seq; numbers : int seq;}

let example =
    @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        .Split("\n")
    |> Array.map (fun s -> s.Trim())
    |> List.ofSeq




let parseCards (value : string) =
    let cardWithoutGameNumber = value.Split(':') |> Array.removeAt 0 |> Seq.ofArray
    let cardValues = cardWithoutGameNumber |> Seq.map(fun s ->  s.Split('|') |> Seq.ofArray)
    let numbers = cardValues |> Seq.map(fun s -> s |> Seq.map(fun s -> s.Split(' '))) |> Seq.map(fun s -> s |> Seq.map(fun s -> s |> Seq.filter(fun s -> s <> "") |> Seq.map int))
    let winningNumbers = numbers |> Seq.map(fun s -> s |> Seq.head)
    let possibleWinningNumbers = numbers |> Seq.map(fun s -> s |> Seq.last)

    {winningNumbers = Seq.head winningNumbers; numbers = Seq.head possibleWinningNumbers}

let cards = input |> Seq.map parseCards


let calculateResult (card : card ) : int=
    
    let matchNumbers (value : int) =
        card.numbers |> Seq.filter(fun s -> s = value)

    let matches = card.winningNumbers |> Seq.map matchNumbers |> Seq.concat
    let isAnyNumber = matches |> Seq.fold(fun a s -> if s > 0 then a + 1 else 0 )0
    if isAnyNumber >= 1 then
        matches |> Seq.fold(fun a s -> if a = 0 then a + 1 else a * 2)0
    else
        0

let result = cards |> Seq.fold(fun a s -> a + calculateResult s)0

printfn "%A" result