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
using YBI02R_HFT_2023241.WPFClient.Services;

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
