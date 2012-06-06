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
        public FileInfo Info
        {
            get
            {
                return _info;
            }
        }

        public string OutputPath
        {
            get
            {
                return Info.DirectoryName.ToString().Replace(Core.settings.InputDirectory, Core.settings.OutputDirectory);
            }
        }

        public string FullOutputName
        {
            get
            {
                return String.Format("{0}\\{1}", this.OutputPath, this.Info.Name.Replace(this.Info.Extension, Core.settings.OutputExt));
            }
        }

        public bool AlreadyConverted()
        {
            if (File.Exists(FullOutputName))
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
