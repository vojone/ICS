using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record UpdateMessage<T> : Message<T>
        where T : IModel
    {
    }
}
