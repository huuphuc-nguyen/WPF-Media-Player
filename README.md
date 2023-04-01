# :musical_score: WPF-Media-Player :musical_score:
## I. Project description:
This app can do:
- Add all media files you want to play into a playlist (currently just support for audio .mp3, ... files).
- Create playlist to store your favorite songs.
- Add and remove files from the playlist.
- Save and load a playlist. Your playlist will save as a folder (include songs inside), you can copy or move it anywhere.
- Show the current progress of the playing file, allow seeking.
- Play the next file in playlist, play the previous file in the playlist.
- Store recently played files.
## II.How to run the program
|To execute the program|To debug and view code|
|------------|:---------------|
|1. Open *Release/AppRelease/MediaPlayer.exe*|1. Open Visual Studio 2019|
|2. You can manage playlists in folder *Release/AppRelease/PlayLists*|2. Open *SourceCode/MediaPlayer.sln*|
## III. Some knowledge 
- Use Style and Template of Controls to design modern UI.
- Use StoryBoard to start animation.
- Use UserControl to create multi screens in the app.
- Use delegate and event to communicate between UserControl (update version will use MMVVM).
- Use TagLib to read ID3 tag of files.
- Use MediaElement to play the file.
- With help of ChatGPT
## IV. Demo link
