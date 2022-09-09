using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using AsyncRelayCommand = Carpool.App.Command.AsyncRelayCommand;
using RelayCommand = Carpool.App.Command.RelayCommand;

namespace Carpool.App.ViewModel
{
    public class ProfileUserDetailViewModel : UserDetailViewModelBase, IProfileUserDetailViewModel
    {
        private readonly ISession _session;

        private UserDetailModel? _origModel;
        

        public ProfileUserDetailViewModel(
            UserFacade userFacade, 
            IMediator mediator,
            ISession session) : base(userFacade, mediator)
        {
            _session = session;

            mediator.Register<LoadToEditMessage<UserWrapper>>(OnLoadUser);

            SaveChangesCommand = new AsyncRelayCommand(OnSaveChanges, CanSave);
            DeleteAccountCommand = new AsyncRelayCommand(OnDeleteAccount);
            LogOutCommand = new RelayCommand(OnLogOut);
            DisplayCarEditCommand = new RelayCommand(OnDisplayCarEdit);
            DisplayRideListCommand = new RelayCommand(OnDisplayRideList);
            DisplayCreateRideCommand = new RelayCommand(OnDisplayCreateRide);
        }


        public IAsyncRelayCommand SaveChangesCommand { get; }

        public ICommand DeleteAccountCommand { get; }

        public ICommand LogOutCommand { get; }

        public ICommand DisplayCarEditCommand { get; }

        public ICommand DisplayRideListCommand { get; }

        public ICommand DisplayCreateRideCommand { get; }


        private bool CanSave()
        {

            if (Model == null)
            {
                return true;
            }

            bool hasChanged = !_origModel?.DataEquals(Model.Model) ?? true;

            return !Model.HasErrors && hasChanged;
        }

        private async Task OnSaveChanges()
        {
            await SaveAsync();
            RememberCurrentModel();
            SaveChangesCommand.NotifyCanExecuteChanged();
        }


        private async Task OnDeleteAccount()
        {
            var answer = MessageBox.Show(
                "The " + Model?.Name + " " + Model?.Surname + " will be deleted forever.\nAre you sure?",
                "Delete account",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if (answer == MessageBoxResult.Yes && Model != null)
            {
                await DeleteAsync();
                Mediator.Send(new DisplayLoginScreenMessage());
            }
        }


        private void OnLogOut()
        {
            _session.LogUserOut();
            Mediator.Send(new DisplayLoginScreenMessage());
            Model = GetEmptyUser();
        }


        private void OnDisplayCarEdit()
        {
            Mediator.Send(new DisplayCarInfoMessage());
        }


        private void OnDisplayRideList()
        {
            Mediator.Send(new DisplayRideListMessage());
        }

        private void OnDisplayCreateRide()
        {
            Mediator.Send(new DisplayCreateRideMessage());
        }


        //This is event handler so "async void" should be ok
        private async void OnLoadUser(LoadToEditMessage<UserWrapper> message)
        {
            if (message.Id != null)
            {
                await LoadAsync((Guid)message.Id);
            }
            else
            {
                await LoadDefaultProfile();
            }

            OnPropertyChanged();
            RememberCurrentModel();

            Mediator.Send(new SendModelToEditMessage<UserWrapper>() { Model = this.Model });
        }


        private void RememberCurrentModel()
        {
            if (Model == null)
            {
                return;
            }

            _origModel = new UserDetailModel(
                Model.Name ?? string.Empty, Model.Surname ?? string.Empty,
                Model.RegistrationDate, Model.PhotoUrl,
                Model.Country, Model.Rating);
        }


        //In this case "default" means profile of logged user or empty profile if there is no logged user
        private async Task LoadDefaultProfile()
        {
            var loggedUserId = _session.GetLoggedUserId();

            if (loggedUserId != null)
            {
                await LoadAsync((Guid)loggedUserId);
            }
            else
            {
                Model = GetEmptyUser();
            }
        }
    }
}
