using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record SelectedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
