using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaPlayer
{
    public class Ultils
    {
       public static String FormatMediaTime(String oldTime)
        {
            String result="";

            String[] tokens = oldTime.Split(new String[] { ":" }, StringSplitOptions.None);

            if (int.Parse(tokens[0]) == 0)
            {
                result = $"";
            }
            else // hours != 0
            {
                if (int.Parse(tokens[0]) < 10)
                {
                    result = $"0{int.Parse(tokens[0])}:";
                }
                else // hours >=10
                {
                    result = $"{int.Parse(tokens[0])}:";
                }
            }

            if (int.Parse(tokens[1]) == 0)
            {
                result += $"00:";
            }
            else // minutes != 0
            {
                if (int.Parse(tokens[1]) < 10)
                {
                    result += $"0{int.Parse(tokens[1])}:";
                }
                else // minutes >=10
                {
                    result += $"{int.Parse(tokens[1])}:";
                }
            }

            if (int.Parse(tokens[2]) == 0)
            {
                result += $"00";
            }
            else // seconds != 0
            {
                if (int.Parse(tokens[2]) < 10)
                {
                    result += $"0{int.Parse(tokens[2])}";
                }
                else // seconds >=10
                {
                    result += $"{int.Parse(tokens[2])}";
                }
            }

            return result;
        }

        /// <summary>
        /// Copy File to New Foler 
        /// </summary>
        /// <param name="FilePath">absolute file path</param>
        /// <param name="DestinationDirectoryPath">absolute path of destination directory</param>
        public static string copyFileTo(string FilePath, string DestinationDirectoryPath)
        {
            string fileName = Path.GetFileName(FilePath);
            string newFilePath = Path.Combine(DestinationDirectoryPath, fileName);

            if (!File.Exists(newFilePath))
            {
                File.Copy(FilePath, newFilePath);
            }

            return newFilePath;
        }

        public static string appDomain = AppDomain.CurrentDomain.BaseDirectory;
        public static string playListsFolderDomain = Path.Combine(appDomain, "PlayLists");
    }
}
