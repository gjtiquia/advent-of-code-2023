namespace AdventOfCode.Day2;

public class Day2Parser
{
    public static int GetSumOfPowerOfGames(string[] gameLines)
    {
        int sum = 0;
        foreach (string game in gameLines)
        {
            sum += GetPowerFromGame(game);
        }

        return sum;
    }

    public static int GetPowerFromGame(string gameLine)
    {
        int power = 1;

        string[] splitLines = Utilities.SplitAndTrim(":", gameLine);
        string game = splitLines[1];

        Dictionary<string, int> minimumConfiguration = GetMinimumConfigurationFromGame(game);
        foreach (var (color, number) in minimumConfiguration)
        {
            power *= number;
        }

        return power;
    }

    public static Dictionary<string, int> GetMinimumConfigurationFromGame(string gameLine)
    {
        Dictionary<string, int> minimumConfiguration = new Dictionary<string, int>();

        string[] cubeSets = Utilities.SplitAndTrim(";", gameLine);
        foreach (string cubeSet in cubeSets)
        {
            Dictionary<string, int> cubeSetDictionary = GetCubeSetDictionary(cubeSet);
            foreach (var (color, number) in cubeSetDictionary)
            {
                if (!minimumConfiguration.ContainsKey(color))
                {
                    minimumConfiguration.Add(color, number);
                }
                else
                {
                    int currentNumber = minimumConfiguration[color];
                    if (number > currentNumber)
                        minimumConfiguration[color] = number;
                }
            }
        }

        return minimumConfiguration;
    }

    public static int GetSumOfPossibleGameIDs(string configuration, string[] lines)
    {
        int sum = 0;

        foreach (string line in lines)
        {
            string[] splitLines = Utilities.SplitAndTrim(":", line);
            string gameInfo = splitLines[0];
            string game = splitLines[1];

            bool isGamePossible = IsGamePossible(game, configuration);
            if (!isGamePossible) continue;

            int gameID = GetGameID(gameInfo);
            sum += gameID;
        }

        return sum;
    }

    public static bool IsGamePossible(string game, string configuration)
    {
        string[] cubeSets = Utilities.SplitAndTrim(";", game);
        foreach (string cubeSet in cubeSets)
        {
            bool isPossible = IsCubeSetPossible(cubeSet, configuration);
            if (!isPossible)
                return false;
        }

        return true;
    }

    public static bool IsCubeSetPossible(string cubeSet, string configuration)
    {
        Dictionary<string, int> cubeSetDictionary = GetCubeSetDictionary(cubeSet);
        Dictionary<string, int> configurationDictionary = GetCubeSetDictionary(configuration);

        foreach (var (color, number) in cubeSetDictionary)
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

    public static Dictionary<string, int> GetCubeSetDictionary(string line)
    {
        Dictionary<string, int> cubeDictionary = new Dictionary<string, int>();

        string[] lines = Utilities.SplitAndTrim(",", line);
        foreach (string infoLine in lines)
        {
            (string color, int number) = GetColorAndNumberFromCubes(infoLine);
            cubeDictionary.Add(color, number);
        }

        return cubeDictionary;
    }

    public static (string, int) GetColorAndNumberFromCubes(string line)
    {
        string[] splitLines = Utilities.SplitAndTrim(" ", line);
        int number = int.Parse(splitLines[0]);
        string color = splitLines[1];

        return (color, number);
    }
}