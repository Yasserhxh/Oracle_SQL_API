using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public interface ISqlUnitOfWork
    {
        Task<int> Complete();
        IDbContextTransaction BeginTransaction();
    }
}
