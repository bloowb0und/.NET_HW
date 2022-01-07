using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MyTerminal
{
    public class FileHelper
    {
        public string CurrentPath { get; private set; }

        public FileHelper()
        {
            CurrentPath = Directory.GetCurrentDirectory();
        }

        public bool SetCurrentPath(string curPath)
        {
            if (string.IsNullOrWhiteSpace(curPath))
            {
                OutputHelper.ConsoleEmptyInputOutput();
                
                return false;
            }

            if (!Directory.Exists(curPath))
            {
                OutputHelper.ConsoleDirectoryDoesntExistOutput();
                
                return false;
            }

            if (curPath == "../")
            {
                var allParts = CurrentPath.Split('\\');
                if (allParts[allParts.Length - 1].Length != 0)
                {
                    curPath = CurrentPath.Substring(0, CurrentPath.Length - allParts[allParts.Length - 1].Length);
                }
                else
                {
                    curPath = CurrentPath.Substring(0, CurrentPath.Length - (allParts[allParts.Length - 2].Length + 1));
                }
            }
            
            CurrentPath = curPath;
            
            return true;
        }
        
        public bool CreateFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                OutputHelper.ConsoleInvalidArgumentsOutput();
                
                return false; 
            }
            
            using (var fs = File.Create($"{CurrentPath}\\{fileName}"))
            {
            }

            return true;
        }

        public bool DeleteFile(string fileName)
        {
            var file = new FileInfo($"{CurrentPath}\\{fileName}");

            if (!file.Exists)
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }

            file.Delete();
            
            return true;
        }
        
        public bool CreateDirectory(string dirName)
        {
            if (dirName == string.Empty)
            {
                OutputHelper.ConsoleInvalidArgumentsOutput();
                
                return false;
            }
            
            var directory = new DirectoryInfo($"{CurrentPath}\\{dirName}");

            if (directory.Exists)
            {
                return false;
            }

            directory.Create();
            
            return true;
        }

        public bool DeleteDirectory(string dirName)
        {
            var directory = new DirectoryInfo($"{CurrentPath}\\{dirName}");
            
            if (!directory.Exists)
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }
            
            directory.Delete(true);
            
            return true;
        }
        
        public bool MoveFile(string fileName, string newPath)
        {
            var file = new FileInfo($"{CurrentPath}\\{fileName}");
            
            if (!file.Exists)
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }

            if (!Directory.Exists(newPath))
            {
                OutputHelper.ConsoleDirectoryDoesntExistOutput();
                
                return false;
            }

            if (File.Exists($"{newPath}\\{fileName}"))
            {
                File.Delete($"{newPath}\\{fileName}");
            }

            File.Move($"{CurrentPath}\\{fileName}", $"{newPath}\\{fileName}");
            
            return true;
        }
        
        public bool ShowDirectoryContent(Input input)
        {
            if (!Directory.Exists(CurrentPath))
            {
                OutputHelper.ConsoleDirectoryDoesntExistOutput();
                
                return false;
            }

            var directoryInfo = new DirectoryInfo(CurrentPath);
            var files = directoryInfo.GetFiles();
            var dirs = directoryInfo.GetDirectories();
            var filteredF = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)).ToArray();
            var filteredD = dirs.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)).ToArray();

            if (input.Flags != null)
            {
                if (Array.Exists(input.Flags, s => s == "-shHid"))
                {
                    filteredF = files;
                    filteredD = dirs;
                }

                if (Array.Exists(input.Flags, s => s == "-srtS"))
                {
                    filteredF = filteredF.OrderByDescending(f => f.Length).ToArray();
                }
                else if (Array.Exists(input.Flags, s => s == "-srtT"))
                {
                    filteredF = filteredF.OrderByDescending(f => f.Extension).ToArray();
                    filteredD = filteredD.OrderByDescending(d => d.Extension).ToArray();
                }
                else if (Array.Exists(input.Flags, s => s == "-srtN"))
                {
                    filteredF = filteredF.OrderByDescending(f => f.Name).ToArray();
                    filteredD = filteredD.OrderByDescending(d => d.Name).ToArray();
                }
            }

            Console.WriteLine(new string('-', 20));
            foreach (var directory in filteredD)
            {
                Console.WriteLine(directory.Name);
            }
            
            foreach (var file in filteredF)
            {
                Console.WriteLine(file.Name);

                if (input.Flags == null || !Array.Exists(input.Flags, s => s == "-outTr"))
                {
                    continue;
                }

                Console.WriteLine("\t" + "- size: " + file.Length + " bytes");
                Console.WriteLine("\t" + "- created: " + file.CreationTimeUtc.Date.ToString("d"));
                Console.WriteLine("\t" + "- edited: " + file.LastWriteTimeUtc.Date.ToString("d"));
            }
            Console.WriteLine(new string('-', 20));
            
            return true;
        }

        public bool ShowFileContent(string fileName)
        {
            if (!File.Exists($"{CurrentPath}\\{fileName}"))
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }
            
            using (var stream = File.OpenRead($"{CurrentPath}\\{fileName}"))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                char[] buffer = new char[200];
                int n = reader.ReadBlock(buffer, 0, 200);

                char[] result = new char[n];

                Array.Copy(buffer, result, n);

                Console.WriteLine(result);
                Console.WriteLine();
            }

            return true;
        }

        public bool FileContainsString(string fileName, string argument)
        {
            if (!File.Exists($"{CurrentPath}\\{fileName}"))
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }

            if (string.IsNullOrWhiteSpace(argument))
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }

            var allFileText = File.ReadAllText($"{CurrentPath}\\{fileName}");
            
            var containStatus = allFileText.Contains(argument);

            OutputHelper.ConsoleFileContainsTextOutput(fileName, argument, containStatus);
            
            return containStatus;
        }

        public bool RenameFile(string fileName, string newFileName)
        {
            if (!File.Exists($"{CurrentPath}\\{fileName}"))
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(newFileName))
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }
            
            File.Move($"{CurrentPath}\\{fileName}", $"{CurrentPath}\\{newFileName}");
            
            return true;
        }
        
        public bool RenameDirectory(string fileName, string newFileName)
        {
            if (!Directory.Exists($"{CurrentPath}\\{fileName}"))
            {
                OutputHelper.ConsoleDirectoryDoesntExistOutput();
                
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(newFileName))
            {
                OutputHelper.ConsoleFileDoesntExistOutput();
                
                return false;
            }
            
            Directory.Move($"{CurrentPath}\\{fileName}", $"{CurrentPath}\\{newFileName}");
            
            return true;
        }
    }
}
