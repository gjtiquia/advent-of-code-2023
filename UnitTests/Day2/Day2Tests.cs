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
    public void ShouldSplitBySemiColon(string line, params string[] lines)
    {
        string[] splitLines = Day2Parser.SplitAndTrim(";", line);
        AssertThatLinesAreEqual(splitLines, lines);
    }

    [TestCase("3 blue, 4 red", "3 blue", "4 red")]
    public void ShouldSplitByComma(string line, params string[] lines)
    {
        string[] splitLines = Day2Parser.SplitAndTrim(",", line);
        AssertThatLinesAreEqual(splitLines, lines);
    }

    [TestCase("3 blue", 3, "blue")]
    public void ShouldGetNumberAndColor(string line, int targetNumber, string targetColor)
    {
        (string color, int number) = Day2Parser.GetColorAndNumber(line);

        Assert.That(number, Is.EqualTo(targetNumber));
        Assert.That(color, Is.EqualTo(targetColor));
    }

    [Test]
    public void ShouldParseIntoDictionary()
    {
        string line = "3 blue, 4 red";
        Dictionary<string, int> cubeDictionary = Day2Parser.GetCubeDictionary(line);

        Dictionary<string, int> targetDictionary = new()
        {
            {"red", 4},
            {"blue", 3}
        };

        AssertDictionaryAreEqual(cubeDictionary, targetDictionary);
    }

    [TestCase("3 blue, 4 red", "12 red, 13 green, 14 blue", true)]
    [TestCase("8 green, 6 blue, 20 red", "12 red, 13 green, 14 blue", false)]
    public void ShouldDetermineIfSetIsPossible(string set, string configuration, bool targetIsPossible)
    {
        bool isPossible = Day2Parser.IsSetPossible(set, configuration);
        Assert.That(targetIsPossible, Is.EqualTo(isPossible));
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
    public static bool IsSetPossible(string set, string configuration)
    {
        Dictionary<string, int> setDictionary = GetCubeDictionary(set);
        Dictionary<string, int> configurationDictionary = GetCubeDictionary(configuration);

        foreach (var (color, number) in setDictionary)
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

    public static Dictionary<string, int> GetCubeDictionary(string line)
    {
        Dictionary<string, int> cubeDictionary = new Dictionary<string, int>();

        string[] lines = SplitAndTrim(",", line);
        foreach (string infoLine in lines)
        {
            (string color, int number) = GetColorAndNumber(infoLine);
            cubeDictionary.Add(color, number);
        }

        return cubeDictionary;
    }

    public static (string, int) GetColorAndNumber(string line)
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