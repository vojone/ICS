using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using CookBook.App.Extensions;

namespace Carpool.App.ViewModel
{
    public class CarEditViewModel : ViewModelBase, ICarEditViewModel
    {
        private readonly IMediator _mediator;

        private readonly CarFacade _carFacade;



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
            SelectCarCommand = new AsyncRelayCommand<Guid>(OnSelectCar);
        }

        public ICommand GoBackCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand NewCarCommand { get; }
        public ICommand SelectCarCommand { get; }

        public CarWrapper? Model { get; private set; }

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

        public async Task OnSelectCar(Guid carId)
        {
            await LoadAsync(carId);
            OnPropertyChanged();
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


