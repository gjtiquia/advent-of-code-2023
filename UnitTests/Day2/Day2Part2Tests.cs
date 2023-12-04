namespace UnitTests;

public class Day2Part2Tests : Day2Tests
{
    [TestCase("3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "4 red, 2 green, 6 blue")]
    public void ShouldGetMinimumConfigurationFromGame(string gameLine, string targetConfigurationLine)
    {
        Dictionary<string, int> minimumConfiguration = Day2Parser.GetMinimumConfigurationFromGame(gameLine);
        Dictionary<string, int> targetConfiguration = Day2Parser.GetCubeSetDictionary(targetConfigurationLine);

        TestUtilities.AssertDictionaryAreEqual(minimumConfiguration, targetConfiguration);
    }

    [TestCase("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48)]
    public void ShouldGetPowerOfGame(string gameLine, int targetPower)
    {
        int power = Day2Parser.GetPowerFromGame(gameLine);

        Assert.That(power, Is.EqualTo(targetPower));
    }

    [TestCase(2286)]
    public void ShouldGetSumOfPowerOfGames(int targetSum)
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day2", "input.txt");

        string[] lines = File.ReadAllLines(filePath);
        int sum = Day2Parser.GetSumOfPowerOfGames(lines);

        Assert.That(sum, Is.EqualTo(targetSum));
    }
}