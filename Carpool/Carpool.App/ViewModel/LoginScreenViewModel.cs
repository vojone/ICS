using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Model;
using Carpool.App.Services;
using Carpool.BL.Facades;
using Carpool.BL.Models;

namespace Carpool.App.ViewModel
{
    public class LoginScreenViewModel : ViewModelBase, ILoginScreenViewModel
    {

        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;

        public LoginScreenViewModel(UserFacade userFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;
        }

        public ObservableCollection<UserListModel> Users { get; set; } = new();

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
