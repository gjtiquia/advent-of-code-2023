namespace UnitTests;

public class Day1Tests
{
    [TestCase("1abc2", 1)]
    [TestCase("pqr3stu8vwx", 3)]
    [TestCase("a1b2c3d4e5f", 1)]
    [TestCase("treb7uchet", 7)]
    public void ShouldGetTheFirstDigit(string line, int expectedResult)
    {
        int firstDigit = Day1Parser.GetFirstDigit(line);
        Assert.That(firstDigit, Is.EqualTo(expectedResult));
    }

    [TestCase("1abc2", 2)]
    [TestCase("pqr3stu8vwx", 8)]
    [TestCase("a1b2c3d4e5f", 5)]
    [TestCase("treb7uchet", 7)]
    public void ShouldGetTheLastDigit(string line, int expectedResult)
    {
        int lastDigit = Day1Parser.GetLastDigit(line);
        Assert.That(lastDigit, Is.EqualTo(expectedResult));
    }

    [TestCase("1abc2", 12)]
    [TestCase("pqr3stu8vwx", 38)]
    [TestCase("a1b2c3d4e5f", 15)]
    [TestCase("treb7uchet", 77)]
    public void ShouldGetCalibrationValue(string line, int expectedResult)
    {
        int calibrationValue = Day1Parser.GetCalibrationValue(line);
        Assert.That(calibrationValue, Is.EqualTo(expectedResult));
    }

    [Test]
    public void ShouldGetSumOfCalibrationValues()
    {
        // Doesnt work yet because... it gotes to the bin folder, a.txt does not get copied over
        // string currentDirectory = TestContext.CurrentContext.TestDirectory;
        // string filePath = Path.Combine(currentDirectory, "a.txt");

        string filePath = @"C:\Users\gersh\OneDrive\Documents\SelfProjects\advent-of-code-2023\UnitTests\Day1\a.txt";

        string[] lines = File.ReadAllLines(filePath);
        int sum = Day1Parser.GetSumOfCalibrationValue(lines);
        Assert.That(sum, Is.EqualTo(142));
    }

    [TestCase("two1nine", 2)]
    [TestCase("eightwothree", 8)]
    [TestCase("abcone2threexyz", 1)]
    [TestCase("xtwone3four", 2)]
    [TestCase("4nineeightseven2", 4)]
    [TestCase("zoneight234", 1)]
    [TestCase("7pqrstsixteen", 7)]
    public void ShouldGetTheFirstDigitWithSpelledOutLetters(string line, int expectedResult)
    {
        int firstDigit = Day1Parser.GetFirstDigit(line);
        Assert.That(firstDigit, Is.EqualTo(expectedResult));
    }

    [TestCase("two1nine", 9)]
    [TestCase("eightwothree", 3)]
    [TestCase("abcone2threexyz", 3)]
    [TestCase("xtwone3four", 4)]
    [TestCase("4nineeightseven2", 2)]
    [TestCase("zoneight234", 4)]
    [TestCase("7pqrstsixteen", 6)]
    public void ShouldGetTheLastDigitWithSpelledOutLetters(string line, int expectedResult)
    {
        int lastDigit = Day1Parser.GetLastDigit(line);
        Assert.That(lastDigit, Is.EqualTo(expectedResult));
    }

    [TestCase("two1nine", 29)]
    [TestCase("eightwothree", 83)]
    [TestCase("abcone2threexyz", 13)]
    [TestCase("xtwone3four", 24)]
    [TestCase("4nineeightseven2", 42)]
    [TestCase("zoneight234", 14)]
    [TestCase("7pqrstsixteen", 76)]
    public void ShouldGetCalibrationValueWithSpelledOutLetters(string line, int expectedResult)
    {
        int calibrationValue = Day1Parser.GetCalibrationValue(line);
        Assert.That(calibrationValue, Is.EqualTo(expectedResult));
    }

    [TestCase("eighthree", 83)]
    [TestCase("sevenine", 79)]
    // Not including because... input doesnt include these lol
    // Also not including because this would invalidate given case treb7uchet = 77
    // [TestCase("one", 1)]
    // [TestCase("two", 2)]
    // [TestCase("three", 3)]
    // [TestCase("four", 4)]
    // [TestCase("five", 5)]
    // [TestCase("six", 6)]
    // [TestCase("seven", 7)]
    // [TestCase("eight", 8)]
    // [TestCase("nine", 9)]
    [TestCase("twone", 21)]
    [TestCase("eightwo", 82)]
    [TestCase("nineight", 98)]
    [TestCase("eighthree", 83)]
    [TestCase("nineight", 98)]
    [TestCase("nineeight", 98)]
    // [TestCase("eeeight", 8)] // Failing, and not including because would invalidate given case treb7uchet = 77
    [TestCase("oooneeone", 11)]
    [TestCase("eightxh23eight", 88)]
    public void ShouldGetCalibrationValueFromRedditHint(string line, int expectedResult)
    {
        // https://www.reddit.com/r/adventofcode/comments/1884fpl/2023_day_1for_those_who_stuck_on_part_2/
        // https://www.reddit.com/r/adventofcode/comments/1885pt4/2023_day_01_part_2_test_cases_that_helped_me/
        // https://www.reddit.com/r/adventofcode/comments/1885lxp/part_two/

        int calibrationValue = Day1Parser.GetCalibrationValue(line);
        Assert.That(calibrationValue, Is.EqualTo(expectedResult));
    }

    [Test]
    public void ShouldGetSumOfCalibrationValuesWithSpelledOutLetters()
    {
        // Doesnt work yet because... it gotes to the bin folder, a.txt does not get copied over
        // string currentDirectory = TestContext.CurrentContext.TestDirectory;
        // string filePath = Path.Combine(currentDirectory, "b.txt");

        string filePath = @"C:\Users\gersh\OneDrive\Documents\SelfProjects\advent-of-code-2023\UnitTests\Day1\b.txt";

        string[] lines = File.ReadAllLines(filePath);
        int sum = Day1Parser.GetSumOfCalibrationValue(lines);
        Assert.That(sum, Is.EqualTo(281));
    }
}