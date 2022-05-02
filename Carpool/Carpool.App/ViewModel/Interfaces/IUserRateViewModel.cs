using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Wrapper;

namespace Carpool.App.ViewModel
{
    public interface IUserRateViewModel : IDetailViewModel<UserWrapper>
    {
        public Task IncreaseRating(Guid id);
    }
}
