module DayFourPartOne

// ---------------------------
// Does not work yet!
// ---------------------------

let input = "....XXMAS.
.SAMXMS...
...S..A...
..A.A.MS.X
XMASAMX.MM
X.....XA.A
S.S.S.S.SS
.A.A.A.A.A
..M.M.M.MM
.X.X.XMASX"

let horizontalLines = input.Split "\n"

let indexedInput = horizontalLines |> Seq.indexed |> Seq.map(fun (s, a) -> a |> Seq.indexed |> Seq.map(fun (b, c) -> (b, s, c))) |> Seq.concat

let zeroLine = indexedInput |> Seq.filter(fun (x,y,v) -> y = 0)

let verticalLines = zeroLine |> Seq.indexed |> Seq.map(fun (i, s) -> indexedInput |> Seq.filter(fun (x, y, z) -> i = x))

let height = horizontalLines.Length
let width = (Seq.toArray verticalLines).Length

let x = (height, width)

// ---------------------------
// Does not work yet!
// ---------------------------
let getDiagonalCoords(xCoordiante : int, yCoordiante : int) =
    // Formula: Coord height - y coordinate = amount of result coordinates
    let iterations = height - yCoordiante
    let seq1 = seq { for i in 0 .. iterations -> i }
    printfn "%A" iterations
    printfn "%A" seq1

    let mutable result = []
    let mutable iteration = 0

    for coord in seq1 do
        let yCoord = coord
        let xCoord = iteration
        result <- result @ [(xCoord, yCoord)]
        iteration <- iteration + 1

    result

let xy = getDiagonalCoords(0, 2)