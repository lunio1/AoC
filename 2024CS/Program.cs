//string[] input = @"............
//........0...
//.....0......
//.......0....
//....0.......
//......A.....
//............
//............
//........A...
//.........A..
//............
//............".Replace("\r", "").Split('\n');

string[] input = File.ReadAllText("./input.txt").Replace("\r", "").Split('\n');


List<(int, int, string)> values = new List<(int, int, string)>();

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

Console.WriteLine();
List<(int, int, string)> result = new List<(int, int, string)> ();

int maxX = values.Max(x => x.Item1);
int maxY = values.Max(x => x.Item2);

foreach (var val in values)
{
  if (val.Item3 == ".")
    continue;

  string symbol = val.Item3;
  List<(int, int, string)> otherCharLocations = [.. values.Where(x => x.Item3 == symbol)];

  foreach (var otherLocation in otherCharLocations)
  {
    if (otherLocation == val)
      continue;

    result.Add(val);

    int xDifference = val.Item1 - otherLocation.Item1;
    int yDifference = val.Item2 - otherLocation.Item2;

    if (val.Item1 + xDifference >= 0 && val.Item1 + xDifference <= maxX)
    {
      if (val.Item2 + yDifference >= 0 && val.Item2 + yDifference <= maxY)
      {
        int newXDifference = xDifference;
        int newYDifference = yDifference;

        while(val.Item1 + newXDifference >= 0 && val.Item1 + newXDifference <= maxX && val.Item2 + newYDifference >= 0 && val.Item2 + newYDifference <= maxY)
        {
          result.Add((val.Item1 + newXDifference, val.Item2 + newYDifference, values.First(x => x.Item1 == val.Item1 + newXDifference && x.Item2  == val.Item2 + newYDifference).Item3));
          newXDifference += xDifference;
          newYDifference += yDifference;
        }
      }
    }

    int secondXDifference = -1 * xDifference;
    int secondYDifference = -1 * yDifference;

    if (otherLocation.Item1 + secondXDifference >= 0 && otherLocation.Item1 + secondXDifference <= maxX)
    {
      if (otherLocation.Item2 + secondYDifference >= 0 && otherLocation.Item2 + secondYDifference <= maxY)
      {
        int newXDifference = secondXDifference;
        int newYDifference = secondYDifference;

        while (otherLocation.Item1 + newXDifference >= 0 && otherLocation.Item1 + newXDifference <= maxX && otherLocation.Item2 + newYDifference >= 0 && otherLocation.Item2 + newYDifference <= maxY)
        {
          result.Add((otherLocation.Item1 + newXDifference, otherLocation.Item2 + newYDifference, values.First(x => x.Item1 == otherLocation.Item1 + newXDifference && x.Item2 == otherLocation.Item2 + newYDifference).Item3));
          newXDifference += secondXDifference;
          newYDifference += secondYDifference;
        }
      }
    }
  }
}
result = result.Distinct().ToList();
Console.Write(result.Distinct().Count());