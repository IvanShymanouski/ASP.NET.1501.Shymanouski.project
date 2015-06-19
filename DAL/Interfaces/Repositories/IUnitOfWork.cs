using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
