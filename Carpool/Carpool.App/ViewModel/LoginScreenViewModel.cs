using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Model;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;

namespace Carpool.App.ViewModel
{
    public class LoginScreenViewModel : ViewModelBase, ILoginScreenViewModel
    {

        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private readonly ISession _session;


        public LoginScreenViewModel(UserFacade userFacade, IMediator mediator, ISession session)
        {
            _userFacade = userFacade;
            _mediator = mediator;
            _session = session;

            DisplayUserCreateScreenCommand = new RelayCommand(DisplayUserCreateScreen);
            LogInCommand = new RelayCommand<Guid>(LogUserIn);
        }

        public ObservableCollection<UserListModel> Users { get; set; } = new();


        public ICommand DisplayUserCreateScreenCommand { get; set; }

        public ICommand LogInCommand { get; set; }


        private void DisplayUserCreateScreen()
        {
            System.Diagnostics.Debug.WriteLine("Going to User Create, sending");
            _mediator.Send(new DisplayUserCreateScreenMessage());
            _mediator.Send(new NewMessage<UserWrapper>());
        }

        private void LogUserIn(Guid userId)
        {
            System.Diagnostics.Debug.WriteLine("Logging In As user with Id:" + userId);
            _session.LogUserIn(userId);
            _mediator.Send(new LoadUserProfileMessage());
            _mediator.Send(new DisplayUserProfileMessage());
        }

        public async Task LoadAsync()
        {
            Users.Clear();
            var users = await _userFacade.GetAsync();

            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
    }
}
