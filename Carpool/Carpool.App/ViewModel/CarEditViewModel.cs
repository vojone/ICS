using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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
using Microsoft.EntityFrameworkCore.Internal;
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

        private CarWrapper? _origCarModel;

        private UserWrapper? _userModel;

        private bool _isPersisted = true;

        public CarEditViewModel(
            CarFacade carFacade,
            IMediator mediator)
        {
            _mediator = mediator;

            _carFacade = carFacade;


            GoBackCommand = new RelayCommand(OnGoBack);

            _mediator.Register<SendModelToEditMessage<UserWrapper>>(OnSendToEdit);
            _mediator.Register<DisplayCarInfoMessage>(Init);

            SaveCommand = new AsyncRelayCommand(OnSave, CanSave);
            DeleteCommand = new AsyncRelayCommand(OnDelete, CanDelete);
            NewCarCommand = new RelayCommand(OnNewCar);
            SelectCarCommand = new Command.AsyncRelayCommand<Guid>(OnSelectCar, CanSelect);
            SelectPhotoCommand = new RelayCommand(OnSelectPhoto);
            ClearPhotoCommand = new RelayCommand(OnClearPhoto);
        }

        public ICommand GoBackCommand { get; set; }
        public IAsyncRelayCommand SaveCommand { get; set; }
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

                Model?.Validate();
                OnPropertyChanged();
            }
        }

        public UserWrapper? UserModel
        {
            get => _userModel;
            set
            {
                _userModel = value;


                OnPropertyChanged();
            }
        }

        private async Task SetDefaultCar()
        {
            if (UserModel?.Cars.Count > 0)
            {
                await OnSelectCar(UserModel.Cars[0].Id);
            }
            else
            {
                OnNewCar();
            }
        }

        private async void Init(DisplayCarInfoMessage message)
        {
            await SetDefaultCar();
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


        private bool CanSave()
        {
            if (Model == null)
            {
                return true;
            }

            bool hasChanged = !_origCarModel?.DataEquals(Model.Model) ?? true;

            return !Model.HasErrors && hasChanged;
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
            }
        }


        private void RememberCurrentModel()
        {
            if (Model == null)
            {
                return;
            }

            _origCarModel = new CarDetailModel(
                Model.Name ?? string.Empty, Model.Brand ?? string.Empty,
                Model.Photo ?? null, Model.Type,
                Model.Registration, Model.Seats, Model.OwnerId);
        }


        private void OnNewCar()
        {
            Model = CarDetailModel.Empty;
            _isPersisted = false;
            RememberCurrentModel();
        }

        private async Task OnSelectCar(Guid carId)
        {
            await LoadAsync(carId);
            _isPersisted = true;
            RememberCurrentModel();
            SelectCarCommand.NotifyCanExecuteChanged();
            SaveCommand.NotifyCanExecuteChanged();
        }

        private bool CanSelect(Guid carId)
        {
            return carId != Model?.Id;
        }

        public async Task LoadAsync(Guid id)
        {
            Model = await _carFacade.GetAsync(id) ?? CarDetailModel.Empty;
            RememberCurrentModel();
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _carFacade.SaveAsync(Model.Model);

            _mediator.Send(new LoadToEditMessage<UserWrapper> { Id = UserModel?.Id });

            SaveCommand.NotifyCanExecuteChanged();
            RememberCurrentModel();
            _isPersisted = true;
        }


        private bool CanDelete()
        {
            return _isPersisted;
        }

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                MessageBox.Show("Cannot delete null mode!", "Unable to delete car");
            }
            else
            {
                try
                {
                    await _carFacade.DeleteAsync(Model);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Car is linked to some rides!", "Unable to delete car");
                    return;
                }

                _mediator.Send(new LoadToEditMessage<UserWrapper> { Id = UserModel?.Id });

                await SetDefaultCar();
                SaveCommand.NotifyCanExecuteChanged();
                RememberCurrentModel();
            }
        }
    }
}


