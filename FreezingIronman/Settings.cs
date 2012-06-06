using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        //Defualts to m4v
        private string _outputExt = "m4v";
        public string OutputExt
        {
            get
            {
                return _outputExt;
            }
        }

        public Settings()
        {
            try
            {
                var lines = File.ReadAllLines("Settings.txt");
                foreach (var l in lines)
                {
                    if (l.Length > 0 && l[0] != '#')
                    {
                        var li = l.Split('=');
                        if(li.Count() == 2){
                            switch(li[0]){
                                case "HandBrakeLocation":
                                    if (li[1] != "")
                                    {
                                        _handBrakeLocation = li[1];
                                    }
                                    else
                                    {
                                        Console.WriteLine("HandBrakeLocation setting not found");
                                        Logger.Log("HandBrakeLocation setting not found");
                                    }
                                    break;
                                case "InputDirectory":
                                    if (li[1] != "")
                                    {
                                        _inputDirectory = li[1];
                                    }
                                    else
                                    {
                                        Console.WriteLine("InputDirectory setting not found");
                                        Logger.Log("InputDirectory setting not found");
                                    }
                                    break;
                                case "OutputDirectory":
                                    if (li[1] != "")
                                    {
                                        _outputDirectory = li[1];
                                    }
                                    else
                                    {
                                        Console.WriteLine("OutputDirectory setting not found");
                                        Logger.Log("OutputDirectory setting not found");
                                    }
                                    break;
                                case "Preset":
                                    if (li[1] != "")
                                    {
                                        _preset = li[1];
                                    }
                                    break;
                                case "Logfile":
                                    if (li[1] != "")
                                    {
                                        _logfile = li[1];
                                    }
                                    break;
                                case "Loop":
                                    if (li[1].ToLower() == "true")
                                    {
                                        _loop = true;
                                    }
                                    break;
                                case "Optimize":
                                    if (li[1].ToLower() == "true")
                                    {
                                        _loop = true;
                                    }
                                    break;
                                case "Recursive":
                                    if (li[1].ToLower() == "true")
                                    {
                                        _loop = true;
                                    }
                                    break;
                                case "Extensions":
                                    if (li[1] != "")
                                    {
                                        _extensions = li[1].ToLower().Split(' ');
                                    }
                                    else
                                    {
                                        Console.WriteLine("Valid video extensions not found");
                                        Logger.Log("Valid video extensions not found");
                                    }
                                    break;
                                case "OutputExt":
                                    if (li[1] != "")
                                    {
                                        _outputExt = li[1];
                                    }
                                    break;
                                default:
                                    Logger.Log(String.Format("Invalid setting: {0}", l));
                                    break;
                            }
                        }
                    }
                }
                ValidateSettings();
            }
            catch (FileNotFoundException)
            {
                Logger.Log("Settings file was not found");
                Console.WriteLine("Settings file not found, the template will be created for you");
                CreateSettingsFile();
            }
        }

        void ValidateSettings()
        {

        }

        //Creates the settings file if its not found
        void CreateSettingsFile()
        {
            File.WriteAllText("Settings.txt",@"# This is the settings file for FreezingIronman
# Lines beginning with '#' symbols are comments

# Location of HandBrakeCLI.exe
# Most likely is C:\Program Files\Handbrake\HandBrakeCLI.exe
HandBrakeLocation=
# Directory to look for input files
InputDirectory=
# Directory to output those files
OutputDirectory=
# Preset to use with Handbrake if none is specified Normal will be used
Preset=
# Location of logfile if you would like one to be kept (optional)
Logfile=
# This determines whether to loop over the files after converting to see if
# any new ones have been added to the directory. true or false, false by default.
Loop=
# This option will convert the smaller files first if set to true, false by default
Optimize=
# Will scan recursively in the input directory for video files
# and convert them. false by default
Recursive=
# List of extensions that are video files seperated by a space
# ex. Extensions=.mp4 .m4v .mkv .mpg
Extensions=
# Extension that will be applied on output. mkv or m4v, m4v by default.
OutputExt=", Encoding.UTF8);
            Logger.Log("Created Settings.txt template");
        }
    }
}
