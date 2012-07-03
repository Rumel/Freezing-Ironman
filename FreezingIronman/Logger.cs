using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FreezingIronman
{
    public enum MessageType
    {
        General,
        Error
    }

    static public class Logger
    {
        const string LOG = "rolling.log";

        public static void Log(string message, MessageType type)
        {
            if (type == MessageType.Error)
            {
                var s = String.Format("ERROR {0:MM/dd/yy hh:mm:ss tt} {1}\r\n", System.DateTime.Now.ToString(), message);
                File.AppendAllText(LOG, s);
            }
            else
            {
                var s = String.Format("{0:MM/dd/yy hh:mm:ss tt} {1}\r\n", System.DateTime.Now.ToString(), message);
                File.AppendAllText(LOG, s);
            }
        }

        public static void LogVideo()
        {

        }

        public static void EncodingFinished()
        {

        }
    }
}
