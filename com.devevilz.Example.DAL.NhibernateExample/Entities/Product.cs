using com.devevil.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevilz.Example.DAL.NhibernateExample.Entities
{
    public class Product : EntityBase<int, Product>
    {
        public virtual String Name { get; set; }
        public virtual Decimal Price { get; set; }

        public virtual Shelf Shelf { get; set; }
        public Product() { }

        protected override bool IsValidState()
        {
            return true;
        }
    }
}
