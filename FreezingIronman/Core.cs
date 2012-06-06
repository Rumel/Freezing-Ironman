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
        public static Settings settings;
        static List<Video> videos;
        static List<string> Candidates;

        static void Main(string[] args)
        {
            settings = new Settings();
            videos = FindVideos();
            EncodeVideos();
        }

        static List<Video> FindVideos()
        {
            return null;
        }

        static void FindCandidatesRecursively()
        {
            return;
        }

        static void FindCandidates()
        {
            return;
        }

        static void EncodeVideos()
        {

        }

        static bool IsVideoFile(string filename)
        {
            var x = new FileInfo(filename);
            foreach (var ext in settings.Extensions)
            {
                if (x.Extension == ext)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
