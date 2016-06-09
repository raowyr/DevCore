using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevil.DAL.Core.RepositoryPattern
{
    public interface IRepositoryFind<T>
    {
        ICollection<T> FindAll();
        int Count();
        IList<T> FindPage(int pageStartRow, int pageSize);
        IList<T> FindSortedPage(int pageStartRow, int pageSize, string sortBy, bool descending);
    }
}
