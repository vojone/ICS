using System;

namespace Carpool.BL.Models
{
    public abstract record ModelBase : IModel
    {
        public Guid Id { get; set; }
    }
}
