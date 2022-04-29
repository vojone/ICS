using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.App.Services
{
    public interface ISession
    {
        public Guid? GetLoggedUser();

        public void LogUserIn(Guid userId);

        public void LogUserOut();
    }
}
