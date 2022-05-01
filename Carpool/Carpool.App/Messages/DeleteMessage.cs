using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record DeleteMessage<T> : Message<T>
        where T : IModel
    {
    }
}
