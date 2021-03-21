using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace File_Master_GB.Logic
{
    /// <summary>
    /// Класс для работы с файлами
    /// </summary>
    public class FilesLogic
    {
        public static string[] GetFiles(string path)
        {
            string[] result = new string[0];

            try
            {
                string[] dirs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
                result = new string[dirs.Length + files.Length];

                for (int i = 0; i < dirs.Length; i++)
                {
                    result[i] = dirs[i];
                }
                for (int j = 0; j < files.Length; j++)
                {
                    result[dirs.Length + j] = files[j];
                }
            }
            catch (Exception e)
            {
                string message = "Ошибка при получении списка файлов:" + Environment.NewLine +
                    e.Message + Environment.NewLine;

                Console.WriteLine(message);
                WriteExceptionLog(message);
            }

            return result;
        }

        public static string GetCurrentDirName()
        {
            return Environment.CurrentDirectory;
        }

        public static void CopyFile(string source, string dest)
        {
            try
            {
                if (File.Exists(source))
                {
                    string dirName = new FileInfo(dest).DirectoryName;
                    if (!Directory.Exists(dirName))
                    {
                        Directory.CreateDirectory(dirName);
                    }
                    File.Copy(source, dest, true);
                }
                else if (Directory.Exists(source))
                {
                    if (!Directory.Exists(dest))
                    {
                        Directory.CreateDirectory(dest);
                    }

                    string[] files = Directory.GetFiles(source);

                    for (int i = 0; i < files.Length; i++)
                    {
                        string fileName = new FileInfo(files[i]).Name;
                        File.Copy(files[i], Path.Combine(dest, fileName), true);
                    }
                }
                else
                {
                    Console.WriteLine("Файл не найден.");
                }
            }
            catch (Exception e)
            {
                string message = "Ошибка при копировании:" + Environment.NewLine +
                    e.Message + Environment.NewLine;

                Console.WriteLine(message);
                WriteExceptionLog(message);
            }
        }

        public static void MoveFile(string source, string dest)
        {
            try
            {
                if (File.Exists(source))
                {
                    string dirName = new FileInfo(dest).DirectoryName;
                    if (!Directory.Exists(dirName))
                    {
                        Directory.CreateDirectory(dirName);
                    }
                    File.Move(source, dest, true);
                }
                else if (Directory.Exists(source))
                {
                    if (!Directory.Exists(dest))
                    {
                        Directory.CreateDirectory(dest);
                    }

                    string[] files = Directory.GetFiles(source);

                    for (int i = 0; i < files.Length; i++)
                    {
                        string fileName = new FileInfo(files[i]).Name;
                        File.Move(files[i], Path.Combine(dest, fileName), true);
                    }
                }
                else
                {
                    Console.WriteLine("Файл не найден.");
                }
            }
            catch (Exception e)
            {
                string message = "Ошибка при перемещении:" + Environment.NewLine +
                    e.Message + Environment.NewLine;

                Console.WriteLine(message);
                WriteExceptionLog(message);
            }
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else if (Directory.Exists(filePath))
                {
                    Directory.Delete(filePath, true);
                }
            }
            catch (Exception e)
            {
                string message = "Ошибка при удалении:" + Environment.NewLine +
                    e.Message + Environment.NewLine;
                Console.WriteLine(message);
                WriteExceptionLog(message);
            }
        }

        public static string GetFileInfo(string filePath)
        {
            StringBuilder result = new StringBuilder();

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                result.Append("Имя\t\t" + fileInfo.Name + Environment.NewLine);
                result.Append("Папка\t\t" + fileInfo.DirectoryName + Environment.NewLine);
                result.Append("Расширение\t" + fileInfo.Extension + Environment.NewLine);
                result.Append("Дата создания\t" + fileInfo.CreationTime + Environment.NewLine);
                result.Append("Размер(kb) \t" + (double)fileInfo.Length / 1024 + Environment.NewLine);
            }
            catch (Exception e)
            {
                string message = "Ошибка при получении информации о файле:" + Environment.NewLine +
                    e.Message + Environment.NewLine;

                Console.WriteLine(message);
                WriteExceptionLog(message);
            }
            return result.ToString();
        }

        private static void WriteExceptionLog(string message)
        {
            string errorDir = @".\Errors\";
            string errorPath = string.Concat(errorDir, "FilesError_", DateTime.Today.ToShortDateString(), ".txt");

            if (!Directory.Exists(errorDir))
            {
                Directory.CreateDirectory(errorDir);
            }
            if (!File.Exists(errorPath))
            {
                File.WriteAllText(errorPath, message);
            }
            else
            {
                File.AppendAllText(errorPath, message);
            }

        }
    }
}
