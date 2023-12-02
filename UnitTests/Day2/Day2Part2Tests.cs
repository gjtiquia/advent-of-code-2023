namespace UnitTests;

public class Day2Part2Tests : Day2Tests
{
    [TestCase("3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "4 red, 2 green, 6 blue")]
    public void ShouldGetMinimumConfigurationFromGame(string gameLine, string targetConfigurationLine)
    {
        Dictionary<string, int> minimumConfiguration = Day2Parser.GetMinimumConfigurationFromGame(gameLine);
        Dictionary<string, int> targetConfiguration = Day2Parser.GetCubeSetDictionary(targetConfigurationLine);

        AssertDictionaryAreEqual(minimumConfiguration, targetConfiguration);
    }

    [TestCase("3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48)]
    public void ShouldGetPowerOfGame(string gameLine, int targetPower)
    {
        int power = Day2Parser.GetPowerFromGame(gameLine);

        Assert.That(power, Is.EqualTo(targetPower));
    }
}