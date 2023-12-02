namespace AoC_2023_Day1;

internal class part1
{
    public static void PartOne()
    {
        string[] lines = File.ReadAllLines("../../../input.txt");

        int result = 0;

        foreach (string line in lines)
        {
            string firstString = string.Empty;
            string secondString = string.Empty;

            firstString = new string(line.SkipWhile(c => !char.IsDigit(c))
                                 .Take(1)
                                 .ToArray());

            secondString = new string(line.Reverse().SkipWhile(c => !char.IsDigit(c))
                                .Take(1)
                                .ToArray());

            result = result + Convert.ToInt32(firstString + secondString);
        }

        Console.WriteLine(result);
    }
}
