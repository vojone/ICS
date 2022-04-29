using System.Threading.Tasks;

namespace Carpool.App.ViewModel
{
    //From example project "CookBook"
    public interface IListViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
