namespace Project.Core;

public class Day1Parser
{
    public static int GetSumOfCalibrationValue(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            sum += GetCalibrationValue(line);
        }
        return sum;
    }

    public static int GetCalibrationValue(string line)
    {
        int firstDigit = GetFirstDigit(line);
        int lastDigit = GetLastDigit(line);

        return 10 * firstDigit + lastDigit;
    }

    public static int GetFirstDigit(string line)
    {
        bool hasWordDigit = false;
        int wordIndex = -1;
        int wordDigit = -1;
        foreach (var (word, digit) in _spelledOutNumbers)
        {
            int index = line.IndexOf(word);
            if (index == -1) continue;

            if (!hasWordDigit)
            {
                hasWordDigit = true;
                wordIndex = index;
                wordDigit = digit;
            }
            else if (index < wordIndex)
            {
                wordIndex = index;
                wordDigit = digit;
            }
        }

        bool hasNumericDigit = false;
        int numericIndex = -1;
        int numericDigit = -1;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (!char.IsDigit(c)) continue;

            hasNumericDigit = true;
            numericIndex = i;
            numericDigit = (int)char.GetNumericValue(c);

            break;
        }

        if (!hasWordDigit && !hasNumericDigit)
            throw new Exception("No digits in input string!");

        if (hasWordDigit && !hasNumericDigit)
            return wordDigit;

        if (!hasWordDigit && hasNumericDigit)
            return numericDigit;

        if (wordIndex < numericIndex)
            return wordDigit;

        return numericDigit;
    }

    public static int GetLastDigit(string line)
    {
        bool hasWordDigit = false;
        int wordIndex = -1;
        int wordDigit = -1;
        foreach (var (word, digit) in _spelledOutNumbers)
        {
            int index = line.IndexOf(word);
            if (index == -1) continue;

            if (!hasWordDigit)
            {
                hasWordDigit = true;
                wordIndex = index;
                wordDigit = digit;
            }
            else if (index > wordIndex)
            {
                wordIndex = index;
                wordDigit = digit;
            }
        }

        bool hasNumericDigit = false;
        int numericIndex = -1;
        int numericDigit = -1;
        for (int i = line.Length - 1; i >= 0; i--)
        {
            char c = line[i];
            if (!char.IsDigit(c)) continue;

            hasNumericDigit = true;
            numericIndex = i;
            numericDigit = (int)char.GetNumericValue(c);

            break;
        }

        if (!hasWordDigit && !hasNumericDigit)
            throw new Exception("No digits in input string!");

        if (hasWordDigit && !hasNumericDigit)
            return wordDigit;

        if (!hasWordDigit && hasNumericDigit)
            return numericDigit;

        if (wordIndex > numericIndex)
            return wordDigit;

        return numericDigit;
    }

    private static readonly Dictionary<string, int> _spelledOutNumbers = new()
    {
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };
}