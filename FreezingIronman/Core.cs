using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezingIronman
{
    class Core
    {
        public static Settings settings;
        static List<Video> videos;

        static void Main(string[] args)
        {
            settings = new Settings();
            videos = FindVideos();
        }

        static List<Video> FindVideos()
        {
            return null;
        }
    }
}
