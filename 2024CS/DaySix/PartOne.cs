namespace _2024CS.DaySix;

internal class PartOne
{
  public static void Main()
  {
    //string[] input = @"....#.....
    //.........#
    //..........
    //..#.......
    //.......#..
    //..........
    //.#..^.....
    //........#.
    //#.........
    //......#...".Replace("\r", "").Split('\n');

    string[] input = File.ReadAllText("./input.txt").Replace("\r", "").Split('\n');

    // X, Y, Value
    List<(int, int, string)> values = new List<(int, int, string)>();
    List<(int, int)> traveledPositions = new List<(int, int)>();
    (int, int, Direction) currentPosition = (0, 0, Direction.Up);

    int iteration = 0;

    foreach (var line in input)
    {
      if (iteration > input.Length)
        break;

      int x = 0;
      int y = iteration;

      foreach (var character in line)
      {
        if (x > line.Length)
          break;

        values.Add((x, y, character.ToString()));
        x++;
      }

      iteration++;
    }

    currentPosition = (values.First(x => x.Item3 == "^").Item1, values.First(x => x.Item3 == "^").Item2, Direction.Up);
    traveledPositions.Add((currentPosition.Item1, currentPosition.Item2));

    void Walk()
    {
      switch (currentPosition.Item3)
      {
        case Direction.Up:
          var newPosition = (currentPosition.Item1, currentPosition.Item2 - 1, currentPosition.Item3);
          var val = values.First(x => x.Item1 == newPosition.Item1 && x.Item2 == newPosition.Item2);

          if (val.Item3 == "#")
          {
            currentPosition.Item3 = Direction.Right;
          }
          else
          {
            traveledPositions.Add((newPosition.Item1, newPosition.Item2));
            currentPosition = newPosition;
          }
          break;
        case Direction.Down:
          newPosition = (currentPosition.Item1, currentPosition.Item2 + 1, currentPosition.Item3);
          val = values.FirstOrDefault(x => x.Item1 == newPosition.Item1 && x.Item2 == newPosition.Item2);

          if (val.Item3 == "#")
          {
            currentPosition.Item3 = Direction.Left;
          }
          else
          {
            traveledPositions.Add((newPosition.Item1, newPosition.Item2));
            currentPosition = newPosition;
          }
          break;
        case Direction.Left:
          newPosition = (currentPosition.Item1 - 1, currentPosition.Item2, currentPosition.Item3);
          val = values.First(x => x.Item1 == newPosition.Item1 && x.Item2 == newPosition.Item2);

          if (val.Item3 == "#")
          {
            currentPosition.Item3 = Direction.Up;
          }
          else
          {
            traveledPositions.Add((newPosition.Item1, newPosition.Item2));
            currentPosition = newPosition;
          }
          break;
        case Direction.Right:
          newPosition = (currentPosition.Item1 + 1, currentPosition.Item2, currentPosition.Item3);
          val = values.First(x => x.Item1 == newPosition.Item1 && x.Item2 == newPosition.Item2);

          if (val.Item3 == "#")
          {
            currentPosition.Item3 = Direction.Down;
          }
          else
          {
            traveledPositions.Add((newPosition.Item1, newPosition.Item2));
            currentPosition = newPosition;
          }
          break;
      }
    }

    while (currentPosition.Item1 <= values.Max(x => x.Item1) && currentPosition.Item2 <= values.Max(x => x.Item2))
    {
      Walk();
    }
    traveledPositions.Remove(traveledPositions.Last());

    Console.WriteLine(traveledPositions.Distinct().Count());
  }

  enum Direction
  {
    Up,
    Down,
    Left,
    Right
  }
}
