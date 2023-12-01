# .Net Unit Test Template

A minimal template for setting up a .Net Console App project with unit tests using [NUnit](https://nunit.org/) in VSCode.

References

- <https://code.visualstudio.com/docs/languages/csharp>
- <https://code.visualstudio.com/docs/csharp/get-started>
- <https://code.visualstudio.com/docs/csharp/testing>
- <https://docs.nunit.org/articles/nunit/getting-started/installation.html#im-using-visual-studio-code-as-my-development-tool>
- <https://docs.nunit.org/articles/nunit/running-tests/Index.html>

## Setup Steps

### 1. Console App Setup

In root directory, using VSCode Command Pallete with C# Dev Kit installed: _.Net: New Project_ > _Console App_.

Name the project `ConsoleApp`.

In the `ConsoleApp` directory, Run the following to create a `.gitignore` file.

```bash
dotnet new gitignore
```

In the `ConsoleApp` directory, Run the following command to run the `Program.cs` file.

```bash
dotnet run
```

### 2. NUnit Setup

In root directory, using VSCode Command Pallete with C# Dev Kit installed: _.Net: New Project_ > _NUnit 3 Test Project_.

Name the project `TestProject`

In the `TestProject` directory, Run the following to create a `.gitignore` file.

```bash
dotnet new gitignore
```

In the root directory, run the following command to reference the main project `ConsoleApp` in the test project `TestProject`.

```bash
dotnet add TestProject/TestProject.csproj reference ConsoleApp/ConsoleApp.csproj
```
