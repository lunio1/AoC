namespace _2024CS.DaySix;

// Not working!
internal class PartTwo
{
  public static void Main()
  {
    string[] input = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...".Replace("\r", "").Split('\n');

    //string[] input = File.ReadAllText("./input.txt").Replace("\r", "").Split('\n');

    // X, Y, Value
    List<(int, int, string)> values = new List<(int, int, string)>();
    (int, int, Direction) currentPosition = (0, 0, Direction.Up);

    int iteration = 0;
    int result = 0;

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

    void Walk()
    {
      switch (currentPosition.Item3)
      {
        case Direction.Up:
          var newPosition = (currentPosition.Item1, currentPosition.Item2 - 1, currentPosition.Item3);
          var val = values.FirstOrDefault(x => x.Item1 == newPosition.Item1 && x.Item2 == newPosition.Item2);

          if (val.Item3 == "#")
          {
            currentPosition.Item3 = Direction.Right;
          }
          else
          {
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
            currentPosition = newPosition;
          }
          break;
        case Direction.Left:
          newPosition = (currentPosition.Item1 - 1, currentPosition.Item2, currentPosition.Item3);
          val = values.FirstOrDefault(x => x.Item1 == newPosition.Item1 && x.Item2 == newPosition.Item2);

          if (val.Item3 == "#")
          {
            currentPosition.Item3 = Direction.Up;
          }
          else
          {
            currentPosition = newPosition;
          }
          break;
        case Direction.Right:
          newPosition = (currentPosition.Item1 + 1, currentPosition.Item2, currentPosition.Item3);
          val = values.FirstOrDefault(x => x.Item1 == newPosition.Item1 && x.Item2 == newPosition.Item2);

          if (val.Item3 == "#")
          {
            currentPosition.Item3 = Direction.Down;
          }
          else
          {
            currentPosition = newPosition;
          }
          break;
      }
    }

    int maxXValue = values.Max(x => x.Item1);
    int maxYValue = values.Max(x => x.Item2);
    var startPosition = (values.First(x => x.Item3 == "^").Item1, values.First(x => x.Item3 == "^").Item2, Direction.Up);

    for (int i = 0; i < values.Count; i++)
    {
      currentPosition = startPosition;
      var currentValue = values[i];
      if (currentValue.Item3 == "#" || currentValue.Item3 == "^")
        continue;

      currentValue.Item3 = "#";
      values[i] = currentValue;

      List<(int, int, Direction)> previousAttempts = new();
      List<(int, int, Direction)> currentAttempts = new();

      while (currentPosition.Item1 <= maxXValue && currentPosition.Item2 <= maxYValue && currentPosition.Item1 >= 0 && currentPosition.Item2 >= 0)
      {
        Walk();
        currentAttempts.Add(currentPosition);

        if (currentAttempts.Count == 5000)
        {
          if (previousAttempts.SequenceEqual(currentAttempts))
          {
            result++;
            previousAttempts.Clear();
            currentAttempts.Clear();
            break;
          }
          else
          {
            previousAttempts = currentAttempts;
            currentAttempts.Clear();
          }
        }
      }

      currentValue.Item3 = ".";
      values[i] = currentValue;
    }

    Console.WriteLine(result);
  }

  enum Direction
  {
    Up,
    Down,
    Left,
    Right
  }
}
