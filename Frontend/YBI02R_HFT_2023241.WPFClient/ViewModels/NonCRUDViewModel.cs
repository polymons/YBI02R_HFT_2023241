using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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
            //check if inputName is in Artist collection
            get { return inputName; }
            set
            {
                if (value != null)
                {
                    var artists = restService.Get<Artist>("Artist").Select(a => a.Name).ToArray();
                    if (artists.Contains(value))
                    {
                        SetProperty(ref inputName, value);
                    }
                    else
                    {
                        ResponseMessage = "Artist not found";
                    }
                }
            }
        }

        #region Artist stats

        [RelayCommand]
        public void MostPopularSongOfArtist()
        {
            if (InputName == null) return;
            try
            {
                Display = new();
                var no1Song = restService.GetString<Song>(InputName, "/Stat/MostPopularSongOfArtist?artistName=");
                Display.Add(new ShowItem($"The most popular song of {InputName} is: {no1Song.Title} by {no1Song.Artist.Name}"));
                ResponseMessage = "";
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message;
            }
        }
        [RelayCommand]
        public void ArtistHomeCity()
        {
            if (InputName == null) return;
            try
            {
                Display = new();
                var homeCity = restService.GetString<string>(InputName, "/Stat/ArtistHomeCity?artistName=");
                Display.Add(new ShowItem($"The home town of {InputName} is: {homeCity}"));
                ResponseMessage = "";
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message;
            }
        }
        [RelayCommand]
        public void AvgSongLengthForArtist()
        {
            if (InputName == null) return;
            try
            {
                Display = new();
                var avgLength = restService.GetString<double?>(InputName, "/Stat/AvgSongLengthForArtist?artistName=");
                Display.Add(new ShowItem($"The average song length for {InputName} is: {avgLength}"));
                ResponseMessage = "";
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message;
            }
        }
        #endregion

        #region General Stats
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
        //public void SongStats()
        //{
        //    Display = new();
        //    int Songstats = restService.Get<SongStats>("Stat/OldestArtistAge");

        //    foreach (var item in Songstats)
        //    {
        //        Display.Add(new ShowItem(item.ToString()));
        //    }
        //}
        #endregion

        [RelayCommand]
        public void Return(Window thisWindow)
        {
            var window = new MainWindow();
            window.Show();
            thisWindow.Close();
        }

    }
}
