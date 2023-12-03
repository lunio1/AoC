open System
open System.IO

let input = File.ReadLines "../../../DayTwo/input.txt"

let colors = [|"red"; "green"; "blue";|]

let findDigit value = value |> Seq.find(fun (s : char) -> Char.IsDigit(s)) |> Char.ToString

let getFirstSemicolonIndex (value : string) = value |> Seq.findIndex(fun (s : char) -> Char.ToString(s).Equals(";")) |> int

let getFirstValue (input : string) = input.Remove(0, 3) |> findDigit

let removeFirstThreeChars (input : string) = input.Remove(0, 2)

let removeCharsFromIndex (input : string) (index : int) = input.Remove(index)

let removeFromFirstSemicolonIndex value = getFirstSemicolonIndex >> removeCharsFromIndex(value)

let getFirstSubGameString = removeFirstThreeChars >> removeFromFirstSemicolonIndex

let result = input
           |> Seq.map(fun input -> input.Remove(0, 5) )
           |> Seq.fold(fun a s -> (a) + getFirstSubGameString(s) ) "aw"

printfn "%A" result