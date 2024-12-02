open System
open System.IO

let input =
    File.ReadLines "C:\\repos\\AoC\\2023\\Day1\\AoC_2023_Day1\\AoC_2023_Day1_FS\\dayfour\\Input.txt"
    |> Seq.map (fun s -> s.Trim())
    |> List.ofSeq


type card = {gameId: int; winningNumbers : int seq; numbers : int seq; matches : int;}

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
    let splitCard = value.Split(':') |> Array.map(fun s -> s.Split('|')) |> Array.concat
    
    printfn "%A" splitCard[0]

    let gameId = splitCard[0].Split(' ') |> Array.map(fun s -> s.Trim()) |> Array.item 1 |> int
    let winningNumbers = splitCard[1].Split(' ') |> Array.filter(fun s -> s <> "") |> Array.map(fun s -> s |> int) |> Seq.ofArray
    let possibleWinningNumbers = splitCard[2].Split(' ') |> Array.filter(fun s -> s <> "") |> Array.map(fun s -> s |> int) |> Seq.ofArray
    {gameId = gameId; winningNumbers = winningNumbers; numbers = possibleWinningNumbers; matches = 0}

let cards = example |> Seq.map parseCards
printfn "%A" cards

let calculateMatches (card : card ) : card =
    
    let matchNumbers (value : int) =
        card.numbers |> Seq.filter(fun s -> s = value)

    let amount = card.winningNumbers |> Seq.map matchNumbers |> Seq.concat |> List.ofSeq
    {gameId = card.gameId; winningNumbers = card.winningNumbers; numbers = card.numbers; matches = amount.Length}


let parsedCards = cards |> Seq.map calculateMatches

let addMatches (card : card) : card =

    

printfn "%A" parsedCards