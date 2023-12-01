namespace Project.Core;

public class Day1Parser
{
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
}