using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Logger.Info("Test info");
            Logger.Error("Test error");
        }
    }
}
