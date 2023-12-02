namespace UnitTests;

public class Day2Tests
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
        string filePath = Path.Combine(currentDirectory, "Day2", "a.txt");

        string[] lines = File.ReadAllLines(filePath);
        int sum = Day2Parser.GetSumOfPossibleGameIDs(configuration, lines);

        Assert.That(sum, Is.EqualTo(targetSum));
    }

    private void AssertThatLinesAreEqual(string[] lines, string[] targetLines)
    {
        Assert.That(lines.Length, Is.EqualTo(targetLines.Length));
        for (int i = 0; i < lines.Length; i++)
        {
            Assert.That(lines[i], Is.EqualTo(targetLines[i]));
        }
    }

    private void AssertDictionaryAreEqual<K, V>(Dictionary<K, V> dictionary, Dictionary<K, V> targetDictionary) where K : notnull
    {
        Assert.That(dictionary.Count, Is.EqualTo(targetDictionary.Count));

        foreach (var (key, value) in targetDictionary)
        {
            Assert.IsTrue(dictionary.ContainsKey(key));
            Assert.That(dictionary[key], Is.EqualTo(value));
        }
    }
}

public class Day2Parser
{
    public static int GetSumOfPossibleGameIDs(string configuration, string[] lines)
    {
        int sum = 0;

        foreach (string line in lines)
        {
            string[] splitLines = SplitAndTrim(":", line);
            string gameInfo = splitLines[0];
            string game = splitLines[1];

            bool isGamePossible = IsGamePossible(game, configuration);
            if (!isGamePossible) continue;

            int gameID = GetGameID(gameInfo);
            sum += gameID;
        }

        return sum;
    }

    public static bool IsGamePossible(string game, string configuration)
    {
        string[] cubeSets = SplitAndTrim(";", game);
        foreach (string cubeSet in cubeSets)
        {
            bool isPossible = IsCubeSetPossible(cubeSet, configuration);
            if (!isPossible)
                return false;
        }

        return true;
    }

    public static bool IsCubeSetPossible(string cubeSet, string configuration)
    {
        Dictionary<string, int> cubeSetDictionary = GetCubeSetDictionary(cubeSet);
        Dictionary<string, int> configurationDictionary = GetCubeSetDictionary(configuration);

        foreach (var (color, number) in cubeSetDictionary)
        {
            int maxNumber = configurationDictionary[color];
            if (number > maxNumber)
                return false;
        }

        return true;
    }

    public static int GetGameID(string line)
    {
        string[] splitLines = line.Split(" ");
        return int.Parse(splitLines[1]);
    }

    public static Dictionary<string, int> GetCubeSetDictionary(string line)
    {
        Dictionary<string, int> cubeDictionary = new Dictionary<string, int>();

        string[] lines = SplitAndTrim(",", line);
        foreach (string infoLine in lines)
        {
            (string color, int number) = GetColorAndNumberFromCubes(infoLine);
            cubeDictionary.Add(color, number);
        }

        return cubeDictionary;
    }

    public static (string, int) GetColorAndNumberFromCubes(string line)
    {
        string[] splitLines = SplitAndTrim(" ", line);
        int number = int.Parse(splitLines[0]);
        string color = splitLines[1];

        return (color, number);
    }

    public static string[] SplitAndTrim(string seperator, string line)
    {
        return line.Split(seperator).Select(x => x.Trim()).ToArray();
    }
}