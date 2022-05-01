using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record SendModelToEditMessage<T> : Message<T>
        where T : IModel
    {
    }
}
