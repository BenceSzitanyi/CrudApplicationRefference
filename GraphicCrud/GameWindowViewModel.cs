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
using GraphicCrud.Models;

namespace GraphicCrud
{
    class GameWindowViewModel : ObservableRecipient
    {
        public RestCollection<VideoGame> Games { get; set; }

        public ICommand CreateGameCommand { get; set; }
        public ICommand DeleteGameCommand { get; set; }
        public ICommand UpdateGameCommand { get; set; }

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

        private VideoGame selectedGame;

        public VideoGame SelectedGame
        {
            get { return selectedGame; }
            set
            {
                if (value != null)
                {
                    selectedGame = new VideoGame()
                    {
                        VideoGameId = value.VideoGameId,
                        Title = value.Title,
                    };
                    OnPropertyChanged();
                    (DeleteGameCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateGameCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                /*SetProperty(ref selectedDeveloper, value);
                OnPropertyChanged();
                (DeleteDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();*/
            }
        }

        public GameWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Games = new RestCollection<VideoGame>("http://localhost:57640/", "game", "hub");

                CreateGameCommand = new RelayCommand(() =>
                {
                    Games.Add(new VideoGame()
                    {
                        Title = SelectedGame.Title,
                        MetacriticsScore = SelectedGame.MetacriticsScore,
                        ReleaseDate = SelectedGame.ReleaseDate,
                        Platform = SelectedGame.Platform
                    });
                });

                UpdateGameCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Games.Update(SelectedGame);
                    }
                    catch (Exception ex)
                    {

                        ErrorMessage = ex.Message;
                    }
                });
                DeleteGameCommand = new RelayCommand(() =>
                { Games.Delete(SelectedGame.VideoGameId); },
                () =>
                { return SelectedGame != null; }
                );
                //SelectedPublisher = new Publisher();
            }
        }
    }
}
