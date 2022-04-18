using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
