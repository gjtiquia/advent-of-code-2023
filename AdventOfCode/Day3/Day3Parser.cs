namespace AdventOfCode.Day3;

public class Day3Parser
{
    public static int FindSumOfAllGearRatios(string[] lines)
    {
        int sum = 0;

        for (int gearRowIndex = 0; gearRowIndex < lines.Length; gearRowIndex++)
        {
            string topRow = gearRowIndex == 0 ? "" : lines[gearRowIndex - 1];
            string currentRow = lines[gearRowIndex];
            string bottomRow = gearRowIndex == lines.Length - 1 ? "" : lines[gearRowIndex + 1];

            int[] gearIndexes = FindIndexesOfGear(currentRow);
            foreach (int gearIndex in gearIndexes)
            {
                bool hasGearRatio = TryFindGearRatioOfGear(topRow, currentRow, bottomRow, gearIndex, out int gearRatio);
                if (!hasGearRatio) continue;

                sum += gearRatio;
            }
        }

        return sum;
    }

    public static bool TryFindGearRatioOfGear(string topRow, string currentRow, string bottomRow, int gearIndex, out int gearRatio)
    {
        int partNumberCount = 0;
        gearRatio = 1;

        List<Dictionary<int, int>> adjacentNumbersToGear = FindAdjacentNumbersOfSymbol(topRow, currentRow, bottomRow, gearIndex);
        foreach (Dictionary<int, int> row in adjacentNumbersToGear)
        {
            foreach (var (index, number) in row)
            {
                partNumberCount++;
                gearRatio *= number;
            }
        }

        // It is only a gear if it has exactly 2 part numbers
        return partNumberCount == 2;
    }

    public static int FindSumOfAllPartNumbers(string[] lines)
    {
        int sum = 0;

        List<Dictionary<int, int>> adjacentNumbers = FindNumbersAdjacentToAllSymbols(lines);
        foreach (Dictionary<int, int> row in adjacentNumbers)
        {
            foreach (var (index, number) in row)
            {
                sum += number;
            }
        }

        return sum;
    }

    public static List<Dictionary<int, int>> FindNumbersAdjacentToAllSymbols(string[] lines)
    {
        List<Dictionary<int, int>> adjacentNumbers = [];
        for (int i = 0; i < lines.Length; i++)
            adjacentNumbers.Add([]);

        for (int symbolRowIndex = 0; symbolRowIndex < lines.Length; symbolRowIndex++)
        {
            string topRow = symbolRowIndex == 0 ? "" : lines[symbolRowIndex - 1];
            string currentRow = lines[symbolRowIndex];
            string bottomRow = symbolRowIndex == lines.Length - 1 ? "" : lines[symbolRowIndex + 1];

            int[] symbolIndexes = FindIndexesOfSymbols(currentRow);
            foreach (int symbolIndex in symbolIndexes)
            {
                List<Dictionary<int, int>> adjacentNumbersToSymbol = FindAdjacentNumbersOfSymbol(topRow, currentRow, bottomRow, symbolIndex);
                for (int relativeRowIndex = 0; relativeRowIndex < 3; relativeRowIndex++)
                {
                    Dictionary<int, int> adjacentNumbersInRow = adjacentNumbersToSymbol[relativeRowIndex];
                    int absoluteRowIndex = -1;

                    // Relative top row
                    if (relativeRowIndex == 0)
                    {
                        // If at top edge, ignore relative top row
                        if (symbolRowIndex == 0) continue;

                        absoluteRowIndex = symbolRowIndex - 1;
                    }

                    // Relative current row
                    else if (relativeRowIndex == 1)
                    {
                        absoluteRowIndex = symbolRowIndex;
                    }

                    // Relative bottom row
                    else if (relativeRowIndex == 2)
                    {
                        // If at bottom edge, ignore relative bottom row
                        if (symbolRowIndex == lines.Length - 1) continue;

                        absoluteRowIndex = symbolRowIndex + 1;
                    }

                    foreach (var (index, number) in adjacentNumbersInRow)
                        adjacentNumbers[absoluteRowIndex].TryAdd(index, number);
                }
            }
        }

        return adjacentNumbers;
    }

    public static List<Dictionary<int, int>> FindAdjacentNumbersOfSymbol(string topRow, string currentRow, string bottomRow, int symbolIndex)
    {
        List<Dictionary<int, int>> adjacentNumbers =
        [
            new Dictionary<int, int>(),
            new Dictionary<int, int>(),
            new Dictionary<int, int>()
        ];

        bool hasTopLeftNumber = TryGetTopLeftNumber(topRow, symbolIndex, out int indexOfTopLeft, out int numberOfTopLeft);
        if (hasTopLeftNumber)
            adjacentNumbers[0].TryAdd(indexOfTopLeft, numberOfTopLeft);

        bool hasTopNumber = TryGetTopNumber(topRow, symbolIndex, out int indexOfTop, out int numberOfTop);
        if (hasTopNumber)
            adjacentNumbers[0].TryAdd(indexOfTop, numberOfTop);

        bool hasTopRightNumber = TryGetTopRightNumber(topRow, symbolIndex, out int indexOfTopRight, out int numberOfTopRight);
        if (hasTopRightNumber)
            adjacentNumbers[0].TryAdd(indexOfTopRight, numberOfTopRight);

        bool hasLeftNumber = TryGetLeftNumber(currentRow, symbolIndex, out int indexOfLeft, out int numberOfLeft);
        if (hasLeftNumber)
            adjacentNumbers[1].TryAdd(indexOfLeft, numberOfLeft);

        bool hasRightNumber = TryGetRightNumber(currentRow, symbolIndex, out int indexOfRight, out int numberOfRight);
        if (hasRightNumber)
            adjacentNumbers[1].TryAdd(indexOfRight, numberOfRight);

        bool hasBottomLeftNumber = TryGetBottomLeftNumber(bottomRow, symbolIndex, out int indexOfBottomLeft, out int numberOfBottomLeft);
        if (hasBottomLeftNumber)
            adjacentNumbers[2].TryAdd(indexOfBottomLeft, numberOfBottomLeft);

        bool hasBottomNumber = TryGetBottomNumber(bottomRow, symbolIndex, out int indexOfBottom, out int numberOfBottom);
        if (hasBottomNumber)
            adjacentNumbers[2].TryAdd(indexOfBottom, numberOfBottom);

        bool hasBottomRightNumber = TryGetBottomRightNumber(bottomRow, symbolIndex, out int indexOfBottomRight, out int numberOfBottomRight);
        if (hasBottomRightNumber)
            adjacentNumbers[2].TryAdd(indexOfBottomRight, numberOfBottomRight);

        return adjacentNumbers;
    }

    private static bool TryGetTopLeftNumber(string topRow, int symbolIndex, out int index, out int number)
    {
        // Default output
        index = -1;
        number = 0;

        // Symbol is at the top edge
        if (string.IsNullOrEmpty(topRow))
            return false;

        // Symbol is at the left edge
        if (symbolIndex == 0)
            return false;

        int indexOfOneOfTheDigits = symbolIndex - 1;
        char topLeftChar = topRow[indexOfOneOfTheDigits];

        if (!char.IsDigit(topLeftChar))
            return false;

        (index, number) = GetIndexAndFullNumber(topRow, indexOfOneOfTheDigits);
        return true;
    }

    private static bool TryGetTopRightNumber(string topRow, int symbolIndex, out int index, out int number)
    {
        // Default output
        index = -1;
        number = 0;

        // Symbol is at the top edge
        if (string.IsNullOrEmpty(topRow))
            return false;

        // Symbol is at the Right edge
        if (symbolIndex == topRow.Length - 1)
            return false;

        int indexOfOneOfTheDigits = symbolIndex + 1;
        char topRightChar = topRow[indexOfOneOfTheDigits];

        if (!char.IsDigit(topRightChar))
            return false;

        (index, number) = GetIndexAndFullNumber(topRow, indexOfOneOfTheDigits);
        return true;
    }

    private static bool TryGetTopNumber(string topRow, int symbolIndex, out int index, out int number)
    {
        // Default output
        index = -1;
        number = 0;

        // Symbol is at the top edge
        if (string.IsNullOrEmpty(topRow))
            return false;

        int indexOfOneOfTheDigits = symbolIndex;
        char topChar = topRow[indexOfOneOfTheDigits];

        if (!char.IsDigit(topChar))
            return false;

        (index, number) = GetIndexAndFullNumber(topRow, indexOfOneOfTheDigits);
        return true;
    }

    private static bool TryGetLeftNumber(string currentRow, int symbolIndex, out int index, out int number)
    {
        // Default output
        index = -1;
        number = 0;

        // Symbol is at the left edge
        if (symbolIndex == 0)
            return false;

        int indexOfOneOfTheDigits = symbolIndex - 1;
        char leftChar = currentRow[indexOfOneOfTheDigits];

        if (!char.IsDigit(leftChar))
            return false;

        (index, number) = GetIndexAndFullNumber(currentRow, indexOfOneOfTheDigits);
        return true;
    }

    private static bool TryGetRightNumber(string currentRow, int symbolIndex, out int index, out int number)
    {
        // Default output
        index = -1;
        number = 0;

        // Symbol is at the right edge
        if (symbolIndex == currentRow.Length - 1)
            return false;

        int indexOfOneOfTheDigits = symbolIndex + 1;
        char rightChar = currentRow[indexOfOneOfTheDigits];

        if (!char.IsDigit(rightChar))
            return false;

        (index, number) = GetIndexAndFullNumber(currentRow, indexOfOneOfTheDigits);
        return true;
    }

    private static bool TryGetBottomLeftNumber(string bottomRow, int symbolIndex, out int index, out int number)
    {
        // Apparently it is the same code, cuz top and bottom just depends on what row is passed in the parameters
        return TryGetTopLeftNumber(bottomRow, symbolIndex, out index, out number);
    }

    private static bool TryGetBottomRightNumber(string bottomRow, int symbolIndex, out int index, out int number)
    {
        // Apparently it is the same code, cuz top and bottom just depends on what row is passed in the parameters
        return TryGetTopRightNumber(bottomRow, symbolIndex, out index, out number);
    }

    private static bool TryGetBottomNumber(string bottomRow, int symbolIndex, out int index, out int number)
    {
        // Apparently it is the same code, cuz top and bottom just depends on what row is passed in the parameters
        return TryGetTopNumber(bottomRow, symbolIndex, out index, out number);
    }

    public static (int index, int number) GetIndexAndFullNumber(string line, int indexOfOneOfTheDigits)
    {
        // Go to the left
        int startIndex = indexOfOneOfTheDigits;
        for (int i = indexOfOneOfTheDigits; i >= 0; i--)
        {
            char c = line[i];

            if (!char.IsDigit(c))
                break;

            startIndex = i;
        }

        // Go to the right
        int length = 0;
        for (int i = startIndex; i < line.Length; i++)
        {
            char c = line[i];
            if (!char.IsDigit(c))
                break;

            length++;
        }

        string fullNumberSubString = line.Substring(startIndex, length);
        int fullnumber = int.Parse(fullNumberSubString);

        return (startIndex, fullnumber);
    }

    public static int[] FindIndexesOfSymbols(string line)
    {
        List<int> indexes = new List<int>();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (!char.IsLetterOrDigit(c) && c != char.Parse("."))
                indexes.Add(i);
        }

        return indexes.ToArray();
    }

    public static int[] FindIndexesOfGear(string line)
    {
        List<int> indexes = new List<int>();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == char.Parse("*"))
                indexes.Add(i);
        }

        return indexes.ToArray();
    }
}