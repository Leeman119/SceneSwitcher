using System;
using System.IO;
using System.Reflection;

namespace SceneSwitcher
{
    public static class ErrorLogger
    {
        public static string LogPath { get; } = @"C:\Users\Leandro Teles\source\repos\SceneSwitcher\SceneSwitcherSetup\Release";//Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string LogFile { get; } = "Log.txt";
        public static void Log(string message, Exception error = null)
        {
            string fullPath = $"{LogPath}\\{LogFile}";

            string StackTrace = "";
            if (error != null) { StackTrace = $"\r\n{error.Message}\r\n{error.StackTrace}"; }

            string errorText = $"--LOG: {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()} --\r\n" +
                $"{message}{StackTrace}\r\n________________________________________________________________________________________________\r\n\r\n";

            File.AppendAllText(fullPath, errorText.ToString());
        }
    }
}
