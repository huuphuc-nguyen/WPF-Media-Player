using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for ucRecentPlay.xaml
    /// </summary>
    public partial class ucRecentPlay : UserControl
    {
        public ObservableCollection<Song> RecentPlayList = new ObservableCollection<Song>();

        static string FolderContainAppDomain = AppDomain.CurrentDomain.BaseDirectory;
        string RecentPlayFolderDomain = Path.Combine(FolderContainAppDomain, "RecentPlay");

        int MAX_SIZE = 20;
        public ucRecentPlay()
        {
            InitializeComponent();

            lvRecentPlay.ItemsSource = RecentPlayList;

            // Create Folder
            if (!Directory.Exists(RecentPlayFolderDomain))
            {
                Directory.CreateDirectory(RecentPlayFolderDomain);
            }

            // Read Folder
            string[] files = Directory.GetFiles(RecentPlayFolderDomain);
            if (files.Length != 0)
            {
                foreach (string file in files)
                {
                    RecentPlayList.Add(new Song(file));
                }
            }
        }

        public EventHandler ChangeUserControlEvent;
        private void btnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeUserControlEvent?.Invoke(this, EventArgs.Empty);
            lvRecentPlay.SelectedItem = null;
        }

        public void AddSong(Song newSong)
        {
            if (isContain(newSong))
            {
                // Move the song to the top of list
                removeAt(newSong);
                RecentPlayList.Insert(0, newSong);

                //string[] files = Directory.GetFiles(RecentPlayFolderDomain);
                
                //if (files.Length != 0)
                //{
                //    foreach (string file in files)
                //    {
                //        if (Path.GetFileName(file).Equals(Path.GetFileName(newSong.FilePath)))
                //        {
                //            File.Delete(file);
                //        }
                //    }
                //}

                Ultils.copyFileTo(newSong.FilePath, RecentPlayFolderDomain);
            }
            else
            {
                // This list will contain maximum 20 songs
                if (RecentPlayList.Count() < MAX_SIZE)
                {
                    RecentPlayList.Insert(0, newSong);

                    Ultils.copyFileTo(newSong.FilePath, RecentPlayFolderDomain);
                }
                // When list is full
                else
                {
                    Song removeSong = RecentPlayList[MAX_SIZE - 1];

                    RecentPlayList.Remove(removeSong);
                    RecentPlayList.Insert(0, newSong);

                    string[] files = Directory.GetFiles(RecentPlayFolderDomain);

                    if (files.Length != 0)
                    {
                        foreach (string file in files)
                        {
                            if (Path.GetFileName(file).Equals(Path.GetFileName(removeSong.FilePath)))
                            {
                                File.Delete(file);
                            }
                        }
                    }

                    Ultils.copyFileTo(newSong.FilePath, RecentPlayFolderDomain);
                }
            }
        }

        private bool isContain(Song song)
        {
            bool result = false;

            string[] files = Directory.GetFiles(RecentPlayFolderDomain);

            foreach (string file in files)
            {
                if (Path.GetFileName(file).Equals(Path.GetFileName(song.FilePath)))
                {
                    result = true;
                    return result;
                }
            }

            return result;
        }

        private void removeAt(Song song)
        {
            string name = song.Name;

            for (int i = 0; i < RecentPlayList.Count(); i++)
            {
                if (RecentPlayList[i].Name.Equals(name))
                {
                    RecentPlayList.RemoveAt(i);
                }
            }
        }

        public delegate void ChangeAddSongHandler(String FilePath);
        public event ChangeAddSongHandler RecentPlayAddSong;

        public EventHandler ChangeUserControlEvent2;

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Important to convert into array, because listView.SelectedItems.Count can be affected
            // when one song of the list is used in process
            Song[] songs = lvRecentPlay.SelectedItems.Cast<Song>().ToArray();

            for (int i = 0; i < songs.Length; i++)
            {
                RecentPlayAddSong?.Invoke(songs[i].FilePath);
            }

            ChangeUserControlEvent2?.Invoke(this, EventArgs.Empty);
            lvRecentPlay.SelectedItem = null;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Delete UI
            RecentPlayList.Clear();

            // Delete files
            Task.Run(() =>
            {
                string[] files = Directory.GetFiles(RecentPlayFolderDomain);

                if (files.Length != 0)
                {
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
            });
        }
    }
}
