using com.devevil.DAL.Nhibernate.Base;
using com.devevilz.Example.DAL.NhibernateExample.Entities;
using NHibernate;

namespace com.devevilz.Example.DAL.NhibernateExample.Repository
{
    public class ShelfRepository : GenericRepository<Shelf>
    {
        public ShelfRepository(ISession session) : base(session) { }
    }
}
