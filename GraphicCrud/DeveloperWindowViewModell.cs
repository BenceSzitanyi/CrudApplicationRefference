using GraphicCrud.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.Input;

namespace GraphicCrud
{
    public class DeveloperWindowViewModell : ObservableRecipient
    {
        public RestCollection<Developer> Developers { get; set; }

        public ICommand CreateDeveloperCommand { get; set; }
        public ICommand DeleteDeveloperCommand { get; set; }
        public ICommand UpdateDeveloperCommand { get; set; }

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

        private Developer selectedDeveloper;

        public Developer SelectedDeveloper
        {
            get { return selectedDeveloper; }
            set
            {
                if (value != null)
                {
                    selectedDeveloper = new Developer()
                    {
                        DeveloperId = value.DeveloperId,
                        DeveloperName = value.DeveloperName,
                    };
                    OnPropertyChanged();
                    (DeleteDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                /*SetProperty(ref selectedDeveloper, value);
                OnPropertyChanged();
                (DeleteDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();*/
            }
        }

        public DeveloperWindowViewModell()
        {
            if (!IsInDesignMode)
            {
                Developers = new RestCollection<Developer>("http://localhost:57640/", "developer", "hub");

                CreateDeveloperCommand = new RelayCommand(() =>
                {
                    Developers.Add(new Developer()
                    {
                        DeveloperName = SelectedDeveloper.DeveloperName,
                    });
                });

                UpdateDeveloperCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Developers.Update(SelectedDeveloper);
                    }
                    catch (Exception ex)
                    {

                        ErrorMessage = ex.Message;
                    }
                });
                DeleteDeveloperCommand = new RelayCommand(() =>
                { Developers.Delete(SelectedDeveloper.DeveloperId); },
                () =>
                { return SelectedDeveloper != null; }
                );
                SelectedDeveloper = new Developer();
            }
        }
    }
}
