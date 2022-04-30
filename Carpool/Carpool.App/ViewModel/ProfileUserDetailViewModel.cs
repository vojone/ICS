using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using Microsoft.Win32;

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

            mediator.Register<LoadUserProfileMessage>(OnLoadUserProfileMessage);

            SaveChangesCommand = new AsyncRelayCommand(OnSaveChanges, CanSave);
            DeleteAccountCommand = new AsyncRelayCommand(OnDeleteAccount);
            LogOutCommand = new RelayCommand(OnLogOut);
            DisplayCarEditCommand = new RelayCommand(OnDisplayCarEdit);
            DisplayRideListCommand = new RelayCommand(OnDisplayRideList);
        }


        public ICommand SaveChangesCommand { get; set; }

        public ICommand DeleteAccountCommand { get; set; }

        public ICommand LogOutCommand { get; set; }

        public ICommand DisplayCarEditCommand { get; set; }

        public ICommand DisplayRideListCommand { get; set; }


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


        private async void OnLoadUserProfileMessage(LoadUserProfileMessage message)
        {
            if (message.UserId != null)
            {
                await LoadAsync((Guid)message.UserId);
            }
            else
            {
                await LoadDefaultProfile();
            }

            OnPropertyChanged();
            RememberCurrentModel();
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
