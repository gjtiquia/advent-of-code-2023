using AdventOfCode.Day3;

namespace UnitTests;

public class Day3Day2Tests
{
    [TestCase("467..114..", new int[] { })]
    [TestCase("...*......", new int[] { 3 })]
    [TestCase("...$.*....", new int[] { 5 })]
    public void ShouldFindTheIndexesOfGear(string line, int[] targetIndexes)
    {
        int[] indexes = Day3Parser.FindIndexesOfGear(line);
        Assert.That(indexes, Is.EqualTo(targetIndexes));
    }

    [Test]
    public void ShouldFindGearRatioOfGear()
    {
        string row1 = "467..114..";
        string row2 = "...*......";
        string row3 = "..35..633.";

        int index = Day3Parser.FindIndexesOfGear(row2)[0];
        bool hasGearRatio = Day3Parser.TryFindGearRatioOfGear(row1, row2, row3, index, out int gearRatio);

        Assert.That(hasGearRatio, Is.True);
        Assert.That(gearRatio, Is.EqualTo(16345));
    }

    [TestCase(467835)]
    public void ShouldFindSumOfAllGearRatios(int expectedSum)
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day3", "a.txt");

        string[] lines = File.ReadAllLines(filePath);
        int sum = Day3Parser.FindSumOfAllGearRatios(lines);

        Assert.That(sum, Is.EqualTo(expectedSum));
    }
}