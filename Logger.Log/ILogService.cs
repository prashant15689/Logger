using System;

namespace Logger.Log
{
    public interface ILogService : IDisposable
    {
        void Fatal(string message);
        void Error(string message);
        void Warn(string message);
        void Info(string message);
        void Debug(string message);
    }
}
