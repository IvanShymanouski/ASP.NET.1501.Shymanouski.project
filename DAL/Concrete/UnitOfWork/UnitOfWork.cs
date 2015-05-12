using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Entity;
using DAL.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            this.Context = context;
            Debug.WriteLine("Unit of work create");
        }

        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }

        }

        public void Dispose()
        {
            Dispose(true);
            Debug.WriteLine("Context dispose!");
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Context != null)
            {
                Context.Dispose();
            }
        }

    }
}