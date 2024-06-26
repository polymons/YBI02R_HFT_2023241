﻿using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.WPFClient.Services;

namespace YBI02R_HFT_2023241.WPFClient.ViewModels
{
    partial class ArtistEditorViewModel : ObservableRecipient
    {
        private string responseMessage;
        public string ResponseMessage
        {
            get { return responseMessage; }
            set { SetProperty(ref responseMessage, value); }
        }

        private Artist selectedItem;
        public Artist SelectedItem
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
        private Artist inputItem;
        public Artist InputItem
        {
            get { return inputItem; }
            set
            {
                SetProperty(ref inputItem, value);
                if (value != null)
                {
                    InputName = value.Name;
                    InputStudioID = value.StudioID;
                    InputAge = value.Age;
                }
                else
                {
                    InputName = null;
                    InputStudioID = null;
                    InputAge = null;
                }
            }
        }

        //private int? inputID;
        //public int? InputID
        //{
        //    get { return inputID; }
        //    set => SetProperty(ref inputID, value);
        //}

        private string? inputName;
        public string? InputName
        {
            get { return inputName; }
            set => SetProperty(ref inputName, value);
        }

        private int? inputStudioID;
        public int? InputStudioID
        {
            get { return inputStudioID; }
            set => SetProperty(ref inputStudioID, value);
        }

        private int? inputAge;
        public int? InputAge
        {
            get { return inputAge; }
            set => SetProperty(ref inputAge, value);
        }

        public bool IsButtonExecutable()
        {
            return SelectedItem != null;
        }

        public RestCollection<Artist> Artists { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public ArtistEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Artists = new RestCollection<Artist>("http://localhost:53910/", "Artist", "hub");
            }
        }

        [RelayCommand]
        public void Create()
        {
            if (InputName != null && InputName != "" && InputStudioID != null && InputAge != null)
            {
                Artists.Add(new Artist(InputName, (int)InputStudioID, (int)InputAge));
                ResponseMessage = "Created";
            }
            else { ResponseMessage = "Wrong input"; }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Update()
        {
            if (InputName != null && InputName != "" && InputStudioID != null && InputAge != null)
            {
                SelectedItem.Name = InputName;
                SelectedItem.StudioID = (int)InputStudioID;
                SelectedItem.Age = (int)InputAge;
                Artists.Update(SelectedItem);
                ResponseMessage = "Updated";
            }
            else { ResponseMessage = "Wrong input"; }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Delete()
        {
            Artists.Delete(SelectedItem.ArtistID);
            ResponseMessage = "Deleted";
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