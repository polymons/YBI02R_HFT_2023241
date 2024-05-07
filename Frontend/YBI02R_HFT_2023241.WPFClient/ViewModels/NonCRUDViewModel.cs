using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.WPFClient.Services;

namespace YBI02R_HFT_2023241.WPFClient.ViewModels
{
    partial class NonCRUDViewModel : ObservableRecipient
    {
        private string responseMessage;
        public string ResponseMessage
        {
            get { return responseMessage; }
            set { SetProperty(ref responseMessage, value); }
        }

        public class ShowItem
        {
            public ShowItem(string display)
            {
                Item = display;
            }
            public string Item { get; set; }
        }

        private ObservableCollection<ShowItem> display;
        public ObservableCollection<ShowItem> Display
        {
            get { return display; }
            set => SetProperty(ref display, value);
        }


        private RestService restService;

        private static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public NonCRUDViewModel()
        {
            if (!IsInDesignMode)
            {
                restService = new("http://localhost:53910/");
            }
        }

        string? inputName;
        public string? InputName
        {
            get {return inputName;}
            set {SetProperty(ref inputName, value); }
        }

        [RelayCommand]
        public void MostPopularSongOfArtist()
        {
            try
            {
                Display = new();
                var no1Song = restService.GetString<Song>(InputName, "/Stat/MostPopularSongOfArtist?artistName=");
                Display.Add(new ShowItem($"The most popular song of {InputName} is: {no1Song.Title} by {no1Song.Artist.Name}"));
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message;
            }
        }
        [RelayCommand]
        public void ArtistHomeCity()
        {
            try
            {
                Display = new();
                Artist artist = restService.GetSingle<Artist>($"Stat/ArtistHomeCity/{InputName}");
                Display.Add(new ShowItem($"{InputName} is from {artist.Studio.City}"));
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message;
            }
        }
        [RelayCommand]
        public void AvgSongLengthForArtist()
        {
            try 
            { 
                Display = new();
                double? avgSongLength = restService.GetSingle<double>($"Stat/AvgSongLengthForArtist/{InputName}");
                Display.Add(new ShowItem($"The average song length for {InputName} is: {avgSongLength}"));
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message;
            }
        }

        [RelayCommand]
        public void ArtistWithMostSongs()
        {
            Display = new();
            Artist artistWithMostSongs = restService.GetSingle<Artist>("Stat/ArtistWithMostSongs/");
            Display.Add(new ShowItem($"The artist with the most songs is: {artistWithMostSongs.Name}"));
        }

        [RelayCommand]
        public void MostPopularArtist()
        {
            Display = new();
            Artist mostPopularArtist = restService.GetSingle<Artist>("Stat/MostPopularArtist/");
            Display.Add(new ShowItem($"The most popular artist is: {mostPopularArtist.Name} at {mostPopularArtist.Age} years"));
        }
        [RelayCommand]
        public void LongestSong()
        {
            Display = new();
            Song longestSong = restService.GetSingle<Song>("Stat/LongestSong/");
            Display.Add(new ShowItem($"The longest song is: {longestSong.Title} by {longestSong.Artist.Name}"));
        }
        //[RelayCommand]
        //public void MinutesListenedToPublisher()
        //{
        //    Display = new();
        //    double? minutesListened = restService.GetSingle<double>($"Stat/MinutesListenedToPublisher/{InputName}");
        //    Display.Add(new ShowItem($"Minutes listened to {InputName} is: {minutesListened}"));
        //}



        //[RelayCommand]
        //public void SongStats()
        //{
        //    Display = new();
        //    int Songstats = restService.Get<SongStats>("Stat/OldestArtistAge");

        //    foreach (var item in Songstats)
        //    {
        //        Display.Add(new ShowItem(item.ToString()));
        //    }
        //}



        [RelayCommand]
        public void Return(Window thisWindow)
        {
            var window = new MainWindow();
            window.Show();
            thisWindow.Close();
        }

    }
}
