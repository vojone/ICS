using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record LoadedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
