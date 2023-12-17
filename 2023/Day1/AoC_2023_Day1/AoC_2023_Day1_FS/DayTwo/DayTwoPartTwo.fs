module DayTwoPartTwo

open System
open System.IO

let input = File.ReadLines "C:\\repos\\AoC\\2023\\Day1\\AoC_2023_Day1\\AoC_2023_Day1_FS\\DayTwo\\input.txt" |> Seq.map(fun s -> s.Trim()) |> List.ofSeq 

let example = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green".Split("\n") |> Array.map (fun s -> s.Trim()) |> List.ofSeq

let parse (line: string) : int =

    let parseHands (subGame : string) =
        let trimmedSubGame = subGame.Trim()
        let hands = trimmedSubGame.Split(",")
        hands

    let findRed (hand : string) =
        let value = hand.Split(" ")[0] |> int
        let color = hand.Split(" ")[1]
        if (color.Contains("red")) then
            value
        else
            0

    let findGreen (hand : string) =
        let value = hand.Split(" ")[0] |> int
        let color = hand.Split(" ")[1]
        if (color.Contains("green")) then
            value
        else
            0
        
    let findBlue (hand : string) =
        let value = hand.Split(" ")[0] |> int
        let color = hand.Split(" ")[1]
        if (color.Contains("blue")) then
            value
        else
            0

    let games = line.Split(":")[1]
    let trimmedGames = games.Trim()
    let subGames = trimmedGames.Split(";")
    let hands = subGames |> Array.map parseHands
    let maxRed = hands |> Array.map(fun s -> s |> Array.map(fun x -> x.Trim() |> findRed) |> Seq.max) |> Seq.max
    let maxBlue = hands |> Array.map(fun s -> s |> Array.map(fun x -> x.Trim() |> findBlue) |> Seq.max) |> Seq.max
    let maxGreen = hands |> Array.map(fun s -> s |> Array.map(fun x -> x.Trim() |> findGreen) |> Seq.max) |> Seq.max
    maxRed * maxBlue * maxGreen

input |> List.fold (fun acc s ->  acc + (parse(s))) 0