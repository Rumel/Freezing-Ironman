using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FreezingIronman
{
    public static class Core
    {
        public static Settings settings;
        static List<Video> videos = new List<Video>();

        static void Main(string[] args)
        {
            settings = new Settings();
            
            //Find possible video candidates
            if (settings.Recursive)
            {
                FindVideosRecursively(new DirectoryInfo(settings.InputDirectory));
                FindVideos(new DirectoryInfo(settings.InputDirectory));
            }
            else
            {
                FindVideos(new DirectoryInfo(settings.InputDirectory));
            }
            EncodeVideos();
            Console.ReadLine();
        }

        static void FindVideosRecursively(DirectoryInfo dir)
        {
            foreach (var d in dir.GetDirectories())
            {
                FindVideosRecursively(d);
                foreach (var f in d.GetFiles())
                {
                    if (IsVideoFile(f))
                    {
                        videos.Add(new Video(f));
                    }
                }
            }
        }

        static void FindVideos(DirectoryInfo dir)
        {
            foreach (var f in dir.GetFiles())
            {
                if (IsVideoFile(f))
                {
                    videos.Add(new Video(f));
                }
            }
        }

        static void EncodeVideos()
        {
            var continueConverting = true;

            while (continueConverting)
            {
                var converted = 0;
                if (settings.Optimize == true)
                {
                    videos = videos.OrderBy(x => x.InputSize).ToList();
                }

                foreach (var v in videos)
                {
                    if (!v.AlreadyConverted())
                    {
                        v.CreateOutputPath();
                        var input = String.Format("-i \"{0}\" ", v.InputFullName);
                        var output = String.Format("-o \"{0}\" ", v.FullOutputName);
                        var preset = String.Format("-Z {0}", settings.Preset);
                        var convertString = String.Format(" {0} {1} {2}", input, output, preset);
                        //Converting is not working correctly yet.
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo(settings.HandBrakeLocation, convertString)
                        {
                            UseShellExecute = false,
                        };
                        p.Start();
                        p.WaitForExit();
                        converted++;
                    }
                }

                if (settings.Loop == true)
                {
                    if (converted == 0)
                    {
                        continueConverting = false;
                    }
                }
                else
                {
                    continueConverting = false;
                }
            }
        }

        static bool IsVideoFile(FileInfo f)
        {
            foreach (var ext in settings.Extensions)
            {
                if (f.Extension == ext)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
