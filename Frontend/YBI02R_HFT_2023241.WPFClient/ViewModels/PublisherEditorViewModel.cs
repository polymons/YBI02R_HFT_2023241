using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;
using System.Windows;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.WPFClient.Services;

namespace YBI02R_HFT_2023241.WPFClient.ViewModels
{
    partial class PublisherEditorViewModel : ObservableRecipient
    {
        private string responseMessage;
        public string ResponseMessage
        {
            get { return responseMessage; }
            set { SetProperty(ref responseMessage, value); }
        }

        private Publisher selectedItem;
        public Publisher SelectedItem
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

        private Publisher inputItem;
        public Publisher InputItem
        {
            get { return inputItem; }
            set
            {
                SetProperty(ref inputItem, value);
                if (value != null)
                {
                    InputID = value.StudioID;
                    InputStudioName = value.StudioName;
                    InputCountry = value.Country;
                }
                else
                {
                    InputID = null;
                    InputStudioName = null;
                    InputCountry = null;
                }
            }
        }

        private int? inputID;
        public int? InputID
        {
            get { return inputID; }
            set => SetProperty(ref inputID, value);
        }

        private string inputStudioName;
        public string InputStudioName
        {
            get { return inputStudioName; }
            set => SetProperty(ref inputStudioName, value);
        }

        private string? inputCountry;
        public string? InputCountry
        {
            get { return inputCountry; }
            set => SetProperty(ref inputCountry, value);
        }


        public bool IsButtonExecutable()
        {
            return SelectedItem != null;
        }

        public RestCollection<Publisher> Publishers { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public PublisherEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Publishers = new RestCollection<Publisher>("http://localhost:53910/", "Publisher", "hub");
            }
        }

        [RelayCommand]
        public void Create()
        {
            if (InputID != null && InputStudioName != null && InputStudioName != "" && InputCountry != null)
            {
                try
                {
                    Publishers.Add(new Publisher(InputCountry, InputStudioName, (int)InputID));
                }
                catch (Exception ex)
                {
                    ResponseMessage = ex.Message;
                }
            }
            else { ResponseMessage = "Wrong Input!"; }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Update()
        {
            if (InputID != null && InputStudioName != null && InputStudioName != "" && InputCountry != null)
            {
                try
                {
                    SelectedItem.StudioID = (int)InputID;
                    SelectedItem.StudioName = InputStudioName;
                    SelectedItem.Country = InputCountry;

                    Publishers.Update(SelectedItem);
                }
                catch (Exception ex)
                {
                    ResponseMessage = ex.Message;
                }
            }
            else { ResponseMessage = "Wrong Input!"; }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Delete()
        {
            Publishers.Delete(SelectedItem.StudioID);
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
