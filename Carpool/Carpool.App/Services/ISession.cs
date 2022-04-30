using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.ViewModel;

namespace Carpool.App.Services
{
    public interface ISession
    {
        public Guid? GetLoggedUserId();

        public void LogUserIn(Guid userId);

        public void LogUserOut();

        public void PushViewModel(IViewModel viewModel);

        public IViewModel? GetLastViewModel();
    }
}
