module DayThreePartTwo

open System
open System.IO

let input =
    File.ReadLines "C:\\repos\\AoC\\2023\\Day1\\AoC_2023_Day1\\AoC_2023_Day1_FS\\daythree\\Input.txt"
    |> Seq.map (fun s -> s.Trim())
    |> List.ofSeq

type Coordinate = {YPosition : int; XPosition : int; Value : Char}
type NumberRange = {YPosition : int; XMinPosition : int; XMaxPosition : int; Value : int;}

let example =
    @"..*10"
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
    
let coordinates = input |> Seq.indexed |> Seq.map parse |> Seq.concat 
 
let gears = coordinates |> Seq.filter(fun x -> (x.Value = '*'))

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
    else if coordinateTwo.XPosition = coordinateOne.XPosition then
        {YPosition = coordinateOne.YPosition; XMinPosition = coordinateOne.XPosition; XMaxPosition = coordinateTwo.XPosition; Value = (coordinateOne.Value.ToString()|> int;)}
    else
        let valueBetween = numbers |> Seq.tryFind(fun s -> (s.XPosition = coordinateOne.XPosition + 1) && (s.YPosition = coordinateOne.YPosition)) 
        {YPosition = coordinateOne.YPosition; XMinPosition = coordinateOne.XPosition; XMaxPosition = coordinateTwo.XPosition; Value = (coordinateOne.Value.ToString() + valueBetween.Value.Value.ToString() + coordinateTwo.Value.ToString()) |> int;}

let firstDigits = 
    numbers 
    |> Seq.map getFirstDigits 
    |> Seq.filter(fun s -> s.Value <> ' ')

let lastDigits = numbers |> Seq.map getLastDigits |> Seq.filter(fun s -> s.Value <> ' ')
let digitMap = Seq.zip firstDigits lastDigits
let combinedDigits = digitMap |> Seq.map combineDigits |> Seq.cache

let calculateAdjcentValues (value : Coordinate, currentResult : int) =
    let getInlineNumber (value : NumberRange option) =
        match value with
            | Some(value) -> value.Value
            | None -> 0
 
    let numberBehind = combinedDigits |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition) && (s.XMaxPosition = value.XPosition - 1)) |> getInlineNumber
    let numberAfter = combinedDigits |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition) && (s.XMinPosition = value.XPosition + 1)) |> getInlineNumber

    let upperNumber = combinedDigits |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition - 1) && ((s.XMinPosition = value.XPosition && s.XMaxPosition = value.XPosition) || (s.XMinPosition = value.XPosition - 1 && s.XMaxPosition = value.XPosition + 1) || (s.XMinPosition = value.XPosition && s.XMaxPosition = value.XPosition + 1) || (s.XMinPosition = value.XPosition && s.XMaxPosition = value.XPosition + 2) || (s.XMinPosition = value.XPosition - 1 && s.XMaxPosition = value.XPosition) || (s.XMinPosition = value.XPosition - 2 && s.XMaxPosition = value.XPosition))) |> getInlineNumber
    let upperLeftNumber = combinedDigits |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition - 1) && ((s.XMaxPosition = value.XPosition - 1 && s.XMinPosition = value.XPosition - 3) || (s.XMinPosition = value.XPosition - 1 && s.XMaxPosition = value.XPosition - 1) || (s.XMinPosition = value.XPosition - 2 && s.XMaxPosition = value.XPosition - 1))) |> getInlineNumber
    let upperRightNumber = combinedDigits |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition - 1) && ((s.XMinPosition = value.XPosition + 1 && s.XMaxPosition = value.XPosition + 3) || (s.XMinPosition = value.XPosition + 1 && s.XMaxPosition = value.XPosition + 1) || (s.XMinPosition = value.XPosition + 1 && s.XMaxPosition = value.XPosition + 2))) |> getInlineNumber
    
    let lowerNumber = combinedDigits |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition + 1) && ((s.XMinPosition = value.XPosition && s.XMaxPosition = value.XPosition) || (s.XMinPosition = value.XPosition - 1 && s.XMaxPosition = value.XPosition + 1) || (s.XMinPosition = value.XPosition && s.XMaxPosition = value.XPosition + 1) || (s.XMinPosition = value.XPosition && s.XMaxPosition = value.XPosition + 2) || (s.XMinPosition = value.XPosition - 1 && s.XMaxPosition = value.XPosition) || (s.XMinPosition = value.XPosition - 2 && s.XMaxPosition = value.XPosition))) |> getInlineNumber
    let lowerLeftNumber = combinedDigits |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition + 1) && ((s.XMaxPosition = value.XPosition - 1 && s.XMinPosition = value.XPosition - 3) || (s.XMinPosition = value.XPosition - 1 && s.XMaxPosition = value.XPosition - 1) || (s.XMinPosition = value.XPosition - 2 && s.XMaxPosition = value.XPosition - 1))) |> getInlineNumber
    let lowerRightNumber = combinedDigits |> Seq.tryFind(fun s -> (s.YPosition = value.YPosition + 1) && ((s.XMinPosition = value.XPosition + 1 && s.XMaxPosition = value.XPosition + 3) || (s.XMinPosition = value.XPosition + 1 && s.XMaxPosition = value.XPosition + 1) || (s.XMinPosition = value.XPosition + 1 && s.XMaxPosition = value.XPosition + 2))) |> getInlineNumber
    
    let adjcentNumber = [numberBehind; numberAfter; upperNumber; upperLeftNumber; upperRightNumber; lowerNumber; lowerLeftNumber; lowerRightNumber;]

    let adjecntNumbersOverZero = adjcentNumber |> Seq.fold(fun a s -> (if s = 0 then a + 0 else a + 1)) 0

    if adjecntNumbersOverZero = 2 then
        adjcentNumber |> Seq.fold(fun a s -> if s = 0 then a else a * s ) 1
    else
        0

let result = gears |> Seq.fold(fun a s -> (a + calculateAdjcentValues(s, a))) 0

printfn "%A" result