using System;

namespace MyTerminal
{
    public class OutputHelper
    {
        public static void ConsoleWelcomeOutput()
        {
            var defCol = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(new string('=', 40));
            Console.ForegroundColor = defCol;
            Console.Write("\t" + "FILE MANAGER by ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("bloowb0und");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(new string('=', 40));
            Console.ForegroundColor = defCol;
            Console.WriteLine("Enter the commands below:");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("cd \"path\" - Change directory to path");
            Console.ForegroundColor = defCol;
            Console.WriteLine("ls - Output directory content");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t" + "Available flags for ls:");
            Console.WriteLine("\t" + "-srtS <= sort by size");
            Console.WriteLine("\t" + "-srtT <= sort by type");
            Console.WriteLine("\t" + "-srtN <= sort by name");
            Console.WriteLine("\t" + "-outTr <= Output directory content in tree view");
            Console.WriteLine("\t" + "-shHid <= Show hidden files");
            Console.ForegroundColor = defCol;
            Console.WriteLine("shCont \"fileName.extension\" - Output first 200 characters of file");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("fContain \"fileName.extension\" \"Finding this\" - Check if file contains given argument");
            Console.ForegroundColor = defCol;
            Console.WriteLine("fMove \"fileName.extension\" \"path\" - Move file from current directory to path");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("fCrt \"fileName.extension\" - Create a file in current directory");
            Console.ForegroundColor = defCol;
            Console.WriteLine("dirCrt \"directoryName\" - Create a directory in current directory");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("fDel \"fileName.extension\" - Delete a file from current directory");
            Console.ForegroundColor = defCol;
            Console.WriteLine("dirDel \"directoryName\" - Delete a directory from current directory");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("fRnm \"fileName.extension\" \"newFileName\" - Rename file");
            Console.ForegroundColor = defCol;
            Console.WriteLine("dirRnm \"directoryName\" \"newDirectoryName\" - Rename directory");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(new string('=', 40));
            Console.ForegroundColor = defCol;
            Console.WriteLine("help - Get list of all commands");
            Console.WriteLine("exit - Stop the file manager");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(new string('=', 40));
            Console.ForegroundColor = defCol;
        }

        public static void ConsoleEmptyInputOutput()
        {
            var defCol = Console.ForegroundColor;
            
            Console.WriteLine(new string('=', 20));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Input string can not be empty!");
            Console.WriteLine("Enter a correct command.");
            Console.ForegroundColor = defCol;
            Console.WriteLine(new string('=', 20));
        }
        
        public static void ConsoleInvalidArgumentsOutput()
        {
            var defCol = Console.ForegroundColor;
            
            Console.WriteLine(new string('=', 20));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not valid arguments provided.");
            Console.WriteLine("Enter help to get a list of commands signatures.");
            Console.ForegroundColor = defCol;
            Console.WriteLine(new string('=', 20));
        }

        public static void ConsoleFileContainsTextOutput(string fileName, string argument, bool status)
        {
            var defCol = Console.ForegroundColor;

            Console.WriteLine(new string('=', 20));
            if (status)
            {
                Console.Write($"File {fileName}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" CONTAINS ");
                Console.ForegroundColor = defCol;
                Console.WriteLine($"{argument}");
            }
            else
            {
                Console.Write($"File {fileName}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" does NOT contain ");
                Console.ForegroundColor = defCol;
                Console.WriteLine($"{argument}");
            }

            Console.ForegroundColor = defCol;
        }
        
        public static void ConsoleDirectoryDoesntExistOutput()
        {
            var defCol = Console.ForegroundColor;
            
            Console.WriteLine(new string('=', 20));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Directory with the given name wasn't found.");
            Console.WriteLine("Enter 'ls' to get all files and directories in current context.");
            Console.ForegroundColor = defCol;
            Console.WriteLine(new string('=', 20));
        }
        
        public static void ConsoleFileDoesntExistOutput()
        {
            var defCol = Console.ForegroundColor;
            
            Console.WriteLine(new string('=', 20));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("File with the given name wasn't found.");
            Console.WriteLine("Enter 'ls' to get all files and directories in current context.");
            Console.ForegroundColor = defCol;
            Console.WriteLine(new string('=', 20));
        }
    }
}