namespace Project.Core;

public class Day1Parser
{
    public static int GetCalibrationValue(string line)
    {
        int firstDigit = GetFirstDigit(line);
        int lastDigit = GetLastDigit(line);

        return 10 * firstDigit + lastDigit;
    }

    public static int GetFirstDigit(string line)
    {
        foreach (char c in line)
        {
            if (char.IsDigit(c))
            {
                return (int)char.GetNumericValue(c);
            }
        }

        throw new Exception("No digits in input string!");
    }

    public static int GetLastDigit(string line)
    {
        for (int i = line.Length - 1; i >= 0; i--)
        {
            char c = line[i];
            if (char.IsDigit(c))
            {
                return (int)char.GetNumericValue(c);
            }
        }

        throw new Exception("No digits in input string!");
    }
}