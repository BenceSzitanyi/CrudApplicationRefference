using GraphicCrud.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace GraphicCrud
{
    class PublisherWindowViewModel:ObservableRecipient
    {
        public RestCollection<Publisher> Publishers { get; set; }

        public ICommand CreatePublisherCommand { get; set; }
        public ICommand DeletePublisherCommand { get; set; }
        public ICommand UpdatePublisherCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private Publisher selectedPublisher;

        public Publisher SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                if (value != null)
                {
                    selectedPublisher = new Publisher()
                    {
                        PublisherId = value.PublisherId,
                        PublisherName = value.PublisherName,
                    };
                    OnPropertyChanged();
                    (DeletePublisherCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdatePublisherCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                /*SetProperty(ref selectedDeveloper, value);
                OnPropertyChanged();
                (DeleteDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();*/
            }
        }

        public PublisherWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Publishers = new RestCollection<Publisher>("http://localhost:57640/", "publisher", "hub");

                CreatePublisherCommand = new RelayCommand(() =>
                {
                    Publishers.Add(new Publisher()
                    {
                        PublisherName = SelectedPublisher.PublisherName,
                    });
                });

                UpdatePublisherCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Publishers.Update(SelectedPublisher);
                    }
                    catch (Exception ex)
                    {

                        ErrorMessage = ex.Message;
                    }
                });
                DeletePublisherCommand = new RelayCommand(() =>
                { Publishers.Delete(SelectedPublisher.PublisherId); },
                () =>
                { return SelectedPublisher != null; }
                );
                //SelectedPublisher = new Publisher();
            }
        }
    }
}
