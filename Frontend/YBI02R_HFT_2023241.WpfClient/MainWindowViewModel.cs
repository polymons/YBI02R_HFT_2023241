using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using YBI02R_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace YBI02R_HFT_2023241.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Song> Songs { get; set; }

        private Song selectedSong;

        public Song SelectedSong
        {
            get { return selectedSong; }
            set
            {
                if (value != null)
                {
                    selectedSong = new Song()
                    {
                        Title = value.Title,
                        SongID = value.SongID
                    };
                    OnPropertyChanged();
                    (DeleteSongCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateSongCommand { get; set; }

        public ICommand DeleteSongCommand { get; set; }

        public ICommand UpdateSongCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Songs = new RestCollection<Song>("http://localhost:53910/", "Song", "hub");
                CreateSongCommand = new RelayCommand(() =>
                {
                    Songs.Add(new Song()
                    {
                        Title = SelectedSong.Title
                    });
                });

                UpdateSongCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Songs.Update(SelectedSong);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                    
                });

                DeleteSongCommand = new RelayCommand(() =>
                {
                    Songs.Delete(SelectedSong.SongID);
                },
                () =>
                {
                    return SelectedSong != null;
                });
                SelectedSong = new Song();
            }
            
        }
    }
}
