open System
open System.IO

let input =
    File.ReadLines "C:\\repos\\AoC\\2023\\Day1\\AoC_2023_Day1\\AoC_2023_Day1_FS\\daythree\\Input.txt"
    |> Seq.map (fun s -> s.Trim())
    |> List.ofSeq

type Coordinate = {YPosition : int; XPosition : int; Value : Char}
type NumberRange = {YPosition : int; XMinPosition : int; XMaxPosition : int; Value : int;}

let example =
    @"467..114..
                ...*......
                ..35..633.
                ......#...
                617*......
                .....+.58.
                ..592.....
                ......755.
                ...$.*....
                .664.598.."
        .Split("\n")
    |> Array.map (fun s -> s.Trim())
    |> List.ofSeq


let parse value = 
    let parseToCoordinates (value, yPos) =
        let x,v = value
        {YPosition = yPos; XPosition = x; Value = v }

    let y,a = value
    let indexedLine = a |> Seq.indexed
    let coordinates = indexedLine |> Seq.map(fun s -> parseToCoordinates(s, y))
    coordinates
    
let coordinates = example |> Seq.indexed |> Seq.map parse |> Seq.concat

let symbols = coordinates |> Seq.filter(fun x -> (Char.IsPunctuation x.Value || Char.IsSymbol x.Value) && (x.Value <> '.'))

let parseNumbers (value : Coordinate) =
    ()

let numbers = coordinates |> Seq.filter(fun x -> Char.IsDigit x.Value)

let getFirstDigits (value : Coordinate) =
    let charBefore = numbers |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition) && (s.XPosition = value.XPosition - 1))
    match charBefore with
        | Some(value) -> {YPosition = -1; XPosition = -1; Value = ' '}
        | None -> value

let getLastDigits (value : Coordinate) =
    let charBefore = numbers |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition) && (s.XPosition = value.XPosition + 1))
    match charBefore with
        | Some(value) -> {YPosition = -1; XPosition = -1; Value = ' '}
        | None -> value


let combineDigits (coordinateOne : Coordinate, coordinateTwo : Coordinate) =
    if coordinateTwo.XPosition - coordinateOne.XPosition = 1 then
        {YPosition = coordinateOne.YPosition; XMinPosition = coordinateOne.XPosition; XMaxPosition = coordinateTwo.XPosition; Value = (coordinateOne.Value.ToString() + coordinateTwo.Value.ToString()) |> int;}
    else
        let valueBetween = numbers |> Seq.tryFind(fun s -> (s.XPosition = coordinateOne.XPosition + 1) && (s.YPosition = coordinateOne.YPosition)) 
        {YPosition = coordinateOne.YPosition; XMinPosition = coordinateOne.XPosition; XMaxPosition = coordinateTwo.XPosition; Value = (coordinateOne.Value.ToString() + valueBetween.Value.Value.ToString() + coordinateTwo.Value.ToString()) |> int;}

let firstDigits = numbers |> Seq.map getFirstDigits |> Seq.filter(fun s -> s.Value <> ' ')
let lastDigits = numbers |> Seq.map getLastDigits |> Seq.filter(fun s -> s.Value <> ' ')
let digitMap = Seq.zip firstDigits lastDigits
let combinedDigits = digitMap |> Seq.map combineDigits