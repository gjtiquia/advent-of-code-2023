using AdventOfCode.Day6;

namespace UnitTests;

public class Day6Tests
{
    [Test]
    public void ShouldFindProductOfWaysToWinFromTestInput()
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day6", "input.txt");

        string[] lines = File.ReadAllLines(filePath);

        int productOfWaysToWin = Day6Parser.FindProductOfWaysToWin(lines);
        Assert.That(productOfWaysToWin, Is.EqualTo(288));
    }

    [TestCase(7, 9, 4)]
    [TestCase(15, 40, 8)]
    [TestCase(30, 200, 9)]
    public void ShouldFindNumberOfWaysToWin(int time, int distance, int expectedWaysToWin)
    {
        int waysToWin = Day6Parser.FindNumberOfWaysToWin(time, distance);
        Assert.That(waysToWin, Is.EqualTo(expectedWaysToWin));
    }
}
