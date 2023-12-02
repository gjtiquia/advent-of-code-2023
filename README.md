# Advent of Code 2023

"Advent of Code is an Advent calendar of small programming puzzles for a variety of skill sets and skill levels that can be solved in any programming language you like." - _Eric Wastl ([Advent Of Code](https://adventofcode.com/2023/about))_

This repository is my attempt at the Advent of Code 2023 with C# using the .Net framework. This also serves as a practice for Test-Driven Development (TDD) using the NUnit testing framework, as such the solutions for each puzzle will be backed by unit tests.

## Terminal Commands

Pre-requisites: Have the .Net 8.0 SDK installed on your local machine.

Run [`/AdventOfCode/Program.cs`](./AdventOfCode/Program.cs)

```bash
dotnet run --project AdventOfCode
```

Run all unit tests

```bash
dotnet test
```

## Troubleshooting

If you have a different .Net SDK version, change the `<TargetFramework>` field in [AdventOfCode.csproj](./AdventOfCode/AdventOfCode.csproj) and [UnitTests.csproj](./UnitTests/UnitTests.csproj) to the corresponging version that you have.
