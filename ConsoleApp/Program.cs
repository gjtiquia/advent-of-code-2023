using Project.Core;

string[] lines = File.ReadAllLines("./ConsoleApp/Core/Day1/a.txt");
Console.WriteLine(Day1Parser.GetSumOfCalibrationValue(lines));