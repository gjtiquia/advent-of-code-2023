string[] lines = File.ReadAllLines("./AdventOfCode/Day2/input.txt");
string configuration = "12 red, 13 green, 14 blue";

Console.WriteLine(Day2Parser.GetSumOfPossibleGameIDs(configuration, lines));