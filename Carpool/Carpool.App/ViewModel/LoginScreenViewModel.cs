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
            
            new UserLoginMenu() {Name = "RadekASDASDASASDASADASDASDASDAS", Surname = "MarekASDASDASDASDASDASDA"},
            new UserLoginMenu() {Name = "1", Surname = "Marek"},
            new UserLoginMenu() {Name = "2", Surname = "Marek"},
            new UserLoginMenu() {Name = "3", Surname = "Marek"},
            new UserLoginMenu() {Name = "4", Surname = "Marek"},
            new UserLoginMenu() {Name = "5", Surname = "Marek"},
            new UserLoginMenu() {Name = "6", Surname = "Marek"},
            new UserLoginMenu() {Name = "7", Surname = "Marek"},
            new UserLoginMenu() {Name = "8", Surname = "Marek"},

        };
    }
}
