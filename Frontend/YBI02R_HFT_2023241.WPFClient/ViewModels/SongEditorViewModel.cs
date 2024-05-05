using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.WPFClient.ViewModels
{
    partial class SongEditorViewModel : ObservableRecipient
    {
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
            get { return inputItem; }
            set
            {
                SetProperty(ref inputItem, value);
                if (value != null)
                {
                    InputId = value.SongID;
                    InputName = value.Title;
                    InputBirthyear = value.Length;
                    InputWeight = value.Plays;
                    InputColor = value.ArtistID;
                }
                else
                {
                    InputId = null;
                    InputName = null;
                    InputBirthyear = null;
                    InputWeight = null;
                    InputColor = null;
                }
            }
        }

        private int? inputId;
        public int? InputId
        {
            get { return inputId; }
            set => SetProperty(ref inputId, value);
        }

        private string inputName;
        public string InputName
        {
            get { return inputName; }
            set => SetProperty(ref inputName, value);
        }

        private int? inputBirthyear;
        public int? InputBirthyear
        {
            get { return inputBirthyear; }
            set => SetProperty(ref inputBirthyear, value);
        }

        private int? inputWeight;
        public int? InputWeight
        {
            get { return inputWeight; }
            set => SetProperty(ref inputWeight, value);
        }

        private int? inputColor;
        public int? InputColor
        {
            get { return inputColor; }
            set => SetProperty(ref inputColor, value);
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
                Songs = new RestCollection<Song>("http://localhost:53910/", "song", "hub");
            }
        }

        [RelayCommand]
        public void Create()
        {
            if (InputId != null && InputName != null && InputName != "" && InputBirthyear != null && InputWeight != null && InputColor != null)
            {
                Songs.Add(new Song(InputName, null, (int)InputBirthyear, (int)InputWeight, (int)InputId, (int)InputColor));
            }
            else { MessageBox.Show("Wrong Input!"); }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Update()
        {
            if (InputId != null && InputName != null && InputName != "" && InputBirthyear != null && InputWeight != null && InputColor != null)
            {
                SelectedItem.Title = InputName;
                SelectedItem.Length = (int)InputBirthyear;
                SelectedItem.Plays = (int)InputWeight;
                Songs.Update(SelectedItem);
            }
            else { MessageBox.Show("Wrong Input!"); }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Delete()
        {
            Songs.Delete(SelectedItem.SongID);
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
