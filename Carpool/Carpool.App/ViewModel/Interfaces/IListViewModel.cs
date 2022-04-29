using System.Threading.Tasks;

namespace Carpool.App.ViewModel
{
    public interface IListViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
