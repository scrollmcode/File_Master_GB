using System;
using System.Collections.Generic;
using System.Text;
using File_Master_GB.Logic;
using System.Text.Json;
using System.IO;

namespace File_Master_GB.UI
{
    /// <summary>
    /// Класс работы с файлами через консоль
    /// </summary>
    public class FilesConsoleUI
    {
        static string configDir = ".\\Config";
        static string configPath = ".\\Config\\config.json";
        static string[] FilesArray { get; set; }
        static int PageLength { get; set; } = 10; // количествой файлов на странице
        static int CurrentPage { get; set; } // номер файла
        static string CurrentDirectory { get; set; }

        /// <summary>
        /// Вывод файлов
        /// </summary>
        private static void ShowFiles()
        {
            if (FilesArray == null || CurrentPage >= FilesArray.Length) { return; }

            int i = CurrentPage;

            for (int j = 0; j < PageLength && i < FilesArray.Length; i++)
            {
                j++;
                Console.WriteLine(FilesArray[i]);
            }
            CurrentPage = i;

            if (FilesArray.Length > CurrentPage)
            {
                Console.WriteLine("More files.");
            }
            Console.WriteLine(Environment.NewLine);
        }

        /// <summary>
        /// Показать файлы в каталоге
        /// </summary>
        /// <param name="path"></param>
        private static void ShowNewFiles(string path)
        {
            FilesArray = FilesLogic.GetFiles(path);
            CurrentPage = 0;
            ShowFiles();
        }

        /// <summary>
        /// показать следующие файлы
        /// </summary>
        /// <param name="path"></param>
        private static void ShowNextFiles()
        {
            ShowFiles();
        }

        private static string ShowCurrentDirName()
        {
            return CurrentDirectory;
        }

        private static void CopyFile(string source, string dest)
        {
            FilesLogic.CopyFile(source, dest);
        }

        private static void MoveFile(string source, string dest)
        {
            FilesLogic.MoveFile(source, dest);
        }

        private static void DeleteFile(string filePath)
        {
            FilesLogic.DeleteFile(filePath);
        }

        private static void GetFileInfo(string filePath)
        {
            var fileInfo = FilesLogic.GetFileInfo(filePath);
            Console.WriteLine(fileInfo + Environment.NewLine);
        }

        public static void RunCommand(Cmd command)
        {
            string source;
            string dest;
            switch (command)
            {
                case Cmd.dir:
                    Console.WriteLine("Содержимое");
                    Console.WriteLine("Введите путь");
                    string path = Console.ReadLine();
                    if (string.IsNullOrEmpty(path))
                    {
                        ShowNewFiles(CurrentDirectory);
                    }
                    else
                    {
                        CurrentDirectory = path;
                        ShowNewFiles(path);
                    }
                    break;
                case Cmd.dir_name:
                    Console.WriteLine("Текущая директория: " + ShowCurrentDirName());
                    break;
                case Cmd.next:
                    ShowNextFiles();
                    break;
                case Cmd.cp:
                    Console.WriteLine("Копирование");
                    Console.WriteLine("Введите название файла");
                    source = Console.ReadLine();
                    Console.WriteLine("Введите назначение копирования файла");
                    dest = Console.ReadLine();
                    CopyFile(source, dest);
                    break;
                case Cmd.mv:
                    Console.WriteLine("Перемещение");
                    Console.WriteLine("Введите название файла");
                    source = Console.ReadLine();
                    Console.WriteLine("Введите назначение копирования файла");
                    dest = Console.ReadLine();
                    MoveFile(source, dest);
                    break;
                case Cmd.rm:
                    Console.WriteLine("Удаление");
                    Console.WriteLine("Введите название файла");
                    source = Console.ReadLine();
                    DeleteFile(source);
                    break;
                case Cmd.info:
                    Console.WriteLine("Информация");
                    Console.WriteLine("Введите название файла");
                    source = Console.ReadLine();
                    GetFileInfo(source);
                    break;
                case Cmd.exit:
                    Console.WriteLine("Выход");
                    string jsonData = JsonSerializer.Serialize<Config>(new Config()
                    { PageLength = PageLength, CurrentDirectory = CurrentDirectory });
                    File.WriteAllText(configPath, jsonData);
                    return;
            }
        }

        public static void ConfigProgram()
        {
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }
            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath,@"{""PageLength"":10,""Directory"":""""}"); // default config
            }

            string jsonText = File.ReadAllText(configPath);
            Config config = JsonSerializer.Deserialize<Config>(jsonText);
            PageLength = config.PageLength;
            CurrentDirectory = config.CurrentDirectory;

        }

        public class Config
        {
            public int PageLength { get; set; }
            public string CurrentDirectory { get; set; }
        }

        public enum Cmd
        {
            help,
            exit,
            dir,
            dir_name,
            next,
            cp,
            mv,
            rm,
            info
        }
    }
}
