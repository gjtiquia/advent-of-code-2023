namespace AdventOfCode.Day4;

public class Card
{
    public int ID { get; private set; }
    public int[] WinningNumbers { get; private set; }
    public int[] CardNumbers { get; private set; }

    public Card(string line)
    {
        string[] splitLines = Utilities.SplitAndTrim(":", line);
        string leftLine = splitLines[0];
        string rightLine = splitLines[1];

        ID = GetIDFromLeftLine(leftLine);
        (WinningNumbers, CardNumbers) = GetNumberArraysFromRightLine(rightLine);
    }

    public int GetPointValue()
    {
        int winningNumberCount = 0;
        foreach (int cardNumber in CardNumbers)
        {
            if (WinningNumbers.Contains(cardNumber))
            {
                winningNumberCount++;
            }
        }

        if (winningNumberCount == 0)
            return 0;

        return (int)Math.Pow(2, winningNumberCount - 1);
    }

    private static int GetIDFromLeftLine(string leftLine)
    {
        string[] splitLines = Utilities.SplitAndTrim(" ", leftLine);
        return int.Parse(splitLines.Last());
    }

    private static (int[] winningNumbers, int[] cardNumbers) GetNumberArraysFromRightLine(string rightLine)
    {
        string[] splitLines = Utilities.SplitAndTrim("|", rightLine);
        string winningNumbersLine = splitLines[0];
        string cardNumbersLine = splitLines[1];

        int[] winningNumbers = GetNumberArrayFromLine(winningNumbersLine);
        int[] cardNumbers = GetNumberArrayFromLine(cardNumbersLine);

        return (winningNumbers, cardNumbers);
    }

    private static int[] GetNumberArrayFromLine(string line)
    {
        string[] numbers = Utilities.SplitAndTrim(" ", line);

        List<int> numberList = [];
        foreach (string number in numbers)
        {
            bool canParse = int.TryParse(number, out int result);
            if (canParse)
                numberList.Add(result);
        }

        return numberList.ToArray();
    }
}