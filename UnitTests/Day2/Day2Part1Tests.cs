namespace UnitTests;

public class Day2Part1Tests : Day2Tests
{
    [TestCase("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "Game 1", "3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green")]
    public void ShouldSplitByColon(string line, params string[] lines)
    {
        string[] splitLines = Day2Parser.SplitAndTrim(":", line);
        AssertThatLinesAreEqual(splitLines, lines);
    }

    [TestCase("Game 1", 1)]
    public void ShouldGetGameID(string line, int targetID)
    {
        int id = Day2Parser.GetGameID(line);
        Assert.That(id, Is.EqualTo(targetID));
    }

    [TestCase("3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "3 blue, 4 red", "1 red, 2 green, 6 blue", "2 green")]
    public void ShouldSplitGameToCubeSets(string game, params string[] targetCubeSets)
    {
        string[] sets = Day2Parser.SplitAndTrim(";", game);
        AssertThatLinesAreEqual(sets, targetCubeSets);
    }

    [TestCase("3 blue, 4 red", "3 blue", "4 red")]
    public void ShouldSplitCubeSetToCubes(string cubeSet, params string[] cubes)
    {
        string[] splitLines = Day2Parser.SplitAndTrim(",", cubeSet);
        AssertThatLinesAreEqual(splitLines, cubes);
    }

    [TestCase("3 blue", 3, "blue")]
    public void ShouldGetNumberAndColor(string cubes, int targetNumber, string targetColor)
    {
        (string color, int number) = Day2Parser.GetColorAndNumberFromCubes(cubes);

        Assert.That(number, Is.EqualTo(targetNumber));
        Assert.That(color, Is.EqualTo(targetColor));
    }

    [Test]
    public void ShouldParseCubeSetIntoDictionary()
    {
        string set = "3 blue, 4 red";
        Dictionary<string, int> cubeSetDictionary = Day2Parser.GetCubeSetDictionary(set);

        Dictionary<string, int> targetDictionary = new()
        {
            {"red", 4},
            {"blue", 3}
        };

        AssertDictionaryAreEqual(cubeSetDictionary, targetDictionary);
    }

    [TestCase("3 blue, 4 red", "12 red, 13 green, 14 blue", true)]
    [TestCase("8 green, 6 blue, 20 red", "12 red, 13 green, 14 blue", false)]
    public void ShouldDetermineIfSetIsPossible(string set, string configuration, bool targetIsPossible)
    {
        bool isPossible = Day2Parser.IsCubeSetPossible(set, configuration);
        Assert.That(targetIsPossible, Is.EqualTo(isPossible));
    }

    [TestCase("3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "12 red, 13 green, 14 blue", true)]
    [TestCase("8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", "12 red, 13 green, 14 blue", false)]
    public void ShouldDetermineIfGameIsPossible(string game, string configuration, bool targetIsPossible)
    {
        bool isPossible = Day2Parser.IsGamePossible(game, configuration);
        Assert.That(targetIsPossible, Is.EqualTo(isPossible));
    }

    [TestCase("12 red, 13 green, 14 blue", 8)]
    public void ShouldGetSumOfGameIDsOfPossibleGames(string configuration, int targetSum)
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day2", "input.txt");

        string[] lines = File.ReadAllLines(filePath);
        int sum = Day2Parser.GetSumOfPossibleGameIDs(configuration, lines);

        Assert.That(sum, Is.EqualTo(targetSum));
    }
}

