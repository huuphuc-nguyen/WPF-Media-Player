using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaPlayer
{
    /// <summary>
    /// When the selection change -> invoke mediaplayer in ucNowPlaying
    /// </summary>
    public partial class ucNowPLayingSideBar : UserControl
    {

        public ObservableCollection<Song> NowPlayingSongs = new ObservableCollection<Song>();

        public ObservableCollection<Song> SearchSongs = new ObservableCollection<Song>();

        public ucNowPLayingSideBar()
        {
            InitializeComponent();

            lvNowPlaying.ItemsSource = NowPlayingSongs;
        }

        public EventHandler ChangeUserControlEvent;
        private void btnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeUserControlEvent?.Invoke(this, EventArgs.Empty);
        }

        // When File is open from browse
       

        public delegate void ChangePlayingSongHandler(Song newSong);
        public event ChangePlayingSongHandler SideBarChangeSong;

        // Important, this func will send the song need to play to Main, Main will use mediaplayer of ucNowPlaying to play song
        private void lvNowPlaying_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvNowPlaying.SelectedIndex;
            if (index != -1)
                //SideBarChangeSong?.Invoke(NowPlayingSongs[index]);
                SideBarChangeSong?.Invoke((Song)lvNowPlaying.SelectedItem);
        }

        public void selectSongAt(int index) // select change -> mediaplayerchange
        {
            lvNowPlaying.SelectedIndex = index;
        }

        private void tbSearchNowPlaying_TextChanged(object sender, TextChangedEventArgs e)
        {
            String keyWord = tbSearchNowPlaying.Text.ToLower();
            ObservableCollection<Song> SearchResult = new ObservableCollection<Song>();

            if (!String.IsNullOrEmpty(keyWord))
            {
                PlayingLayout.Visibility = Visibility.Hidden;
                SearchLayout.Visibility = Visibility.Visible;

                Song selectedSong = (Song)lvNowPlaying.SelectedItem;
                int selectedIndex = -1;

                foreach (Song song in NowPlayingSongs)
                {
                    if (song.Name.ToLower().Contains(keyWord))
                    {
                        SearchResult.Add(song);

                        // check if the currently selected song is in the search result
                        if (selectedSong != null && selectedSong == song)
                        {
                            selectedIndex = SearchResult.Count - 1;
                        }
                    }
                }

                SearchSongs = SearchResult;
                lvSearch.ItemsSource = SearchSongs;

                if (selectedIndex != -1)
                {
                    lvSearch.SelectedIndex = selectedIndex;
                }
            }
            else
            {
                PlayingLayout.Visibility = Visibility.Visible;
                SearchLayout.Visibility = Visibility.Hidden;
            }
        }

        private void lvSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Song selectedSong = (Song)lvSearch.SelectedItem;

            if (lvSearch.SelectedIndex != -1) 
            // without this condition, the NowPlayingList will lost current selection when the searchlistview change to a list without selected song
            {
                // Change in listViewNowPlaying
                lvNowPlaying.SelectedItem = selectedSong;
                lvNowPlaying.ScrollIntoView(selectedSong);
                // Scroll to the selected song
                lvNowPlaying.UpdateLayout();
            }
        }

        private void lvNowPlaying_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView listView = (ListView)sender;
            Point position = e.GetPosition(listView);
            HitTestResult result = VisualTreeHelper.HitTest(listView, position);
            if (result != null)
            {
                DependencyObject target = result.VisualHit;
                while (target != null && !(target is ListViewItem))
                {
                    target = VisualTreeHelper.GetParent(target);
                }
                if (target != null)
                {
                    ListViewItem listViewItem = (ListViewItem)target;
                    Song choosenSong = (Song)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    Song currentSelectedSong = (Song)lvNowPlaying.SelectedItem;
                    
                    if (choosenSong != currentSelectedSong && !File.Exists(choosenSong.FilePath))
                    {
                        string msg = "Couldn't find FilePath: \n" + choosenSong.FilePath;
                        var msgBox = new CustomMessageBox(msg);

                        msgBox.ShowDialog();
                        NowPlayingSongs.Remove(choosenSong);
                    }
                }
            }
        }

        // // Song List Context Menu
        private void lvNowPlaying_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView listView = (ListView)sender;
            Point position = e.GetPosition(listView);
            HitTestResult result = VisualTreeHelper.HitTest(listView, position);

            if (result != null)
            {
                DependencyObject target = result.VisualHit;
                while (target != null && !(target is ListViewItem))
                {
                    target = VisualTreeHelper.GetParent(target);
                }
                if (target != null)
                {
                    ListViewItem listViewItem = (ListViewItem)target;
                    ctmnPlayingSong.PlacementTarget = listViewItem;
                    ctmnPlayingSong.IsOpen = true;
                    e.Handled = true;
                }
            }
        }

        private void mnDelete_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuitem.Parent;
            ListViewItem listViewItem = (ListViewItem)contextMenu.PlacementTarget;
            Song deleteSong = (Song)lvNowPlaying.ItemContainerGenerator.ItemFromContainer(listViewItem);

            if (deleteSong != (Song)lvNowPlaying.SelectedItem)
            {
                NowPlayingSongs.Remove(deleteSong);
            }
            else
            {
                string msg = "Can't delete this file. \n" + deleteSong.Name + " is playing";
                var msgBox = new CustomMessageBox(msg);
                msgBox.ShowDialog();
            }
        }

        // Search List Context Menu
        private void lvSearch_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView listView = (ListView)sender;
            Point position = e.GetPosition(listView);
            HitTestResult result = VisualTreeHelper.HitTest(listView, position);

            if (result != null)
            {
                DependencyObject target = result.VisualHit;
                while (target != null && !(target is ListViewItem))
                {
                    target = VisualTreeHelper.GetParent(target);
                }
                if (target != null)
                {
                    ListViewItem listViewItem = (ListViewItem)target;
                    ctmnPlayingSong2.PlacementTarget = listViewItem;
                    ctmnPlayingSong2.IsOpen = true;
                    e.Handled = true;
                }
            }
        }

        private void mnDelete2_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuitem.Parent;
            ListViewItem listViewItem = (ListViewItem)contextMenu.PlacementTarget;
            Song deleteSong = (Song)lvSearch.ItemContainerGenerator.ItemFromContainer(listViewItem);

            if (deleteSong != (Song)lvNowPlaying.SelectedItem)
            {
                SearchSongs.Remove(deleteSong);
                NowPlayingSongs.Remove(deleteSong);
            }
            else
            {
                string msg = "Can't delete this file. \n" + deleteSong.Name + " is playing";
                var msgBox = new CustomMessageBox(msg);
                msgBox.ShowDialog();
            }
        }

        private void lvSearch_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView listView = (ListView)sender;
            Point position = e.GetPosition(listView);
            HitTestResult result = VisualTreeHelper.HitTest(listView, position);
            if (result != null)
            {
                DependencyObject target = result.VisualHit;
                while (target != null && !(target is ListViewItem))
                {
                    target = VisualTreeHelper.GetParent(target);
                }
                if (target != null)
                {
                    ListViewItem listViewItem = (ListViewItem)target;
                    Song choosenSong = (Song)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    Song currentSelectedSong = (Song)lvNowPlaying.SelectedItem;

                    if (choosenSong != currentSelectedSong && !File.Exists(choosenSong.FilePath))
                    {
                        string msg = "Couldn't find FilePath: \n" + choosenSong.FilePath;
                        var msgBox = new CustomMessageBox(msg);

                        if (msgBox.ShowDialog() == true)
                        {
                            SearchSongs.Remove(choosenSong);
                            NowPlayingSongs.Remove(choosenSong);
                        }    
                            
                    }
                }
            }
        }
    }
}
