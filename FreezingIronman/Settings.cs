using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezingIronman
{
    public class Settings
    {
        private string _preset;
        public string Preset
        {
            get
            {
                return _preset;
            }
        }

        private string _logfile;
        public string Logfile
        {
            get
            {
                return _logfile;
            }
        }

        private string _inputDirectory;
        public string InputDirectory
        {
            get
            {
                return _inputDirectory;
            }
        }

        private string _outputDirectory;
        public string OutputDirectory
        {
            get
            {
                return _outputDirectory;
            }
        }

        private bool _loop;
        public bool Loop
        {
            get
            {
                return _loop;
            }
        }

        private bool _optimize;
        public bool Optimize
        {
            get
            {
                return _optimize;
            }
        }


        public Settings()
        {

        }
    }
}
