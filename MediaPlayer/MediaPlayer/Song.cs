using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    public class Song
    {
        public String Name { get; set; }
        public String FilePath { get; set; }
        public String Singer { get; set; }

        public Song(String nFilePath)
        {
            FilePath = nFilePath;

            var info = new FileInfo(nFilePath);
            Name = info.Name;

            Singer = "";
        }
    }
}
