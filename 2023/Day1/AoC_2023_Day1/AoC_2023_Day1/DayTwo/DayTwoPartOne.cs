using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AoC_2023_CS.DayTwo;

internal class DayTwoPartOne
{
    public static void PartOne()
    {
        string[] lines = File.ReadAllLines("../../../DayTwo/input.txt");
        char[] charsToTrim = [','];
        int result = 0;
        Dictionary<string, int> colorNumbers = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        foreach (string line in lines)
        {
            int sum = 0;
            
            string lijsdf = new string(line.TakeWhile(char.IsDigit).ToArray());
            int gameNumber = Convert.ToInt32(line.Remove(0, 5).Split(':').First());
            
            string[] newLines = line.Remove(0, 8).Split(' ');

            for (int i = 0; i < newLines.Length; i++)
            {
                newLines[i] = newLines[i].TrimEnd(charsToTrim);
            }

            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            bool isValidLine = true;
            for (int i = 0; i < newLines.Length; i++)
            {
                if (int.TryParse(newLines[i], out int a))
                {
                    keyValuePairs.Add($"{i + newLines[i + 1]}", a);
                    foreach (KeyValuePair<string, int> keyValuePair in keyValuePairs)
                    {
                        foreach (KeyValuePair<string, int> numberColor in colorNumbers)
                        {
                            bool isColor = keyValuePair.Key.IndexOf($"{i + numberColor.Key}") != -1;
                            if (isColor)
                            {
                                if (keyValuePair.Value > numberColor.Value)
                                {
                                    isValidLine = false;
                                }
                            }
                        }
                    }

                    if (newLines[i++].Contains(';') && !isValidLine)
                    {
                        keyValuePairs.Clear();
                        break;
                    }
                }
            }

            if(isValidLine)
            {
                result = result + gameNumber;
            }

        }
        Console.Write(result);
    }
}
