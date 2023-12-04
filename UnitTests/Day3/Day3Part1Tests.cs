using AdventOfCode.Day3;

namespace UnitTests;

public class Day3Part1Tests
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

            TestUtilities.AssertDictionaryAreEqual(row, expectedRow);
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

            TestUtilities.AssertDictionaryAreEqual(row, expectedRow);
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

