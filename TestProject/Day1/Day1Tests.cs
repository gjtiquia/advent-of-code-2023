using Project.Core;

namespace TestProject;

public class Day1Tests
{
    [TestCase("1abc2", 1)]
    [TestCase("pqr3stu8vwx", 3)]
    [TestCase("a1b2c3d4e5f", 1)]
    [TestCase("treb7uchet", 7)]
    public void ShouldGetTheFirstDigit(string input, int expectedResult)
    {
        Day1Parser parser = new Day1Parser();
        int firstDigit = parser.GetFirstDigit(input);

        Assert.That(firstDigit, Is.EqualTo(expectedResult));
    }


}