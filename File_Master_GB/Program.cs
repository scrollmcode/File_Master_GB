using System;
using File_Master_GB.Logic;
using File_Master_GB.UI;

namespace File_Master_GB
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string helpString = @"HELP - помощь, EXIT - выход, 
DIR - содержимое, NEXT - следующая страница, DIR_NAME - текущая директория
CP - копировать, MV - переместить, RM - удалить, INFO - информация о файле";

            FilesConsoleUI.ConfigProgram();
            FilesConsoleUI.Cmd userCmdCode = FilesConsoleUI.Cmd.help;
            string userCmd = string.Empty;

            while (userCmdCode != FilesConsoleUI.Cmd.exit)
            {

                if (string.Equals(userCmd.ToUpper(), "HELP") || string.Equals(userCmd.ToUpper(), "СПРАВКА"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.help;
                }
                else if (string.Equals(userCmd.ToUpper(), "EXIT") || string.Equals(userCmd.ToUpper(), "ВЫХОД"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.exit;
                }
                else if (string.Equals(userCmd.ToUpper(), "DIR") || string.Equals(userCmd.ToUpper(), "КАТ"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.dir;
                }
                else if (string.Equals(userCmd.ToUpper(), "DIR_NAME") || string.Equals(userCmd.ToUpper(), "КАТ_ИМЯ"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.dir_name;
                }
                else if (string.Equals(userCmd.ToUpper(), "NEXT") || string.Equals(userCmd.ToUpper(), "СЛЕД"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.next;
                }
                else if (string.Equals(userCmd.ToUpper(), "CP") || string.Equals(userCmd.ToUpper(), "КОП"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.cp;
                }
                else if (string.Equals(userCmd.ToUpper(), "MV") || string.Equals(userCmd.ToUpper(), "ПЕР"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.mv;
                }
                else if (string.Equals(userCmd.ToUpper(), "RM") || string.Equals(userCmd.ToUpper(), "УД"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.rm;
                }
                else if (string.Equals(userCmd.ToUpper(), "INFO") || string.Equals(userCmd.ToUpper(), "ИНФО"))
                {
                    userCmdCode = FilesConsoleUI.Cmd.info;
                }
                else
                {
                    userCmdCode = FilesConsoleUI.Cmd.help;
                }

                // Выполнение команд
                switch ((int)userCmdCode)
                {
                    case (int)FilesConsoleUI.Cmd.help: Console.WriteLine(helpString, Environment.NewLine); break;
                    default: FilesConsoleUI.RunCommand(userCmdCode); break;
                }

                userCmd = Console.ReadLine();
            }
        }

        public static void ConfigProgram()
        {
            string config = string.Empty;
        }
    }
}
