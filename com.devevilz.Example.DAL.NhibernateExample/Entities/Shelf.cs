using com.devevil.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevilz.Example.DAL.NhibernateExample.Entities
{
    public class Shelf : EntityBase<int, Shelf>
    {
        public virtual string Code { get; set; }
        public virtual IList<Product> Products { get; set; }

        public Shelf()
        {
            Products = new List<Product>();
        }

        public virtual void AddProduct(Product p)
        {
            p.Shelf = this;
            Products.Add(p);
        }

        protected override bool IsValidState()
        {
            throw new NotImplementedException();
        }
    }
}
