using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class TempFileLog
    {

        private static object _locker = new object();

        public static void Write(string s)
        {
            lock (_locker)
            {
                Directory.CreateDirectory("c:/temp/aspnettests/");
                File.AppendAllText("c:/temp/aspnettests/mylog.txt", $"{DateTime.UtcNow.ToString("O")} : {s}" + Environment.NewLine);
            }
        }
    }
}
