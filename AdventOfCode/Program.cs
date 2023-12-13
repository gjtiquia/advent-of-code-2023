using AdventOfCode.Day1;
using AdventOfCode.Day2;
using AdventOfCode.Day3;
using AdventOfCode.Day4;
using AdventOfCode.Day5;
using AdventOfCode.Day6;
using AdventOfCode.Day7;

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
Console.WriteLine($"Day 4 Part 2: {Day4Parser.GetTotalScratchcards(day4Input)}");

// Day5
string[] day5Input = File.ReadAllLines($"{baseDirectory}/Day5/input.txt");
Console.WriteLine($"Day 5 Part 1: (Invalid due to change of requirements in Part 2)");
// ulong nearestLocation = Day5Parser.GetNearestLocation(day5Input); // Took about 50min on 2019 Intel MacBook Pro on battery
Console.WriteLine($"Day 5 Part 2: (Skipped because calculated by Brute Force)");

// Day6
string[] day6Input = File.ReadAllLines($"{baseDirectory}/Day6/input.txt");
Console.WriteLine($"Day 6 Part 1: {Day6Parser.FindProductOfWaysToWin(day6Input)}");
Console.WriteLine($"Day 6 Part 2: {Day6Parser.FindWaysToWinFromLongRace(day6Input)}");

// Day 7
string[] day7Input = File.ReadAllLines($"{baseDirectory}/Day7/input.txt");
Console.WriteLine($"Day 7 Part 1: {Day7Parser.CalculateWinnings(day7Input)}");
