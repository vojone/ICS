using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.App.Services
{
    public class Session : ISession
    {
        private Guid? CurrentUserId { get; set; }

        public void LogUserIn(Guid userId)
        {
            CurrentUserId = userId;
        }

        public Guid? GetLoggedUser()
        {
            return CurrentUserId;
        }

        public void LogUserOut()
        {
            CurrentUserId = null;
        }
    }
}
