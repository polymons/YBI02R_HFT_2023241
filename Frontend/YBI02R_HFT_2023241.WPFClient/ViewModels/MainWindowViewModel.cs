using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace YBI02R_HFT_2023241.WPFClient.ViewModels
{
    partial class MainWindowViewModel
    {
        [RelayCommand]
        public static void EditPublishers(Window window)
        {
            var PublisherWindow = new PublisherEditor();
            PublisherWindow.Show();
            window.Close();
        }

        [RelayCommand]
        public static void EditSongs(Window window)
        {
            var SongWindow = new SongEditor();
            SongWindow.Show();
            window.Close();
        }

        [RelayCommand]
        public static void EditArtists(Window window)
        {
            var ArtistWindow = new ArtistEditor();
            ArtistWindow.Show();
            window.Close();
        }

        [RelayCommand]
        public static void NonCRUD(Window window)
        {
            var Window = new NonCRUD();
            Window.Show();
            window.Close();
        }
        public MainWindowViewModel() { }
    }
}
