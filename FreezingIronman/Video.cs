using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FreezingIronman
{
    public class Video
    {
        private FileInfo _info;

        public string InputFullName
        {
            get
            {
                return this._info.FullName;
            }
        }

        public long InputSize
        {
            get
            {
                if (File.Exists(this.InputFullName))
                {
                    return this._info.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string InputSizeHuman
        {
            get
            {
                return this.HumanReadeable(this.InputSize);
            }
        }

        public long OutputSize
        {
            get
            {
                if (File.Exists(this.FullOutputName))
                {
                    return new FileInfo(this.FullOutputName).Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string OutputSizeHuman
        {
            get
            {
                return this.HumanReadeable(this.OutputSize);
            }
        }

        private string HumanReadeable(long bytes)
        {
            if (bytes != 0)
            {
                string[] suf = { "B", "KB", "MB", "GB", "TB", "PB" };
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                var readable = String.Format("{0:#0.0} {1}", num, suf[place]);
                return readable;
            }
            else
            {
                var fileName = String.Format("{0}\\ERROR.txt", this.OutputPath);
                File.WriteAllText(fileName, "ERRORED", Encoding.UTF8);
                Logger.Log(String.Format("The conversion of {0} seems to have gone wrong", this.InputFullName), MessageType.Error);
                return "NaN";
            }
        }

        public string OutputPath
        {
            get
            {
                return this._info.DirectoryName.ToString().Replace(Core.settings.InputDirectory, Core.settings.OutputDirectory);
            }
        }

        public void CreateOutputPath()
        {
            Directory.CreateDirectory(this.OutputPath);
        }

        public string FullOutputName
        {
            get
            {
                return String.Format("{0}\\{1}", this.OutputPath, this._info.Name.Replace(this._info.Extension, Core.settings.OutputExt));
            }
        }

        public bool AlreadyConverted()
        {
            if (File.Exists(this.FullOutputName))
            {
                return true;
            }
            return false;
        }

        public Video(FileInfo fi)
        {
            this._info = fi;
        }
    }
}
