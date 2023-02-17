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
using System.Windows.Threading;
using System.IO;

namespace MediaPlayer
{
    /// <summary>
    /// Main will manage NowPlayList of ucNowPlayingSideBar
    /// </summary>
    public partial class MainWindow : Window
    {
        ucMenu _ucMenu = new ucMenu();
        ucNowPLayingSideBar _ucNowPlayingSidebar = new ucNowPLayingSideBar();
        ucNowPlaying _ucNowPlaying = new ucNowPlaying();
        ucRecentPlay _ucRecentPlay = new ucRecentPlay();

        public bool IsPlaying = false;
        public bool IsHavingFilePlaying = false; // Use for btnPLay/Pause event, if don't have file -> don't catch event
        public bool IsRepeat = false;
        public bool IsMute = false;
        
        DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            // Set change User Control (child view) events
            _ucMenu.ChangeUserControlEvent += Change_ucMenu_to_ucNowPlayingSideBar;
            _ucNowPlayingSidebar.ChangeUserControlEvent += Change_ucNowPlayingSideBar_to_ucMenu;
            _ucMenu.ChangeUserControlRecentPlayEvent += Change_ucMenu_to_ucRecentPlay;
            _ucRecentPlay.ChangeUserControlEvent += Change_ucRecentPlay_to_ucMenu;
            _ucRecentPlay.ChangeUserControlEvent2 += Change_ucRecentPlay_to_ucNowPlayingSideBar;

           // Set other event 
           _ucMenu.LoadFileToPlayer += ReceiveFilePath;

            _ucNowPlayingSidebar.SideBarChangeSong += MediaChangeSong;

            _ucNowPlaying.MediaEnded += FinishPlaySong;

            _ucRecentPlay.RecentPlayAddSong += ReceiveFilePath;

            _ucMenu.ShowDetailPlayList += SetUpDetailPlayList;

            _ucMenu.ShowDiskRequire += ShowDisk;

            _ucNowPlaying.AddSongToPlayList += ReceiveFilePath;

            _ucNowPlaying.ShowDisk += ShowDisk;

            _ucNowPlaying.ChangeMenuToNowPlaying += ChangeMenuToNowPlaying;
        }

        private void Change_ucMenu_to_ucNowPlayingSideBar(object sender, EventArgs e)
        {
            ccSideView.Content = null;
            ccSideView.Content = _ucNowPlayingSidebar;
        }

        private void Change_ucNowPlayingSideBar_to_ucMenu(object sender, EventArgs e)
        {
            ccSideView.Content = null;
            ccSideView.Content = _ucMenu;
        }

        private void Change_ucMenu_to_ucRecentPlay(object sender, EventArgs e)
        {
            ccSideView.Content = null;
            ccSideView.Content = _ucRecentPlay;
        }

        private void Change_ucRecentPlay_to_ucNowPlayingSideBar(object sender, EventArgs e)
        {
            ccSideView.Content = null;
            ccSideView.Content = _ucNowPlayingSidebar;
        }

        private void Change_ucRecentPlay_to_ucMenu(object sender, EventArgs e)
        {
            ccSideView.Content = null;
            ccSideView.Content = _ucMenu;
        }

        private void ChangeMenuToNowPlaying()
        {
            _ucMenu.lvPlaylist.SelectedItem = null;
            ccSideView.Content = null;
            ccSideView.Content = _ucNowPlayingSidebar;
        }

        private void SetUpDetailPlayList (Playlist playlist)
        {
            if (playlist.Name.Equals("")) // the last item is deleted -> set view to disk
            {
                _ucNowPlaying.Disk.Visibility = Visibility.Visible;
                _ucNowPlaying.Detail.Visibility = Visibility.Hidden;
            }
            else 
            {
                _ucNowPlaying.Disk.Visibility = Visibility.Hidden;
                _ucNowPlaying.Detail.Visibility = Visibility.Visible;
                _ucNowPlaying.ShowDetail(playlist);
            }
        }

        private void ShowDisk()
        {
            _ucNowPlaying.Disk.Visibility = Visibility.Visible;
            _ucNowPlaying.Detail.Visibility = Visibility.Hidden;
        }

        // After user choose a file from ucMenu
        private void ReceiveFilePath(String FilePath)
        { 
            if(IsHavingFilePlaying == false) // This is firt song
            {
                // Set flag
                IsHavingFilePlaying = true;
                IsPlaying = true;

                // Change icon button play/pause 
                icPlayPause.Icon = FontAwesome.Sharp.IconChar.Pause;

                // Create Song with FilePath
                Song newSong = new Song(FilePath);

                // Add file to nowplaying list
                _ucNowPlayingSidebar.NowPlayingSongs.Add(newSong);
                _ucNowPlayingSidebar.selectSongAt(0);

            }
            else // Just add to nowplaying list and wait for its turn
            {
                if (!_ucNowPlayingSidebar.NowPlayingSongs.Any(p => p.Name == System.IO.Path.GetFileName(FilePath))) // Check if file has added before
                {
                    // Create Song with FilePath
                    Song newSong = new Song(FilePath);

                    // Add file to nowplaying list
                    _ucNowPlayingSidebar.NowPlayingSongs.Add(newSong);
                }
                else
                {
                    for (int i = 0; i < _ucNowPlayingSidebar.NowPlayingSongs.Count; i++)
                    {
                        if (_ucNowPlayingSidebar.NowPlayingSongs[i].Name == System.IO.Path.GetFileName(FilePath))
                        {
                            _ucNowPlayingSidebar.NowPlayingSongs[i].FilePath = FilePath;
                            return;
                        }
                    }
                }
            }          
        }

        // Change song when now playing list is change
        public void MediaChangeSong(Song newSong)
        {
            // Check validate file path
            if (!File.Exists(newSong.FilePath))
            {
                string msg = "Couldn't find FilePath: \n" + newSong.FilePath;
                var msgBox = new CustomMessageBox(msg);

                if (msgBox.ShowDialog() == true)
                {
                    // Which case
                    if (_ucNowPlaying.mediaPlayer.Source == null)
                    {
                        // 1. The curent file is ended
                        // Delete Unvalid File From Playlist
                        _ucNowPlayingSidebar.NowPlayingSongs.Remove(newSong);
                        _ucNowPlayingSidebar.selectSongAt(0);
                    }
                    else
                    {
                        // 2. The current file is playing
                        // Denfine in ucNowPlayingSideBar.xaml.cs _ previewmouseleftdown
                    }
                }    
            }
            else
            {
                // Set UI stop the previous
                IsPlaying = false;
                icPlayPause.Icon = FontAwesome.Sharp.IconChar.Play;
                sliderMySlider.Visibility = Visibility.Hidden;

                // Set slider total time
                _ucNowPlaying.LoadTotalTime(newSong); // Get source and play+stop 1 time here
                String mainTotalTime = _ucNowPlaying.totalTime;
                tbTotalTime.Text = Ultils.FormatMediaTime(mainTotalTime);
                tbCurrentTime.Text = "00:00";

                // Set slider
                sliderMySlider.Maximum = _ucNowPlaying.mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliderMySlider.Value = 0;

                // Set Volume
                _ucNowPlaying.mediaPlayer.Volume = sliderVolume.Value;

                // Set UI
                IsPlaying = true;
                icPlayPause.Icon = FontAwesome.Sharp.IconChar.Pause;
                sliderMySlider.Visibility = Visibility.Visible;

                _ucNowPlaying.SetUpSong(newSong);

                // Set MediaPlayer
                _ucNowPlaying.mediaPlayer.Source = new Uri(newSong.FilePath, UriKind.Absolute);
                _ucNowPlaying.mediaPlayer.Play();

                // Put this song into RecentPLayList
                _ucRecentPlay.AddSong(newSong);

                // Set position time
                _timer.Interval = new TimeSpan(0, 0, 1);
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            int hours = _ucNowPlaying.mediaPlayer.Position.Hours;
            int minutes = _ucNowPlaying.mediaPlayer.Position.Minutes;
            int seconds = _ucNowPlaying.mediaPlayer.Position.Seconds;

            // Update slider
            double newPosition = _ucNowPlaying.mediaPlayer.Position.TotalSeconds;
            sliderMySlider.Value = newPosition;

            // Update textbox
            String timeCurrent = $"{hours}:{minutes}:{seconds}";
            tbCurrentTime.Text = Ultils.FormatMediaTime(timeCurrent); // many ifs so slower
        }

        public void FinishPlaySong()
        {
            if (IsRepeat)
            {
                //TimeSpan startPos = TimeSpan.FromSeconds(0);
                //_ucNowPlaying.mediaPlayer.Position = startPos;
                //_ucNowPlaying.mediaPlayer.Play();
                Song song = (Song)_ucNowPlayingSidebar.lvNowPlaying.SelectedItem;
                _ucNowPlayingSidebar.lvNowPlaying.SelectedItem = null;
                _ucNowPlayingSidebar.lvNowPlaying.SelectedItem = song;
            }
            else
            {
                int currentSongIndex = _ucNowPlayingSidebar.lvNowPlaying.SelectedIndex;
                _ucNowPlaying.mediaPlayer.Pause();

                // Remove song
                _ucNowPlayingSidebar.lvNowPlaying.SelectedItem = -1;
                _ucNowPlayingSidebar.NowPlayingSongs.RemoveAt(currentSongIndex);
                // Remove from search list
                if (_ucNowPlayingSidebar.SearchSongs.Count() != 0)
                {
                    _ucNowPlayingSidebar.lvSearch.SelectedItem = -1;
                    _ucNowPlayingSidebar.SearchSongs.RemoveAt(_ucNowPlayingSidebar.lvSearch.SelectedIndex);
                }

                // Timer..//
                _timer.Stop();

                // Play next song

                if (_ucNowPlayingSidebar.SearchSongs.Count() != 0) // Still in Search mode, After remove, still have song
                {
                    _ucNowPlayingSidebar.lvSearch.SelectedIndex = 0; // Play next song in search list
                }
                else
                {
                    // No more song in search list -> out search mode by clear the keyword
                    _ucNowPlayingSidebar.tbSearchNowPlaying.Text = "";
                    _ucNowPlayingSidebar.lvNowPlaying.UpdateLayout();
                    _ucNowPlayingSidebar.lvNowPlaying.ScrollIntoView(Top);
                    
                    // Play next song in play list
                    if (_ucNowPlayingSidebar.NowPlayingSongs.Count() != 0)
                    {
                        _ucNowPlayingSidebar.selectSongAt(0);
                    }
                    else // == 0
                    {
                        IsHavingFilePlaying = false;

                        // Set UI + flag
                        IsPlaying = false;

                        icPlayPause.Icon = FontAwesome.Sharp.IconChar.Play;
                        tbTotalTime.Text = "";
                        tbCurrentTime.Text = "";
                        sliderMySlider.Value = 0;
                        sliderMySlider.Visibility = Visibility.Hidden;

                        _ucNowPlaying.StopAvatarMovement();
                        _ucNowPlaying.SetUpDefaultAvatar();
                    }
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ccSideView.Content = _ucMenu;
            ccMainView.Content = _ucNowPlaying;

            // TODO: Read saved playlist and load to ccMenu.ListOfPlayList
        }

        /////////////////////// Controls Tasks////////////////////

        // Drag window when hold the control bar
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {DragMove();}
        
        private void btnClose_Click(object sender, RoutedEventArgs e) {Application.Current.Shutdown();}

        private void btnMinimize_Click(object sender, RoutedEventArgs e) {this.WindowState = WindowState.Minimized;}

        private void IconImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsHavingFilePlaying)
            {
                switch (IsPlaying)
                {
                    case true:
                        _ucNowPlaying.mediaPlayer.Pause();
                        _timer.Stop();
                        icPlayPause.Icon = FontAwesome.Sharp.IconChar.Play;
                        IsPlaying = false;
                        break;
                    case false:
                        _ucNowPlaying.mediaPlayer.Play();
                        _timer.Start();
                        icPlayPause.Icon = FontAwesome.Sharp.IconChar.Pause;
                        IsPlaying = true;
                        break;
                }
            }
            else{
                // No have file playing ->  Don't change anything
            }
        }

        private void icPrevious_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsHavingFilePlaying)
            {
                int currentSongIndex = _ucNowPlayingSidebar.lvNowPlaying.SelectedIndex;
                int previousSongIndex = currentSongIndex - 1;

                if (previousSongIndex >= 0) _ucNowPlayingSidebar.selectSongAt(previousSongIndex);
            }
        }

        private void icNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsHavingFilePlaying)
            {
                int currentSongIndex = _ucNowPlayingSidebar.lvNowPlaying.SelectedIndex;
                int nextSongIndex = currentSongIndex + 1;

                if (nextSongIndex < _ucNowPlayingSidebar.NowPlayingSongs.Count)
                    _ucNowPlayingSidebar.selectSongAt(nextSongIndex);
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = sliderMySlider.Value;
            TimeSpan newPosition = TimeSpan.FromSeconds(value);

            _ucNowPlaying.mediaPlayer.Position = newPosition;
        }

        private void icRepeat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsRepeat = true;
            icRepeat.Visibility = Visibility.Hidden;
            icRepeat_1.Visibility = Visibility.Visible;
        }

        private void icRepeat_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsRepeat = false;
            icRepeat.Visibility = Visibility.Visible;
            icRepeat_1.Visibility = Visibility.Hidden;
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double volume = sliderVolume.Value;

            if (volume == 0)
            {
                IsMute = true;
                icOffVolume.Visibility = Visibility.Visible;
                icVolume.Visibility = Visibility.Hidden;
            }
            else
            {
                if (IsMute)
                {
                    IsMute = false;
                    icOffVolume.Visibility = Visibility.Hidden;
                    icVolume.Visibility = Visibility.Visible;
                }
            }

            _ucNowPlaying.mediaPlayer.Volume = volume;
        }

        private void icVolume_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMute == false)
            {
                sliderVolume.Value = 0;
                IsMute = true;
            }
        }

        private void icOffVolume_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMute == true)
            {
                sliderVolume.Value = 0.7;
                IsMute = false;
            }
        }
    }
}
