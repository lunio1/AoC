namespace AoC_2023_CS.DayOne;

internal class part2
{
    public static void PartTwo()
    {
        string[] lines = File.ReadAllLines("../../../DayOne/input.txt");

        string[] numberStrings = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        string[] reverseNumberStrings = ["eno", "owt", "eerht", "ruof", "evif", "xis", "neves", "thgie", "enin"];

        int result = 0;

        foreach (string line in lines)
        {
            string firstString = string.Empty;
            string secondString = string.Empty;

            Dictionary<string, int> keyValuePairsStringOne = new Dictionary<string, int>();
            Dictionary<string, int> keyValuePairsStringTwo = new Dictionary<string, int>();

            // First String
            firstString = new string(line.SkipWhile(c => !char.IsDigit(c))
                                 .Take(1)
                                 .ToArray());

            keyValuePairsStringOne.Add(firstString, line.IndexOf(firstString));

            foreach (string numberString in numberStrings)
            {
                int index = line.IndexOf(numberString);
                if (index != -1)
                    keyValuePairsStringOne.Add(numberString, index);
            }

            firstString = keyValuePairsStringOne.OrderBy(x => x.Value).First().Key;

            // Second String
            char[] reversedLineCharArray = line.ToCharArray();
            Array.Reverse(reversedLineCharArray);
            string reversedLine = new string(reversedLineCharArray);

            secondString = new string(line.Reverse().SkipWhile(c => !char.IsDigit(c))
                                .Take(1)
                                .ToArray());

            keyValuePairsStringTwo.Add(secondString, reversedLine.IndexOf(secondString));

            foreach (string reverseNumberString in reverseNumberStrings)
            {
                int index = reversedLine.IndexOf(reverseNumberString);
                if (index != -1)
                    keyValuePairsStringTwo.Add(reverseNumberString, index);
            }

            secondString = keyValuePairsStringTwo.OrderBy(x => x.Value).First().Key;

            if (firstString.Length > 1)
                firstString = Convert.ToString(ConvertStringToInt(firstString));

            if (secondString.Length > 1)
                secondString = Convert.ToString(ConvertReversedStringToInt(secondString));

            result = result + Convert.ToInt32(firstString + secondString);
        }

        Console.WriteLine(result);
    }

    public static int ConvertReversedStringToInt(string reversedInput)
    {
        switch (reversedInput)
        {
            case "eno":
                return 1;
            case "owt":
                return 2;
            case "eerht":
                return 3;
            case "ruof":
                return 4;
            case "evif":
                return 5;
            case "xis":
                return 6;
            case "neves":
                return 7;
            case "thgie":
                return 8;
            case "enin":
                return 9;
            default:
                return 0;
        }
    }

    public static int ConvertStringToInt(string input)
    {
        switch (input)
        {
            case "one":
                return 1;
            case "two":
                return 2;
            case "three":
                return 3;
            case "four":
                return 4;
            case "five":
                return 5;
            case "six":
                return 6;
            case "seven":
                return 7;
            case "eight":
                return 8;
            case "nine":
                return 9;
            default:
                return 0;
        }
    }
}