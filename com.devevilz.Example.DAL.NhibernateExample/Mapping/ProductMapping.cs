using com.devevilz.Example.DAL.NhibernateExample.Entities;
using FluentNHibernate.Mapping;

namespace com.devevilz.Example.DAL.NhibernateExample.Mapping
{
    public class ProductMapping : ClassMap<Product>
    {
        public ProductMapping()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Price);
        }
    }
}
