using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;

namespace Carpool.App.ViewModel
{
    public class ProfileUserDetailViewModel : UserDetailViewModelBase, IProfileUserDetailViewModel
    {
        private readonly ISession _session;

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
            DisplayCarEditCommand = new AsyncRelayCommand(OnDisplayCarEdit);
            DisplayRideListCommand = new RelayCommand(OnDisplayRideList);
        }


        public ICommand SaveChangesCommand { get; set; }

        public ICommand DeleteAccountCommand { get; set; }

        public ICommand LogOutCommand { get; set; }

        public ICommand DisplayCarEditCommand { get; set; }

        public ICommand DisplayRideListCommand { get; set; }


        private bool CanSave()
        {
            return Model is {HasErrors: false};
        }


        private async Task OnSaveChanges()
        {
            await SaveAsync();
        }


        private async Task OnDeleteAccount()
        {
            await DeleteAsync();
            Mediator.Send(new DisplayLoginScreenMessage());
        }


        private void OnLogOut()
        {
            System.Diagnostics.Debug.WriteLine("Log Out...");
            _session.LogUserOut();
            Mediator.Send(new DisplayLoginScreenMessage());
        }


        private async Task OnDisplayCarEdit()
        {
            await LoadDefaultProfile();
        }


        private void OnDisplayRideList()
        {

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
        }


        //In this case "default" means profile of logged user or empty profile if there is no logged user
        private async Task LoadDefaultProfile()
        {
            var loggedUserId = _session.GetLoggedUser();

            if (loggedUserId != null)
            {
                await LoadAsync((Guid)loggedUserId);
            }
            else
            {
                Model = GetEmptyUser();
            }

            OnPropertyChanged();
        }
    }
}
