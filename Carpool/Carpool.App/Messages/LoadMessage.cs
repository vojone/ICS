using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record LoadMessage<T> : Message<T>
        where T : IModel
    {
    }
}
