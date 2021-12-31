using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyTerminal
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string inputString;
            var oneArgCommands = new[]
            {
                "cd",
                "shCont",
                "fCrt",
                "dirCrt",
                "fDel",
                "dirDel"
            };
            
            var twoArgCommands = new[]
            {
                "fContain",
                "fMove",
                "fRnm",
                "dirRnm"
            };
            var input = new Input();
            var fileHelper = new FileHelper();
            
            OutputHelper.ConsoleWelcomeOutput();
            do
            {
                Console.Write($"<{fileHelper.CurrentPath}> ");
                inputString = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputString))
                {
                    OutputHelper.ConsoleEmptyInputOutput();
                    continue;
                }

                if (inputString == "exit")
                {
                    return;
                }
                
                if (inputString == "help")
                {
                    OutputHelper.ConsoleWelcomeOutput();
                    continue;
                }

                var command = inputString.Split(' ')[0].Trim();

                string inputStrWithoutCommand = null;
                if (inputString.Length != command.Length)
                {
                    inputStrWithoutCommand = inputString.Substring(command.Length + 1);
                }
                else
                {
                    if (command != "ls")
                    {
                        OutputHelper.ConsoleInvalidArgumentsOutput();
                        continue;
                    }
                }

                string[] parts;
                string firstArg = null, secondArg = null;

                if (command == "ls")
                {
                    if (inputStrWithoutCommand != null)
                    {
                        var flagsArr = inputStrWithoutCommand.Split(' ');

                        var valid = true;
                        foreach (var flag in flagsArr)
                        {
                            if (flag[0] != '-')
                            {
                                OutputHelper.ConsoleInvalidArgumentsOutput();
                                valid = false;
                                break;
                            }
                        }

                        if (!valid)
                            continue;

                        input.Flags = flagsArr;
                    }
                }
                else
                {
                    parts = Regex.Matches(inputStrWithoutCommand, @"[\""].+?[\""]|[^ ]+")
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToArray();

                    var isOneArgCommand = true;
                    if (Array.Exists(oneArgCommands, s => s == command))
                    {
                        if (parts.Length != 1)
                        {
                            OutputHelper.ConsoleInvalidArgumentsOutput();
                            continue;
                        }
                    }
                    else if (Array.Exists(twoArgCommands, s => s == command))
                    {
                        isOneArgCommand = false;
                        if (parts.Length != 2)
                        {
                            OutputHelper.ConsoleInvalidArgumentsOutput();
                            continue;
                        }
                    }

                    if (command == "cd" && parts[0] == "../")
                    {
                        fileHelper.SetCurrentPath(parts[0]);
                    }

                    var valid = true;
                    for (var i = 0; i < parts.Length; i++)
                    {
                        if (!parts[i].StartsWith("\"") && !parts[i].EndsWith("\""))
                        {
                            OutputHelper.ConsoleInvalidArgumentsOutput();
                            valid = false;
                            break;
                        }

                        parts[i] = parts[i].Substring(1, parts[i].Length - 2);
                    }

                    if(!valid)
                        continue;
                    
                    firstArg = parts[0];
                    
                    if(!isOneArgCommand)
                        secondArg = parts[1];
                }

                input.Command = command;
                switch (command)
                {
                    case "ls":
                        fileHelper.ShowDirectoryContent(input);
                        break;

                    case "cd":
                    case "shCont":
                    case "fCrt":
                    case "dirCrt":
                    case "fDel":
                    case "dirDel":

                        switch (command)
                        {
                            case "cd":
                                fileHelper.SetCurrentPath(firstArg);
                                break;
                            case "shCont":
                                fileHelper.ShowFileContent(firstArg);
                                break;
                            case "fCrt":
                                fileHelper.CreateFile(firstArg);
                                break;
                            case "dirCrt":
                                fileHelper.CreateDirectory(firstArg);
                                break;
                            case "fDel":
                                fileHelper.DeleteFile(firstArg);
                                break;
                            case "dirDel":
                                fileHelper.DeleteDirectory(firstArg);
                                break;
                        }

                        break;

                    case "fContain":
                    case "fMove":
                    case "fRnm":
                    case "dirRnm":

                        switch (command)
                        {
                            case "fContain":
                                fileHelper.FileContainsString(firstArg, secondArg);
                                break;
                            case "fMove":
                                fileHelper.MoveFile(firstArg, secondArg);
                                break;
                            case "fRnm":
                                fileHelper.RenameFile(firstArg, secondArg);
                                break;
                            case "dirRnm":
                                fileHelper.RenameDirectory(firstArg, secondArg);
                                break;
                        }

                        break;
                }

            } while (inputString != "exit");
        }

        public string hello()
        {
            return "str";
        }
    }
}