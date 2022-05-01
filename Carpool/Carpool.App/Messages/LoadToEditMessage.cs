using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record LoadToEditMessage<T> : Message<T>
        where T : IModel
    {
    }
}
