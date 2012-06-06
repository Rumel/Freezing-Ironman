using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            }
            else
            {
                FindVideos(new DirectoryInfo(settings.InputDirectory));
            }

            foreach (var v in videos)
            {
                Console.WriteLine("{0}\n{1}\n\n", v.Info.FullName, v.FullOutputName);
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
