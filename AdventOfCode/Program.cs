using AdventOfCode.Day1;
using AdventOfCode.Day2;
using AdventOfCode.Day3;
using AdventOfCode.Day4;


string baseDirectory = "./AdventOfCode";

Console.WriteLine("===== ADVENT OF CODE 2023 =====");

// Day 1
string[] day1Input = File.ReadAllLines($"{baseDirectory}/Day1/input.txt");
Console.WriteLine($"Day 1 Part 1: (Invalid due to change of requirements in Part 2)");
Console.WriteLine($"Day 1 Part 2: {Day1Parser.GetSumOfCalibrationValue(day1Input)}");

// Day 2
string[] day2Input = File.ReadAllLines($"{baseDirectory}/Day2/input.txt");
string day2Part1Configuraion = "12 red, 13 green, 14 blue";
Console.WriteLine($"Day 2 Part 1: {Day2Parser.GetSumOfPossibleGameIDs(day2Part1Configuraion, day2Input)}");
Console.WriteLine($"Day 2 Part 2: {Day2Parser.GetSumOfPowerOfGames(day2Input)}");

// Day3
string[] day3Input = File.ReadAllLines($"{baseDirectory}/Day3/input.txt");
Console.WriteLine($"Day 3 Part 1: {Day3Parser.FindSumOfAllPartNumbers(day3Input)}");
Console.WriteLine($"Day 3 Part 2: {Day3Parser.FindSumOfAllGearRatios(day3Input)}");

// Day4
string[] day4Input = File.ReadAllLines($"{baseDirectory}/Day4/input.txt");
Console.WriteLine($"Day 4 Part 1: {Day4Parser.GetSumOfPoints(day4Input)}");


