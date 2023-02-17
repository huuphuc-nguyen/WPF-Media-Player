using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    //public class Playlist
    //{
    //    public String Name { get; set; }
    //    public String FilePath { get; set; }

    //    public Song[] Songs;

    //    public Playlist(String nName)
    //    {
    //        Name = nName;

    //        FilePath = Path.Combine(Ultils.playListsFolderDomain, nName);
    //    }
    //}
    public class Playlist : INotifyPropertyChanged
    {
        // Name Property
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    FilePath = Path.Combine(Ultils.playListsFolderDomain, value);
                    OnPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // File path Property
        public string FilePath { get; set; }

        // List Of Songs
        public ObservableCollection<Song> Songs = new ObservableCollection<Song>();

        // Constructor with Name parameter
        public Playlist(string nName)
        {
            Name = nName;
            FilePath = Path.Combine(Ultils.playListsFolderDomain, nName);
        }
    }

}
