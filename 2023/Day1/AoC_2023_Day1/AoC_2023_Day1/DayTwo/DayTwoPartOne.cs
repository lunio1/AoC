using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023_CS.DayTwo;

internal class DayTwoPartOne
{

    public static void PartOne()
    {
        string[] lines = File.ReadAllLines("../../../DayTwo/input.txt");
        int result = 0;
        foreach (string line in lines) 
        {
            int sum = 0;
            int gameNumber = Convert.ToInt32(new string(line.SkipWhile(c => !char.IsDigit(c))
                                 .Take(1)
                                 .ToArray()));

            string[] newLines = line.Remove(0, 8).Split(' ');
            foreach (string newLine in newLines )
            {
                int i = 0;
                newLine.Trim(';');
                newLines[i] = newLine.Trim(',');
                i++;
            }
            string numberOne = line[0].ToString();
            string numberTwo = line[1].ToString();
        }
    }
}
