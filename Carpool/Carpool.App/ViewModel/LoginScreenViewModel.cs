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
using CookBook.App.Extensions;

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

            DisplayUserCreateScreenCommand = new RelayCommand(OnDisplayUserCreateScreen);
            LogInCommand = new RelayCommand<Guid>(OnLogIn);
        }

        public ObservableCollection<UserListModel> Users { get; set; } = new();


        public ICommand DisplayUserCreateScreenCommand { get; set; }

        public ICommand LogInCommand { get; set; }


        private void OnDisplayUserCreateScreen()
        {
            System.Diagnostics.Debug.WriteLine("Going to User Create, sending");
            _mediator.Send(new DisplayUserCreateScreenMessage());
            _mediator.Send(new NewMessage<UserWrapper>());
        }

        private void OnLogIn(Guid userId)
        {
            System.Diagnostics.Debug.WriteLine("Logging In As user with Id:" + userId);
            _session.LogUserIn(userId);
            _mediator.Send(new LoadToEditMessage<UserWrapper>() {Id = userId});
            _mediator.Send(new DisplayUserProfileMessage());
        }

        public async Task LoadAsync()
        {
            Users.Clear();
            var users = await _userFacade.GetAsync();
            Users.AddRange(users);
        }
    }
}
