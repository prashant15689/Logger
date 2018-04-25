using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Logging
{
    public class Logger
    {
        private static string LogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + Assembly.GetEntryAssembly().GetName().Name + ".txt";
        protected static readonly object lockObj = new object();
        public static void Info(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter sw = new StreamWriter(LogFile, true))
                {
                    sw.WriteLine(FormatOutput(message));
                    sw.Close();
                }
                CheckFileSize(LogFile);
            }
        }
        public static void Error(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter sw = new StreamWriter(LogFile, true))
                {
                    sw.WriteLine(FormatOutput(message));
                    sw.Close();
                }
                CheckFileSize(LogFile);
            }
        }

        private static string FormatOutput(string message)
        {
            StackFrame[] frames = new StackTrace().GetFrames();
            string className = frames[2].GetMethod().ReflectedType.Name;
            return string.Format("{0} - {1} - {2}", DateTime.Now, className, message);
        }

        private static void CheckFileSize(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Length >= 102400000)
            {
                string sourceDirectory = Path.GetDirectoryName(filePath);
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                string suffix = DateTime.Now.Date.ToString("MM.dd.yyyy");
                string destFileName = Path.Combine(sourceDirectory, filenameWithoutExtension + "_" + suffix + fileExtension);
                File.Move(LogFile, destFileName);
            }
        }
    }
}
