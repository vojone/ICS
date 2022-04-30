using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;

namespace Carpool.App.ViewModel
{
    public class CarInfoViewModel : ViewModelBase, ICarInfoViewModel
    {
        private readonly CarFacade _carFacade;

        private readonly IMediator _mediator;

        public UserWrapper? CurrentUserModel { get; private set; }


        public CarInfoViewModel(
            CarFacade carFacade, 
            UserFacade userFacade,
            ISession session,
            IMediator mediator)
        {
            _carFacade = carFacade;
            _mediator = mediator;
            

            SaveCommand = new AsyncRelayCommand(OnSave);
            DeleteCommand = new AsyncRelayCommand(OnDelete);
            NewCarCommand = new RelayCommand(OnNewCar);
            GoBackCommand = new RelayCommand(OnGoBack);

            _mediator.Register<LoadedMessage<UserWrapper>>(OnUpdateUser);
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand NewCarCommand { get; }

        public CarWrapper? Model { get; private set; }

        private void OnUpdateUser(LoadedMessage<UserWrapper> msg)
        {
            if (msg.Model != null)
            {
                CurrentUserModel = msg.Model;
                OnPropertyChanged();
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

        private void OnGoBack()
        {
            _mediator.Send(new DisplayLastMessage());
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
            _mediator.Send(new UpdateMessage<CarWrapper> { Model = Model });
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
        }
    }
};


