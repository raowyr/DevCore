using com.devevilz.Example.DAL.NhibernateExample.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevilz.Example.DAL.NhibernateExample.Mapping
{
    public class ShelfMapping : ClassMap<Shelf>
    {
        public ShelfMapping() {
            Id(x => x.Id);
            HasMany(x => x.Products).Inverse().Cascade.SaveUpdate();
        }
    }
}
