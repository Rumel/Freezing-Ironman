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
            Console.Title = "Freezing Ironman";
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
            var totalConverted = 0;
            while (continueConverting)
            {
                var converted = 0;
                //Removes already converted videos from the list
                videos = RemoveFinished(videos);
                Console.Title = String.Format("Freezing Ironman {0}/{1}", totalConverted, videos.Count);
                if (settings.Optimize == true)
                {
                    videos = videos.OrderBy(x => x.InputSize).ToList();
                }

                foreach (var v in videos)
                {
                    if (!v.AlreadyConverted())
                    {
                        Console.Title = String.Format("Freezing Ironman {0}/{1}", converted, videos.Count);
                        v.CreateOutputPath();
                        var input = String.Format("-i \"{0}\" ", v.InputFullName);
                        var output = String.Format("-o \"{0}\" ", v.FullOutputName);
                        var preset = String.Format("-Z \"{0}\"", settings.Preset);
                        var convertString = String.Format(" {0} {1} {2}", input, output, preset);
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo(settings.HandBrakeLocation, convertString)
                        {
                            UseShellExecute = false,
                        };
                        p.Start();
                        p.WaitForExit();
                        Logger.LogVideo(p, v);
                        converted++;
                        totalConverted++;
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
            Logger.EncodingFinished(totalConverted);
        }

        static bool IsVideoFile(FileInfo f)
        {
            foreach (var ext in settings.Extensions)
            {
                if (f.Extension.ToLower() == ext.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        static List<Video> RemoveFinished(List<Video> vids)
        {
            List<Video> toRemove = new List<Video>();
            foreach(var v in vids){
                if (File.Exists(v.FullOutputName))
                {
                    toRemove.Add(v);
                }
            }

            foreach (var v in toRemove)
            {
                vids.Remove(v);
                Console.WriteLine("Removed {0}", v.InputFullName);
            }

            return vids;
        }
    }
}
