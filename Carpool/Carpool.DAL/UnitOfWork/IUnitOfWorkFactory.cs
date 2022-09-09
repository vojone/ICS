using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.DAL.UnitOfWork
{
    // Taken from the sample project 'CookBook'
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
