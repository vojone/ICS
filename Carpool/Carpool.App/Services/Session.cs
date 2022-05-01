using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.View;
using Carpool.App.ViewModel;
using Carpool.BL.Facades;
using Carpool.BL.Models;

namespace Carpool.App.Services
{
    public class Session : ISession
    {
        private readonly UserFacade _userFacade;


        public Session(UserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        private Guid? CurrentUserId { get; set; }

        private Stack<IViewModel> LastViewModels { get; init; } = new Stack<IViewModel>();


        public void LogUserIn(Guid userId)
        {
            CurrentUserId = userId;

        }

        public Guid? GetLoggedUserId()
        {
            return CurrentUserId;
        }

        public void LogUserOut()
        {
            CurrentUserId = null;
        }

        public void PushViewModel(IViewModel viewModel)
        {
            LastViewModels.Push(viewModel);
        }

        public IViewModel? GetLastViewModel()
        {
            return LastViewModels.Any() ? LastViewModels.Pop() : null;
        }
    }
}
