using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezingIronman
{
    public class Settings
    {
        //Should have default
        private string _preset;
        public string Preset
        {
            get
            {
                return _preset;
            }
        }

        //Should have default
        private string _logfile;
        public string Logfile
        {
            get
            {
                return _logfile;
            }
        }

        //Necessary
        private string _inputDirectory;
        public string InputDirectory
        {
            get
            {
                return _inputDirectory;
            }
        }

        //Necessary
        private string _outputDirectory;
        public string OutputDirectory
        {
            get
            {
                return _outputDirectory;
            }
        }

        //Defaults to false
        private bool _loop = false;
        public bool Loop
        {
            get
            {
                return _loop;
            }
        }

        //Defaults to false
        private bool _optimize = false;
        public bool Optimize
        {
            get
            {
                return _optimize;
            }
        }

        //Defaults to false
        private bool _recursive = false;
        public bool Recursive
        {
            get
            {
                return _recursive;
            }
        }

        //Necessary
        private string[] _extensions;
        public string[] Extensions
        {
            get
            {
                return _extensions;
            }
        }

        //Necessary
        private string _handBrakeLocation;
        public string HandBrakeLocation
        {
            get
            {
                return _handBrakeLocation;
            }
        }

        public Settings()
        {

        }
    }
}
