using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.Messages;

namespace Carpool.App.Services
{
    //From example project "CookBook"
    public interface IMediator
    {
        void Register<TMessage>(Action<TMessage> action)
            where TMessage : IMessage;
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;
        void UnRegister<TMessage>(Action<TMessage> action)
            where TMessage : IMessage;
    }
}
