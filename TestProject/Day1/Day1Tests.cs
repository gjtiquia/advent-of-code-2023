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
}