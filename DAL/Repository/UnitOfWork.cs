using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Entity;
using DAL.Interfaces;
using System.Data.Entity.Infrastructure;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            if (_context != null)
            {
                _context.SaveChanges();
                foreach (var entity in _context.ChangeTracker.Entries())
                {
                    ((IObjectContextAdapter)_context).ObjectContext.Detach(entity.Entity);
                }
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
            if (_context != null)
            {
                _context.Dispose();
            }
        }

    }
}