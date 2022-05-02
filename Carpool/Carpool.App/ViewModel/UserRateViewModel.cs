using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Carpool.App.Command;
using Carpool.App.Messages;
using Carpool.App.Services;
using Carpool.App.Wrapper;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using Microsoft.Win32;


namespace Carpool.App.ViewModel
{
    public abstract class UserRateViewModel : ViewModelBase, IUserRateViewModel
    {

        private readonly UserFacade _userFacade;
        private readonly IMediator _meadiator;

        protected UserRateViewModel(UserFacade userFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _meadiator = mediator;
        }


        public UserWrapper? Model { get; protected set; }


        public async Task IncreaseRating(Guid id)
        {
            await _userFacade.IncreaseRatingAsync(id);
        }
        

        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? UserDetailModel.Empty;
        }


        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _userFacade.SaveAsync(Model.Model);
        }


        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                try
                {
                    await _userFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    //TODO showing error msg
                }
            }
        }
    }
}
