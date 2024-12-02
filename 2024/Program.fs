open System.IO

let input = File.ReadLines "./fortnite.txt" |> Seq.map(fun s -> s.Split " ") |> Seq.map(fun s -> s |> Array.map(fun s -> int s))

let diff(input : int array) : bool =
    let mutable iteration = 1
    let mutable higher = false
    let mutable lower = false
    let mutable equal = false
    let mutable tooBigDifference = false

    for i in input do
        if iteration <= Array.length input then
            try
                let nextNumber = input[iteration]
                iteration <- iteration + 1
                if i > nextNumber then
                    higher <- true
                    if i - nextNumber > 3 then
                        tooBigDifference <- true
                else if i < nextNumber then
                    lower <- true
                    if nextNumber - i > 3 then
                        tooBigDifference <- true
                else if i = nextNumber then
                    equal <- true
            with ex ->
                printfn "ex"

    printfn "Higher %b" higher
    printfn "Lower %b" lower
    printfn "Equal %b" equal

    if higher && lower && equal && tooBigDifference then
        false
    else if higher && lower && tooBigDifference then
        false
    else if higher && equal && tooBigDifference then    
        false
    else if lower && equal && tooBigDifference then
        false
    else if higher && tooBigDifference then 
        false
    else if higher && lower then
        false
    else if higher && equal then
        false
    else if tooBigDifference && lower then
        false
    else if tooBigDifference && equal then
        false
    else if equal && lower then
        false
    else
        true
    

let result = input |> Seq.map(fun s -> diff(s)) |> Seq.filter(fun s -> s) |> Seq.length
