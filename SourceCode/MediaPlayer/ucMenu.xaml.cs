using Microsoft.WindowsAPICodePack.Dialogs;
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
    /// Interaction logic for ucMenu.xaml
    /// </summary>
    public partial class ucMenu : UserControl
    {
        ObservableCollection<Playlist> ListOfPlaylist = new ObservableCollection<Playlist>();
        public ucMenu()
        {
            InitializeComponent();

            // Create Folder to save Playlists
            if (!Directory.Exists(Ultils.playListsFolderDomain))
            {
                Directory.CreateDirectory(Ultils.playListsFolderDomain);
            }

            // Load Playlists
            string[] playLists = Directory.GetDirectories(Ultils.playListsFolderDomain);
            for (int i = 0; i < playLists.Count(); i++)
            {
                Playlist buffer = new Playlist(System.IO.Path.GetFileName(playLists[i]));
                ListOfPlaylist.Add(buffer);
            }

            // Load Songs into each Playlist
            for (int i = 0; i < ListOfPlaylist.Count; i++)
            {
                string[] SongsPath = Directory.GetFiles(ListOfPlaylist[i].FilePath);
                
                for (int j = 0; j < SongsPath.Count(); j++)
                {
                    Song buffer = new Song(SongsPath[j]);
                    ListOfPlaylist[i].Songs.Add(buffer);
                }
            }

            lvPlaylist.ItemsSource = ListOfPlaylist;
        }

        public event EventHandler ChangeUserControlEvent;

        // Now Playing click event
        public delegate void ShowDiskHandler();
        public event ShowDiskHandler ShowDiskRequire;
        private void radbtnNowPlaying_Checked(object sender, RoutedEventArgs e)
        {
            ChangeUserControlEvent?.Invoke(this, EventArgs.Empty);

            radbtnNowPlaying.IsChecked = false;
            lvPlaylist.SelectedItem = null;

            ShowDiskRequire?.Invoke();
        }

        public delegate void LoadFileToPlayerHandler(String FileName);
        public event LoadFileToPlayerHandler LoadFileToPlayer;

        // OpenFiles click
        private void radbtnOpenFiles_Checked(object sender, RoutedEventArgs e)
        {
            // OpenFile Dialog
            var chooseFilesDialog = new CommonOpenFileDialog();
            chooseFilesDialog.IsFolderPicker = false;
            chooseFilesDialog.Multiselect = true;

            chooseFilesDialog.Filters.Add(new CommonFileDialogFilter("Media Files", "*.mp3 , *.mpg, *.mpeg"));


            if (chooseFilesDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ChangeUserControlEvent?.Invoke(this, EventArgs.Empty);

                foreach (String fileName in chooseFilesDialog.FileNames)
                {
                    LoadFileToPlayer?.Invoke(fileName);
                }
            }

            // Unchecked
            radbtnOpenFiles.IsChecked = false;
            lvPlaylist.SelectedItem = null;

            ShowDiskRequire?.Invoke();
        }

        // Add playlist click event
        private void IconImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Open dialog
            var inputNameScreen = new AddPlaylist();
            
            // Create var to get the input
            String getNewPlaylistName = "";

            if (inputNameScreen.ShowDialog() == true)
            {
                //take the input from dialog
                getNewPlaylistName = inputNameScreen.NewPlaylistName;
            }
            else{
                //Nothing
            }

            if (!String.IsNullOrEmpty(getNewPlaylistName))
            {
                bool isPlayListExisted = false;

                for (int i = 0; i < ListOfPlaylist.Count; i++)
                {
                    if (ListOfPlaylist[i].Name.Equals(getNewPlaylistName))
                    {
                        isPlayListExisted = true;
                    }
                }

                if (!isPlayListExisted)
                {
                    // Add newplaylist to Observablecollection
                    var newPlaylist = new Playlist(getNewPlaylistName);

                    ListOfPlaylist.Add(newPlaylist);

                    // Save ListOfPlayList to Database
                    Directory.CreateDirectory(newPlaylist.FilePath);

                    // Adjust some UI, playlist that is created will be choose automatically
                    ScrollToBottom();
                } else
                {
                    var messageBox = new CustomMessageBox("Name has existed. Please choose another name.");
                    messageBox.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Use This function to select the lastes items in listview and make it scroll down
        /// </summary>
        private void ScrollToBottom()
        {
            // Tricks: set the selected item to the last one
            var selectedIndex = lvPlaylist.Items.Count - 1;

            lvPlaylist.SelectedIndex = selectedIndex;

            // Notify to UI that something has change
            lvPlaylist.UpdateLayout();

            // Scroll to that selected
            lvPlaylist.ScrollIntoView(lvPlaylist.SelectedItem);
        }

        // Recent Play Click
        public event EventHandler ChangeUserControlRecentPlayEvent;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ChangeUserControlRecentPlayEvent?.Invoke(this, EventArgs.Empty);

            radbtnRecentPlay.IsChecked = false;
            lvPlaylist.SelectedItem = null;

            ShowDiskRequire?.Invoke();
        }

        // Show contextMenu without selection

        private void mnRename_Click(object sender, RoutedEventArgs e)
        {
            // Open dialog
            var inputNameScreen = new AddPlaylist();

            // Create var to get the input
            String getNewPlaylistName = "";

            if (inputNameScreen.ShowDialog() == true)
            {
                //take the input from dialog
                getNewPlaylistName = inputNameScreen.NewPlaylistName;
            }
            else
            {
                //Nothing
            }

            if (!String.IsNullOrEmpty(getNewPlaylistName))
            {
                bool isPlayListExisted = false;

                for (int i = 0; i < ListOfPlaylist.Count; i++)
                {
                    if (ListOfPlaylist[i].Name.Equals(getNewPlaylistName))
                    {
                        isPlayListExisted = true;
                    }
                }

                if (!isPlayListExisted)
                {
                    MenuItem menuItem = (MenuItem)sender;
                    ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
                    ListViewItem selectedItem = (ListViewItem)contextMenu.PlacementTarget;
                    Playlist selectedPlayList = (Playlist)lvPlaylist.ItemContainerGenerator.ItemFromContainer(selectedItem);
                    string currentDirectoryPath = selectedPlayList.FilePath;
                    
                    // rename in list
                    int i = ListOfPlaylist.IndexOf(selectedPlayList);
                    ListOfPlaylist[i].Name = getNewPlaylistName;

                    // update fodler path
                    string newDirectoryName = getNewPlaylistName;
                    string newDirectoryPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(currentDirectoryPath), newDirectoryName);

                    Directory.Move(currentDirectoryPath, newDirectoryPath);

                    // update UI of showing detail playlist
                    if ((Playlist)lvPlaylist.SelectedItem == selectedPlayList)
                    {
                        lvPlaylist.SelectedItem = null;
                        lvPlaylist.SelectedItem = selectedPlayList;
                    }
                }
                else
                {
                    var messageBox = new CustomMessageBox("Name has existed. Please choose another name.");
                    messageBox.ShowDialog();
                }
            }
        }

        private void mnDelete_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            ListViewItem selectedItem = (ListViewItem)contextMenu.PlacementTarget;
            Playlist selectedPlayList = (Playlist)lvPlaylist.ItemContainerGenerator.ItemFromContainer(selectedItem);

            // Pre-check user's decision
            string msg = $"Your action will delete PlayList '{selectedPlayList.Name}' permanently.";
            var msgBox = new CustomMessageBox(msg);
            if (msgBox.ShowDialog() == true)
            {
                Directory.Delete(selectedPlayList.FilePath, true);
                ListOfPlaylist.Remove(selectedPlayList);
            }
        }

        private void lvPlaylist_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
                    ctmnPlayLists.PlacementTarget = listViewItem;
                    ctmnPlayLists.IsOpen = true;
                    e.Handled = true;
                }
            }
        }

        public delegate void ShowDetailPlayListHandler (Playlist playlist);
        public event ShowDetailPlayListHandler ShowDetailPlayList;

        
        // Inform MainWindow to set visible for detail Playlisst and hidden for disk
        private void lvPlaylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvPlaylist.SelectedIndex != -1)
            {
                Playlist playlist = (Playlist)lvPlaylist.SelectedItem;
                ShowDetailPlayList?.Invoke(playlist);
            }
            else // If the last playlist that is selected and deleted as same time give "" msg
            {
                Playlist playlist = new Playlist("");
                ShowDetailPlayList?.Invoke(playlist);
            }
        }
    }
}
