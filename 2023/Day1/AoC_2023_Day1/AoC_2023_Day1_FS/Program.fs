open System
open System.IO

let input = File.ReadLines "../../../input.txt"

let line = "heightseven4two5"

let reverseString (input : string) = new String(input |> Seq.toArray |> Array.rev)

let findDigit value = value |> Seq.find(fun (s : char) -> Char.IsDigit(s)) |> Char.ToString

let findLastDigit = reverseString >> findDigit

let concatStrings (firstString : string) (secondString : string) = firstString + secondString

let result = input |> Seq.fold(fun x y -> x + (findDigit(y) + findLastDigit(y) |> int)) 0

printfn "%A" result