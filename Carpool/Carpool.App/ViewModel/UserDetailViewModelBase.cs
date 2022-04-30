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
    public class UserDetailViewModelBase : ViewModelBase, IUserDetailViewModelBase
    {

        protected readonly UserFacade UserFacade;
        protected readonly IMediator Mediator;

        public UserDetailViewModelBase(UserFacade userFacade, IMediator mediator)
        {
            UserFacade = userFacade;
            Mediator = mediator;

            SelectPhotoCommand = new RelayCommand(OnSelectPhoto);
        }

        public UserWrapper? Model { get; protected set; }

        public ICommand SelectPhotoCommand { get; set; }


        private void OnSelectPhoto()
        {
            if (Model == null)
                return;
                
            OpenFileDialog file = new OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };

            var wasFileChosen = file.ShowDialog();

            if (wasFileChosen == true)
            {
                Model.PhotoUrl = file.FileName;
            }
        }


        protected async Task OnSaveUser()
        {
            await SaveAsync();
        }


        protected bool CanSaveUser()
        {
            return Model is { HasErrors: false };
        }


        public UserDetailModel GetEmptyUser()
        {
            return UserDetailModel.Empty;
        }


        public async Task LoadAsync(Guid id)
        {
            Model = await UserFacade.GetAsync(id) ?? GetEmptyUser();
        }


        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await UserFacade.SaveAsync(Model.Model);
            Mediator.Send(new UpdateMessage<UserWrapper> { Model = Model });
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
                    await UserFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    //TODO showing error msg
                }

                Mediator.Send(new DeleteMessage<UserWrapper>
                {
                    Model = Model
                });
            }
        }
    }
}
