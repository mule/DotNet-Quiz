using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetQuizDataAccess.Interface;
using Raven.Client;

namespace DotNetQuizDataAccess
{
    public class RavenUnitOfWork : IUnitOfWork
    {
        public IDocumentSession Context { get; private set; }

        public RavenUnitOfWork(IDocumentStore session)
        {
            Context = session.OpenSession();
        }

        #region IUnitOfWork Members

        public void Commit()
        {
            Context.SaveChanges();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }

            GC.SuppressFinalize(this);
        }

        #endregion
    }

}
