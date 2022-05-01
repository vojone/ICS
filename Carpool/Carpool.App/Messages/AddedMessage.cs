using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record AddedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
