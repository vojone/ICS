using Carpool.BL.Models;

namespace Carpool.App.Messages
{
    public record NewMessage<T> : Message<T>
        where T : IModel
    {
    }
}
