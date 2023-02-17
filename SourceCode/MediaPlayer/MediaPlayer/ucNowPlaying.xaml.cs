using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TagLib;


namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for ucNowPlaying.xaml
    /// </summary>
    public partial class ucNowPlaying : UserControl
    {
        public String totalTime = "";

        Storyboard storyboard = new Storyboard();

        Playlist currentPlayList = null;

        public ucNowPlaying()
        {
            InitializeComponent();

            DoubleAnimation animation = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(10)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            storyboard.Children.Add(animation);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            Storyboard.SetTarget(animation, ellipseAvatar);

            // set default Avatar
            SetUpDefaultAvatar();
        }

        public void LoadTotalTime(Song Song)
        {
            // If song is now Playing
            if (mediaPlayer.Source != null && mediaPlayer.Source.LocalPath == Song.FilePath)
            {
                return;
            }

            mediaPlayer.Source = new Uri(Song.FilePath, UriKind.Absolute);
            mediaPlayer.Play();
            mediaPlayer.Stop();

            ////System.Threading.Thread.Sleep(500);
            System.Threading.Thread.Sleep(800);

            double hours = mediaPlayer.NaturalDuration.TimeSpan.Hours;
            double minutes = mediaPlayer.NaturalDuration.TimeSpan.Minutes;
            double seconds = mediaPlayer.NaturalDuration.TimeSpan.Seconds;

            totalTime = $"{hours}:{minutes}:{ seconds}";
        }


        public delegate void MediaEndedHandler();
        public event MediaEndedHandler MediaEnded;

        private void mediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Source = null;
            MediaEnded?.Invoke();
        }

        public void StartAvatarMovement()
        {
            storyboard.Begin(this, true);
        }

        public void StopAvatarMovement()
        {
            storyboard.Stop(this);
        }

        public void SetUpSong(Song song)
        {
            TagLib.File file = TagLib.File.Create(song.FilePath);

            Tag tag = file.GetTag(TagTypes.Id3v2);

            var pictures = tag.Pictures;

            if (pictures.Length > 0)
            {
                var picture = pictures[0];
                var imageData = picture.Data.Data;

                var memoryStream = new MemoryStream(imageData);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = memoryStream;
                image.EndInit();

                ImageBrush brush = new ImageBrush();
                brush.ImageSource = image;

                ellipseAvatar.Fill = brush;
            }

            StartAvatarMovement();
        }

        public void SetUpDefaultAvatar()
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/defaultAvatar.jpg"));

            ellipseAvatar.Fill = brush;
        }

        public void ShowDetail(Playlist playlist)
        {
            // Set Playlist Name
            tbPlName.Text = playlist.Name;

            // Set User Name
            tbUserName.Text = "Created by: " + Environment.UserName;

            // Set Last Modified
            DateTime updateTime = Directory.GetLastWriteTime(playlist.FilePath);
            tbUpdate.Text = "Update: " + updateTime.Date.ToString("d");

            // Set Listview source
            lvPlayListSongs.ItemsSource = playlist.Songs;

            //
            currentPlayList = playlist;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Open dialog
            var FileDialog = new CommonOpenFileDialog();
            FileDialog.IsFolderPicker = false;
            FileDialog.Multiselect = true;

            FileDialog.Filters.Add(new CommonFileDialogFilter("All Files", "*.*"));
            FileDialog.Filters.Add(new CommonFileDialogFilter("Media Files", "*.mp3 , *.mpg, *.mpeg, *.mkv"));

            if (FileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // Create a separate variable to store the playlist to which new songs are being added
                Playlist playlistToAddSongs = currentPlayList;

                string[] filepaths = FileDialog.FileNames.ToArray();
                for (int f = 0; f < filepaths.Length; f++)
                {
                    Song buffer = new Song(filepaths[f]);

                    bool isExisted = false;

                    for (int i = 0; i < playlistToAddSongs.Songs.Count; i++)
                    {
                        if (playlistToAddSongs.Songs[i].Name.Equals(buffer.Name))
                        {
                            isExisted = true;
                        }
                    }

                    if (!isExisted)
                    {
                        // Save to UI List
                        playlistToAddSongs.Songs.Add(buffer);

                        // Save to Folder on a separate thread
                        string oldPath = buffer.FilePath;
                        string newPath = System.IO.Path.Combine(Ultils.playListsFolderDomain, playlistToAddSongs.Name, buffer.Name);

                        Task.Run(() =>
                        {
                            System.IO.File.Copy(oldPath, newPath);
                        });
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Song[] songs = lvPlayListSongs.SelectedItems.Cast<Song>().ToArray();

            for (int i = 0; i < songs.Length; i++)
            {
                currentPlayList.Songs.Remove(songs[i]);
                System.IO.File.Delete(songs[i].FilePath);
            }
        }

        public delegate void AddSongToPlaayListHandler(String filePath);
        public event AddSongToPlaayListHandler AddSongToPlayList;

        public delegate void ShowDiskHandler();
        public event ShowDiskHandler ShowDisk;

        public delegate void ChangeToNowPlayingHandler();
        public event ChangeToNowPlayingHandler ChangeMenuToNowPlaying;
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Song[] songs = lvPlayListSongs.SelectedItems.Cast<Song>().ToArray();

            for (int i = 0; i < songs.Length; i++)
            {
                AddSongToPlayList?.Invoke(songs[i].FilePath);
            }

            lvPlayListSongs.SelectedItem = null;

            ShowDisk?.Invoke();
            ChangeMenuToNowPlaying?.Invoke();
        }
    }
}
