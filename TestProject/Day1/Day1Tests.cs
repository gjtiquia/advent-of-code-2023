using System.Reflection;
using Project.Core;

namespace TestProject;

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

        string filePath = @"C:\Users\gersh\OneDrive\Documents\SelfProjects\advent-of-code-2023\TestProject\Day1\a.txt";

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
}