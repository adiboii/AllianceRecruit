using BaseCode.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public DbContext Database { get; private set; }

        public UnitOfWork(BaseCodeEntities serviceContext)
        {
            Database = serviceContext;
        }

        public void SaveChanges()
        {
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
