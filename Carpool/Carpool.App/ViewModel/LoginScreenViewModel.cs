using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Model;

namespace Carpool.App.ViewModel
{
    internal class LoginScreenViewModel
    {
        public ICollection<UserLoginMenu> Items { get; set; } = new List<UserLoginMenu>()
        {
            
        };
    }
}
