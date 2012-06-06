using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FreezingIronman
{
    class Core
    {
        public static Settings Settings;
        static List<Video> videos;
        static List<FileInfo> Candidates = new List<FileInfo>();

        static void Main(string[] args)
        {
            Settings = new Settings();
            
            //Find possible video candidates
            if (Settings.Recursive)
            {
                FindCandidatesRecursively(new DirectoryInfo(Settings.InputDirectory));
            }
            else
            {
                FindCandidates(new DirectoryInfo(Settings.InputDirectory));
            }
            videos = FindVideos();
            EncodeVideos();
        }

        static List<Video> FindVideos()
        {
            return null;
        }

        static void FindCandidatesRecursively(DirectoryInfo dir)
        {
            foreach (var d in dir.GetDirectories())
            {
                FindCandidatesRecursively(d);
                foreach (var f in d.GetFiles())
                {
                    if (IsVideoFile(f))
                    {
                        Candidates.Add(f);
                    }
                }
            }
        }

        static void FindCandidates(DirectoryInfo dir)
        {
            foreach (var f in dir.GetFiles())
            {
                if (IsVideoFile(f))
                {
                    Candidates.Add(f);
                }
            }
        }

        static void EncodeVideos()
        {

        }

        static bool IsVideoFile(FileInfo f)
        {
            foreach (var ext in Settings.Extensions)
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
