module DayThreePartTwo

open System.IO
open System.Text.RegularExpressions

let input = File.ReadAllText "./Input.txt"

let empty = ""
let removeDontDo(input : string) =
    let re = Regex(@"don't\(\).*?do\(\)", RegexOptions.Singleline)
    re.Replace(input, empty)

let findMuls(input : string) =
    let re = Regex(@"mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Compiled)
    re.Matches input |> Seq.map(fun s -> int s.Groups[1].Value * int s.Groups[2].Value)

let result = removeDontDo input |> findMuls |> Seq.sum