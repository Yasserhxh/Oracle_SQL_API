using Microsoft.EntityFrameworkCore.Storage;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly SqlDbContext context;

        public SqlUnitOfWork(SqlDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }
    }
}
