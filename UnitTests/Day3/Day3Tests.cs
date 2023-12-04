namespace UnitTests;

public class Day3Tests
{
    [TestCase("467..114..", new int[] { })]
    [TestCase("...*......", new int[] { 3 })]
    [TestCase("...$.*....", new int[] { 3, 5 })]
    public void ShouldFindTheIndexesOfSymbols(string line, int[] targetIndexes)
    {
        int[] indexes = Day3Parser.FindIndexesOfSymbols(line);
        Assert.That(indexes, Is.EqualTo(targetIndexes));
    }

    [TestCase("467..114..", 1, 0, 467)]
    public void ShouldGetIndexAndFullNumberGivenIndexOfOneOfTheDigits(string line, int indexOfOneOfTheDigits, int targetIndex, int targetNumber)
    {
        (int index, int number) = Day3Parser.GetIndexAndFullNumber(line, indexOfOneOfTheDigits);

        Assert.That(index, Is.EqualTo(targetIndex));
        Assert.That(number, Is.EqualTo(targetNumber));
    }

    [Test]
    public void ShouldFindNumbersAdjacentToSymbol()
    {
        string row1 = "467..114..";
        string row2 = "...*......";
        string row3 = "..35..633.";

        int index = Day3Parser.FindIndexesOfSymbols(row2)[0];
        List<Dictionary<int, int>> adjacentNumbers = Day3Parser.FindAdjacentNumbersOfSymbol(row1, row2, row3, index);

        List<Dictionary<int, int>> expectedAdjacentNumbers =
        [
            new Dictionary<int, int>()
            {
                {0, 467}
            },
            new Dictionary<int, int>()
            {

            },
            new Dictionary<int, int>()
            {
                {2, 35}
            },
        ];

        Assert.That(adjacentNumbers.Count, Is.EqualTo(expectedAdjacentNumbers.Count));
        for (int i = 0; i < adjacentNumbers.Count; i++)
        {
            Dictionary<int, int> row = adjacentNumbers[i];
            Dictionary<int, int> expectedRow = expectedAdjacentNumbers[i];

            Utilities.AssertDictionaryAreEqual(row, expectedRow);
        }
    }

    [Test]
    public void ShouldFindNumbersAdjacentToAllSymbols()
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day3", "a.txt");

        string[] lines = File.ReadAllLines(filePath);
        List<Dictionary<int, int>> adjacentNumbers = Day3Parser.FindNumbersAdjacentToAllSymbols(lines);

        List<Dictionary<int, int>> expectedAdjacentNumbers =
        [
            new Dictionary<int, int>()
            {
                {0, 467}
            },
            new Dictionary<int, int>()
            {

            },
            new Dictionary<int, int>()
            {
                {2, 35}, {6, 633}
            },
            new Dictionary<int, int>()
            {

            },
            new Dictionary<int, int>()
            {
                {0, 617}
            },
            new Dictionary<int, int>()
            {

            },
            new Dictionary<int, int>()
            {
                {2, 592}
            },
            new Dictionary<int, int>()
            {
                {6, 755}
            },
            new Dictionary<int, int>()
            {

            },
            new Dictionary<int, int>()
            {
                {1, 664}, {5, 598}
            },
        ];

        Assert.That(adjacentNumbers.Count, Is.EqualTo(expectedAdjacentNumbers.Count));
        for (int i = 0; i < adjacentNumbers.Count; i++)
        {
            Dictionary<int, int> row = adjacentNumbers[i];
            Dictionary<int, int> expectedRow = expectedAdjacentNumbers[i];

            Utilities.AssertDictionaryAreEqual(row, expectedRow);
        }
    }

    [TestCase(4361)]
    public void ShouldFindSumOfAllPartNumbers(int expectedSum)
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day3", "a.txt");

        string[] lines = File.ReadAllLines(filePath);
        int sum = Day3Parser.FindSumOfAllPartNumbers(lines);

        Assert.That(sum, Is.EqualTo(expectedSum));
    }
}

public class Day3Parser
{
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
}