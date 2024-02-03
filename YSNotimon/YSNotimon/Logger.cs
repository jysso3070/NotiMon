using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace YSNotimon
{
    class Logger
    {
        static Object logLock = new Object();

        static string directoryPath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, @"\Log");
        static string fileName = "log.log";
        static string filePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, @"\Log\log.log");

        static bool consoleWrite = true;

        public static void SetConsoleWrite(bool value)
        {
            consoleWrite = value;
        }

        public static void SetDirectoryPath(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath) == true)
                return;

            SetFilePath(dirPath, fileName);
        }

        public static void SetFileName(string name)
        {
            if (string.IsNullOrEmpty(name) == true)
                return;

            SetFilePath(directoryPath, name);
        }

        public static void SetFilePath(string dirPath, string name)
        {
            if (string.IsNullOrEmpty(name) == true || string.IsNullOrEmpty(dirPath) == true)
                return;


            directoryPath = dirPath;
            fileName = name;
            filePath = string.Format(@"{0}\{1}", directoryPath, fileName);
        }

        public static void Log(string text)
        {
            if (string.IsNullOrEmpty(text) == true)
                return;

            string log = string.Format("[{0}]\n{1}\n", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), text);

            if (consoleWrite == true)
                Console.WriteLine(log);

            if (Directory.Exists(directoryPath) == false)
            {
                lock (logLock)
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            lock (logLock)
            {
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine(log);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(log);
                    }
                }
            }
        }

        public static void LogI(string text)
        {
            if (string.IsNullOrEmpty(text) == true)
                return;

            string log = string.Format("[INFO] {0}", text);

            Log(log);
        }

        public static void LogE(string text)
        {
            if (string.IsNullOrEmpty(text) == true)
                return;

            string log = string.Format("[ERROR] {0}", text);

            Log(log);
        }

        public static void LogEx(Exception e)
        {
            if (string.IsNullOrEmpty(e.ToString()) == true)
                return;

            string log = string.Format("[EXCEPTION] msg: {0}\nstack: {1}", e.Message, e.StackTrace);

            Log(log);
        }
    }
}
