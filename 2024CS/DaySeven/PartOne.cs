using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _2024CS.DaySeven;

internal class PartOne
{
  public void Main()
  {

    string[] input = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20".Replace("\r", "").Split('\n');

    //string[] input = File.ReadAllText("./input.txt").Replace("\r", "").Split('\n');

    int result = 0;

    foreach (var line in input)
    {
      bool isValid = false;
      Regex findNumbers = new Regex(@"\d+");
      List<int> numbers = findNumbers.Matches(line).Select(x => Convert.ToInt32(x.Value)).ToList();
      int expectedNumber = numbers[0];
      numbers.RemoveAt(0);
      List<int> newNumbers = new List<int>();


      for (int i = 0; i < numbers.Count; i++)
      {
        newNumbers = newNumbers.Append(numbers[i]).ToList();
        newNumbers = newNumbers.Append(-1).ToList();
      }

      int floatingPoint = 1;

      for (int i = -1; i < newNumbers.Count; i++)
      {
        if (i % 2 == 0)
          continue;

        if (i != -1)
        {
          newNumbers[i] = -2;
        }

        for (int ii = 0; ii < newNumbers.Count; ii++)
        {
          if (ii % 2 == 0)
            continue;

          int element = newNumbers[ii];

          if (element == -2 && ii % 2 == 0)
            continue;

          if (ii <= floatingPoint)
            continue;

          newNumbers[ii] = -2;

          newNumbers.ForEach(x => Console.Write(x.ToString() + " "));
          Console.WriteLine();

          int r = Calc(newNumbers);

          newNumbers[ii] = -1;

          if (ii == newNumbers.Count - 1)
            floatingPoint += 2;
        }
      }

      newNumbers = new List<int>();

      for (int i = 0; i < numbers.Count; i++)
      {
        newNumbers = newNumbers.Append(numbers[i]).ToList();
        newNumbers = newNumbers.Append(-1).ToList();
      }

      floatingPoint = newNumbers.Count - 1;

      for (int i = newNumbers.Count + 1; i > 0; i--)
      {
        if (i % 2 == 0)
          continue;

        if (i != newNumbers.Count + 1)
        {
          newNumbers[i] = -2;
        }

        for (int ii = newNumbers.Count; ii > 0; ii--)
        {
          if (ii % 2 == 0)
            continue;

          int element = newNumbers[ii];

          if (element == -2 && ii % 2 == 0)
            continue;

          if (ii >= floatingPoint)
            continue;

          newNumbers[ii] = -2;

          newNumbers.ForEach(x => Console.Write(x.ToString() + " "));
          Console.WriteLine();

          int r = Calc(newNumbers);

          newNumbers[ii] = -1;

          if (ii == newNumbers.Count - 1)
            floatingPoint -= 2;
        }
      }

      int sum = numbers.Sum(x => x);
      int product = numbers.Aggregate((s, a) => s * a);
    }

    int Calc(List<int> nums)
    {
      return 0;
    }
  }
}
