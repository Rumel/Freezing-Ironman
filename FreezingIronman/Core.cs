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
            var totalVideos = 0;
            while (continueConverting)
            {
                var converted = 0;
                //Removes already converted videos from the list
                videos = RemoveFinished(videos);
                Logger.Log(String.Format("Found {0} video(s)", videos.Count), MessageType.General);
                totalVideos += videos.Count;
                if (settings.Optimize == true)
                {
                    videos = videos.OrderBy(x => x.InputSize).ToList();
                    Logger.Log("Optimized list", MessageType.General);
                }

                foreach (var v in videos)
                {
                    if (!v.AlreadyConverted())
                    {
                        Logger.Log("Starting conversion", MessageType.General);
                        Console.Title = String.Format("Freezing Ironman {0}/{1}", totalConverted, totalVideos);
                        v.CreateOutputPath();
                        var input = String.Format("-i \"{0}\" ", v.InputFullName);
                        var output = String.Format("-o \"{0}\" ", v.FullOutputName);
                        string convertString;
                        if(settings.ExtraArgs != ""){
                            var extra = String.Format(settings.ExtraArgs);
                            convertString = String.Format(" {0} {1} {2}", input, output, extra);
                        }
                        else{
                            var preset = String.Format("-Z \"{0}\"", settings.Preset);
                            convertString = String.Format(" {0} {1} {2}", input, output, preset);
                        }
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo(settings.HandBrakeLocation, convertString)
                        {
                            UseShellExecute = false,
                        };
                        p.Start();
                        p.WaitForExit();
                        Logger.Log("Finished Video", MessageType.General);
                        Logger.LogVideo(p, v);
                        WriteEncodedFile(v, p);
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
            Console.Title = String.Format("Freezing Ironman {0}/{1}", totalConverted, totalVideos);
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
                var errorFile = String.Format("{0}\\ERROR.txt", v.OutputPath);
                if (File.Exists(v.FullOutputName) || File.Exists(errorFile))
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

        static void WriteEncodedFile(Video v, Process p)
        {
            if (settings.WriteEncoded == true)
            {
                var elapsed = p.ExitTime - p.StartTime;
                var elapsedTime = String.Format("{0:00}.{1:00}.{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
                var outputFile = String.Format("{0}\\Encoded.txt", v.OutputPath);
                var data = String.Format("{0}\nInput Size {1}\nOutput Size{2}\nEncoding Time{3}\nEncode Date {4:MM/dd/yy  hh:mm:ss tt}", v.FullOutputName, v.InputSizeHuman, v.OutputSizeHuman, elapsedTime, DateTime.Now);
                File.WriteAllText(outputFile, data, Encoding.UTF8);
            }
        }
    }
}
