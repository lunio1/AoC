module DayFivePartOne

open System.IO

//let input = "47|53
//97|13
//97|61
//97|47
//75|29
//61|13
//75|53
//29|13
//97|29
//53|29
//61|53
//97|53
//61|29
//47|13
//75|47
//97|75
//47|61
//75|61
//47|29
//75|13
//53|13

//75,47,61,53,29
//97,61,53,29,13
//75,29,13
//75,97,47,61,53
//61,13,29
//97,13,75,29,47"

let input = File.ReadAllText "./input.txt"

let rules = (input.Split("\n\n")[0]).Split("\n") |> Array.map(fun s -> s.Split("|")) |> Array.map(fun s -> s |> Array.map(fun s -> int s))

let games = (input.Split("\n\n")[1]).Split("\n") |> Array.map(fun s -> s.Split(",")) |> Array.map(fun s -> s |> Array.map(fun s -> int s))

let getNumbersAfter(value: int) =
    rules |> Array.filter(fun s -> s[0] = value) |> Array.map(fun s -> s[1])

let getNumbersBefore(value: int) =
    rules |> Array.filter(fun s -> s[1] = value) |> Array.map(fun s -> s[0])

let isNumberCorrect(value: int, index: int, game: int array) : bool =
    let gameIndexed = game |> Array.indexed
    let numbersBefore = gameIndexed |> Array.filter(fun (i, s) -> i < index) |> Array.map(fun (i, s) -> s)
    let numbersAfter = gameIndexed |> Array.filter(fun (i, s) -> i > index) |> Array.map(fun (i, s) -> s)
    let allowedNumbersAfter = getNumbersAfter(value)
    let allowedNumbersBefore = getNumbersBefore(value)

    // I.e. Check if numbers before are contained in allowed numbers after
    let numbersAfterCorrect = numbersAfter |> Seq.map(fun s -> List.contains s (Array.toList allowedNumbersBefore))
    let numbersBeforeCorrect = numbersBefore |> Seq.map(fun s -> List.contains s (Array.toList allowedNumbersAfter))

    printfn "Before: %A" numbersAfterCorrect
    printfn "After: %A" numbersBeforeCorrect

    not (List.contains true (Seq.toList numbersAfterCorrect) || List.contains true (Seq.toList numbersBeforeCorrect))

let isGameValid(game: int array) =
    let result = game |> Array.indexed |> Array.map(fun (i, s) -> isNumberCorrect(s, i, game)) |> Array.contains false
    (not result, game)

let getMiddleValue(input: int array) =
    input[input.Length / 2]

let validatedMatches = games |> Seq.map(fun s -> isGameValid s) |> Seq.filter(fun (b, s) -> b)

let result = validatedMatches |> Seq.map(fun (s, i) -> getMiddleValue i) |> Seq.sum