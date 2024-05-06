using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.WPFClient.Services;

namespace YBI02R_HFT_2023241.WPFClient.ViewModels
{
    partial class SongEditorViewModel : ObservableRecipient
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private Song selectedItem;
        public Song SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value);
                InputItem = value;
                DeleteCommand.NotifyCanExecuteChanged();
                UpdateCommand.NotifyCanExecuteChanged();
            }
        }

        private Song inputItem;
        public Song InputItem
        {
            get => inputItem;
            set
            {
                SetProperty(ref inputItem, value);
                if (value != null)
                {
                    InputSongID = value.SongID;
                    InputTitle = value.Title;
                    InputGenre = value.Genre;
                    InputArtistID = value.ArtistID;
                    InputArtist = value.Artist;
                }
                else
                {
                    InputSongID = null;
                    InputTitle = null;
                    InputGenre = null;
                    InputArtistID = null;
                    InputArtist = null;
                }
            }
        }

        private Artist inputArtist;
        public Artist InputArtist
        {
            get { return inputArtist; }
            set => SetProperty(ref inputArtist, value);
        }

        private int? inputSongID;
        public int? InputSongID
        {
            get { return inputSongID; }
            set => SetProperty(ref inputSongID, value);
        }

        private string inputTitle;
        public string InputTitle
        {
            get { return inputTitle; }
            set => SetProperty(ref inputTitle, value);
        }

        private string? inputGenre;
        public string? InputGenre
        {
            get { return inputGenre; }
            set => SetProperty(ref inputGenre, value);
        }

        private int? inputArtistID;
        public int? InputArtistID
        {
            get { return inputArtistID; }
            set => SetProperty(ref inputArtistID, value);
        }

        public bool IsButtonExecutable()
        {
            return SelectedItem != null;
        }

        public RestCollection<Song> Songs { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public SongEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Songs = new RestCollection<Song>("http://localhost:53910/", "Song", "hub");
            }
        }

        [RelayCommand]
        public void Create()
        {
            if (!string.IsNullOrWhiteSpace(InputTitle) && !string.IsNullOrWhiteSpace(InputGenre) && InputArtistID != null)
            {
                var song = new Song
                {

                    Title = InputTitle,
                    Genre = InputGenre,
                    ArtistID = (int)InputArtistID
                };
                Songs.Add(song);
            }
            else
            {
                MessageBox.Show("Please fill in all fields correctly!");
            }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Update()
        {
            if (SelectedItem != null && !string.IsNullOrWhiteSpace(InputTitle) && !string.IsNullOrWhiteSpace(InputGenre) && InputArtistID != null)
            {
                try
                {
                    SelectedItem.Title = InputTitle;
                    SelectedItem.Genre = InputGenre;
                    SelectedItem.ArtistID = (int)InputArtistID;

                    Songs.Update(SelectedItem);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields correctly!");
            }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Delete()
        {
            try
            {
                Songs.Delete(SelectedItem.SongID);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }   
            SelectedItem = null;
        }

        [RelayCommand]
        public void Return(Window thisWindow)
        {
            var window = new MainWindow();
            window.Show();
            thisWindow.Close();
        }
    }
}
