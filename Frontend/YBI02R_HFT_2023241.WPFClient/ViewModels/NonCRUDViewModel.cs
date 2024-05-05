using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.WPFClient.ViewModels
{
    partial class NonCRUDViewModel : ObservableRecipient
    {
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

        private int? inputX;

        public int? InputX
        {
            get { return inputX; }
            set { inputX = value; }
        }

        private int? inputY;

        public int? InputY
        {
            get { return inputY; }
            set { inputY = value; }
        }

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

        private bool CheckInputX()
        {
            return InputX != null;
        }

        private bool CheckInputY()
        {
            return InputY != null;
        }

        [RelayCommand]
        public void SongsBornBeforeIsArtist()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Song> Out = restService.Get<Song>("ExtendMethodLogic/SongsBornBeforeIsArtist/" + InputX + "/" + InputY);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        [RelayCommand]
        public void SongsBornAfterIsArtist()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Song> Out = restService.Get<Song>("ExtendMethodLogic/SongsBornAfterIsArtist/" + InputX + "/" + InputY);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        [RelayCommand]
        public void ArtistWithSongsMoreThan()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Artist> Out = restService.Get<Artist>("ExtendMethodLogic/ArtistWithSongsMoreThan/" + InputX);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        [RelayCommand]
        public void PublisherWithMoreSongsThan()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Publisher> Out = restService.Get<Publisher>("ExtendMethodLogic/PublisherWithMoreSongsThan/" + InputX);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        [RelayCommand]
        public void PublisherWithMoreSongsThanAndOlderThan()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Publisher> Out = restService.Get<Publisher>("ExtendMethodLogic/PublisherWithMoreSongsThanAndOlderThan/" + InputX + "/" + InputY);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        //[RelayCommand]
        //public void SongStats()
        //{
        //    Display = new();
        //    List<SongStats> Songstats = restService.Get<SongStats>("ExtendMethodLogic/SongStats");

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
