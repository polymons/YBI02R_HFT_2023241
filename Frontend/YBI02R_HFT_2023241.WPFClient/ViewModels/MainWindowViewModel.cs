using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YBI02R_HFT_2023241.WPFClient.ViewModels
{
    partial class MainWindowViewModel
    {
        [RelayCommand]
        public void EditPublishers(Window window)
        {
            var PublisherWindow = new PublisherEditor();
            PublisherWindow.Show();
            window.Close();
        }

        [RelayCommand]
        public void EditSongs(Window window)
        {
            var SongWindow = new SongEditor();
            SongWindow.Show();
            window.Close();
        }

        [RelayCommand]
        public void EditArtists(Window window)
        {
            var ArtistWindow = new ArtistEditor();
            ArtistWindow.Show();
            window.Close();
        }

        [RelayCommand]
        public void NonCRUD(Window window)
        {
            var Window = new NonCRUD();
            Window.Show();
            window.Close();
        }
        public MainWindowViewModel() { }
    }
}
