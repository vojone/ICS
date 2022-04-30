using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.App.Messages
{
    public class LoadUserProfileMessage : IMessage
    {
        public LoadUserProfileMessage(Guid? userId = null)
        {
            UserId = null;
        }

        public Guid? UserId { get; set; }
    }
}
