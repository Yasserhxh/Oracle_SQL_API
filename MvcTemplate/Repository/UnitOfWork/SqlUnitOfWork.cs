using Microsoft.EntityFrameworkCore.Storage;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public class SqlUnitOfWork : ISqlUnitOfWork
    {
        private readonly SqlDbContext Sqlcontext;

        public SqlUnitOfWork(SqlDbContext context)
        {
            this.Sqlcontext = context;
        }

        public async Task<int> Complete()
        {
            return await Sqlcontext.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return Sqlcontext.Database.BeginTransaction();
        }
    }
}
