using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevil.DAL.Generic.Core.RepositoryPattern
{
    public interface IRepository<T> : IRepositoryDelete<T>, IRepositoryRead<T>, IRepositoryFind<T>
    {

    }
}
