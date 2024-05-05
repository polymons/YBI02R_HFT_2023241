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
    partial class PublisherEditorViewModel : ObservableRecipient
    {
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
                    InputId = value.StudioID;
                    InputStudioName = value.StudioName;
                    InputCountry = value.Country;
                    InputCity = value.City;
                }
                else
                {
                    InputId = null;
                    InputStudioName = null;
                    InputCountry = null;
                    InputCity = null;
                }
            }
        }

        private int? inputId;
        public int? InputId
        {
            get { return inputId; }
            set => SetProperty(ref inputId, value);
        }

        private string inputStudioName;
        public string InputStudioName
        {
            get { return inputStudioName; }
            set => SetProperty(ref inputStudioName, value);
        }

        private string inputCountry;
        public string InputCountry
        {
            get { return inputCountry; }
            set => SetProperty(ref inputCountry, value);
        }

        private string inputCity;
        public string InputCity
        {
            get { return inputCity; }
            set => SetProperty(ref inputCity, value);
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
                Publishers = new RestCollection<Publisher>("http://localhost:53910/", "publisher", "hub");
            }
        }

        [RelayCommand]
        public void Create()
        {
            if (InputId != null && InputStudioName != null && InputStudioName != "" && InputCountry != null && InputCity != null)
            {
                Publishers.Add(new Publisher(InputCountry, InputStudioName, InputCity, (int)InputId));
            }
            else { MessageBox.Show("Wrong Input!"); }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Update()
        {
            if (InputId != null && InputStudioName != null && InputStudioName != "" && InputCountry != null && InputCity != null)
            {
                SelectedItem.StudioID = (int)InputId;
                SelectedItem.StudioName = InputStudioName;
                SelectedItem.Country = InputCountry;
                SelectedItem.City = InputCity;
                Publishers.Update(SelectedItem);
            }
            else { MessageBox.Show("Wrong Input!"); }
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
