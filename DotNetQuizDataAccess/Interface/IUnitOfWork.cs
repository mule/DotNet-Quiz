using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetQuizDataAccess.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }

}
