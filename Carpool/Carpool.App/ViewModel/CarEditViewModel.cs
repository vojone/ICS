using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using CookBook.App.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using AsyncRelayCommand = Carpool.App.Command.AsyncRelayCommand;
using RelayCommand = Carpool.App.Command.RelayCommand;

namespace Carpool.App.ViewModel
{
    public class CarEditViewModel : ViewModelBase, ICarEditViewModel
    {
        private readonly IMediator _mediator;

        private readonly CarFacade _carFacade;

        private CarWrapper? _carModel;

        private UserWrapper? _userModel;

        public CarEditViewModel(
            ISession session,
            CarFacade carFacade,
            IMediator mediator)
        {
            _mediator = mediator;

            _carFacade = carFacade;


            GoBackCommand = new RelayCommand(OnGoBack);

            _mediator.Register<SendModelToEditMessage<UserWrapper>>(OnSendToEdit);

            SaveCommand = new AsyncRelayCommand(OnSave);
            DeleteCommand = new AsyncRelayCommand(OnDelete);
            NewCarCommand = new RelayCommand(OnNewCar);
            SelectCarCommand = new Command.AsyncRelayCommand<Guid>(OnSelectCar, CanSelect);
            SelectPhotoCommand = new RelayCommand(OnSelectPhoto);
            ClearPhotoCommand = new RelayCommand(OnClearPhoto);
        }

        public ICommand GoBackCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand NewCarCommand { get; set; }
        public IAsyncRelayCommand SelectCarCommand { get; set; }

        public ICommand SelectPhotoCommand { get; set; }
        public ICommand ClearPhotoCommand { get; set; }

        public CarWrapper? Model
        {
            get => _carModel;
            set
            {
                _carModel = value;


                if (UserModel != null && _carModel != null)
                {
                    _carModel.OwnerId = UserModel.Id;
                }

                OnPropertyChanged();
            }
        }

        public UserWrapper? UserModel
        {
            get => _userModel;
            set
            {
                _userModel = value;

                
                if (value?.Model.Cars != null && UserModel != null)
                {
                    foreach (var car in value.Model.Cars)
                    {
                        UserModel.Cars.Add(car);
                    }
                }

                OnPropertyChanged();
            }
        }


        private void OnSendToEdit(SendModelToEditMessage<UserWrapper> message)
        {
            UserModel = message.Model;
        }

        private void OnGoBack()
        {
            _mediator.Send(new DisplayLastMessage());
        }

        private void OnClearPhoto()
        {
            if (Model != null)
            {
                Model.Photo = null;
            }
        }

        private void OnSelectPhoto()
        {
            if (Model == null)
                return;

            var file = new OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };

            var wasFileChosen = file.ShowDialog();

            if (wasFileChosen == true)
            {
                Model.Photo = file.FileName;
            }
        }


        private async Task OnSave()
        {
            await SaveAsync();
        }

        private async Task OnDelete()
        {
            var answer = MessageBox.Show(
                "The " + Model?.Name + " will be deleted forever.\nAre you sure?",
                "Delete car",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if (answer == MessageBoxResult.Yes && Model != null)
            {
                await DeleteAsync();
                _mediator.Send(new DisplayUserProfileMessage());
            }
        }

        private void OnNewCar()
        {
            Model = CarDetailModel.Empty;
        }

        private async Task OnSelectCar(Guid carId)
        {
            await LoadAsync(carId);
            SelectCarCommand.NotifyCanExecuteChanged();
        }

        private bool CanSelect(Guid carId)
        {
            return carId != Model?.Id;
        }

        public async Task LoadAsync(Guid id)
        {
            Model = await _carFacade.GetAsync(id) ?? CarDetailModel.Empty;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _carFacade.SaveAsync(Model.Model);

            _mediator.Send(new LoadToEditMessage<UserWrapper> { Id = UserModel?.Id });
        }

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }
            else
            {
                await _carFacade.DeleteAsync(Model);
            }

            _mediator.Send(new LoadToEditMessage<UserWrapper> { Id = UserModel?.Id });
        }
    }
}


