using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

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

        public static void LogVideo(Process p, Video v)
        {
            var percent = (double)v.OutputSize / (double)v.InputSize;
            var elapsed = p.ExitTime - p.StartTime;
            var s = String.Format("{0:MM/dd/yy  hh:mm:ss tt} ", System.DateTime.Now);
            s += String.Format(" {0:00}.{1:00}.{2:00} ", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
            s += String.Format("{0}", v.InputSizeHuman).PadLeft(10);
            s += String.Format("{0}", v.OutputSizeHuman).PadLeft(10);
            s += String.Format("{0:#0.0%}\r\n", percent).PadLeft(12);
            File.AppendAllText(Core.settings.Logfile, s);
        }

        public static void EncodingFinished(int count)
        {
            String s;
            if (count == 1)
            {
                s = String.Format("Finished converting 1 video\r\n\r\n");
            }
            else
            {
                s = String.Format("Finished converting {0} videos\r\n\r\n", count);
            }
             
            File.AppendAllText(Core.settings.Logfile, s);
        }
    }
}