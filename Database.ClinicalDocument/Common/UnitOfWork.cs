using System;
using System.Diagnostics.CodeAnalysis;
using Database.Abstraction.ClinicalDocument.Common;
using Microsoft.EntityFrameworkCore;

namespace Database.ClinicalDocument.Common
{
    /// <summary>
    /// Unit of work implementation
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// private property for db context 
        /// </summary>
        private readonly DbContext dataContext;

        /// <summary>
        /// private property for disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// instantiate db context object
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(DbContext dbContext)
        {
            this.dataContext = dbContext;
        }

        /// <summary>
        /// save changes to database
        /// </summary>
        public void Commit()
        {
            this.dataContext.SaveChanges();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose  virtual method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Destructor of unit of work
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(true);
        }
    }
}
