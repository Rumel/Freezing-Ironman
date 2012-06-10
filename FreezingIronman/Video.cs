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

        public string InputPath
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
                if (File.Exists(this.InputPath))
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
                if (File.Exists(this.OutputPath))
                {
                    return new FileInfo(this.OutputPath).Length;
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
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB" };
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            string readable = num.ToString() + suf[place];
            return readable;
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
