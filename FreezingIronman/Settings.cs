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
        private string _preset = "Normal";
        public string Preset
        {
            get
            {
                return this._preset;
            }
        }

        //Should have default
        private string _logfile;
        public string Logfile
        {
            get
            {
                return this._logfile;
            }
        }

        //Necessary
        private string _inputDirectory;
        public string InputDirectory
        {
            get
            {
                return this._inputDirectory;
            }
        }

        //Necessary
        private string _outputDirectory;
        public string OutputDirectory
        {
            get
            {
                return this._outputDirectory;
            }
        }

        //Defaults to false
        private bool _loop = false;
        public bool Loop
        {
            get
            {
                return this._loop;
            }
        }

        //Defaults to false
        private bool _optimize = false;
        public bool Optimize
        {
            get
            {
                return this._optimize;
            }
        }

        //Defaults to false
        private bool _recursive = false;
        public bool Recursive
        {
            get
            {
                return this._recursive;
            }
        }

        //Necessary
        private string[] _extensions;
        public string[] Extensions
        {
            get
            {
                return this._extensions;
            }
        }

        //Necessary
        private string _handBrakeLocation;
        public string HandBrakeLocation
        {
            get
            {
                return this._handBrakeLocation;
            }
        }

        //Defualts to m4v
        private string _outputExt = "m4v";
        public string OutputExt
        {
            get
            {
                return this._outputExt;
            }
        }

        //Extra Arguments
        private string _extraArgs = "";
        public string ExtraArgs
        {
            get
            {
                return this._extraArgs;
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
                        var index = l.IndexOf("=");
                        var argument = l.Substring(0,index);
                        var data = l.Substring(index + 1);
                        switch(argument){
                            case "HandBrakeLocation":
                                if (data != "")
                                {
                                    this._handBrakeLocation = data;
                                }
                                else
                                {
                                    Console.WriteLine("HandBrakeLocation setting not found");
                                    Logger.Log("HandBrakeLocation setting not found", MessageType.General);
                                }
                                break;
                            case "InputDirectory":
                                if (data!= "")
                                {
                                    this._inputDirectory = data;
                                }
                                else
                                {
                                    Console.WriteLine("InputDirectory setting not found");
                                    Logger.Log("InputDirectory setting not found", MessageType.General);
                                }
                                break;
                            case "OutputDirectory":
                                if (data != "")
                                {
                                    this._outputDirectory = data;
                                }
                                else
                                {
                                    Console.WriteLine("OutputDirectory setting not found");
                                    Logger.Log("OutputDirectory setting not found", MessageType.General);
                                }
                                break;
                            case "Preset":
                                if (data != "")
                                {
                                    this._preset = data;
                                }
                                break;
                            case "Logfile":
                                if (data != "")
                                {
                                    this._logfile = data;
                                }
                                break;
                            case "Loop":
                                if (data.ToLower() == "true")
                                {
                                    this._loop = true;
                                }
                                break;
                            case "Optimize":
                                if (data.ToLower() == "true")
                                {
                                    this._optimize = true;
                                }
                                break;
                            case "Recursive":
                                if (data.ToLower() == "true")
                                {
                                    this._recursive = true;
                                }
                                break;
                            case "Extensions":
                                if (data != "")
                                {
                                    this._extensions = data.ToLower().Split(' ');
                                }
                                else
                                {
                                    Console.WriteLine("Valid video extensions not found");
                                    Logger.Log("Valid video extensions not found", MessageType.General);
                                }
                                break;
                            case "OutputExt":
                                if (data != "")
                                {
                                    this._outputExt = data;
                                }
                                break;
                            case "ExtraArgs":
                                if (data != "")
                                {
                                    this._extraArgs = data;
                                }
                                break;
                            default:
                                Logger.Log(String.Format("Invalid setting: {0}", l), MessageType.General);
                                break;
                        }
                    }
                }
                ValidateSettings();
            }
            catch (FileNotFoundException)
            {
                Logger.Log("Settings file was not found", MessageType.General);
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
            Logger.Log("Created Settings.txt template", MessageType.General);
        }
    }
}
