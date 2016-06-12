using com.devevil.DAL.Nhibernate;
using com.devevil.DAL.Nhibernate.Base;
using com.devevilz.Example.DAL.NhibernateExample.Entities;
using com.devevilz.Example.DAL.NhibernateExample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.devevilz.Example.DAL.NhibernateExample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SessionManager.Instance.Configure(Assembly.GetExecutingAssembly());
                SessionManager.Instance.DropAndBuildSchema();

                using (UnitOfWork uow = new UnitOfWork())
                {
                    ShelfRepository s = new ShelfRepository(uow.Current);
                    Shelf she = new Shelf() { Code = "AAA" };
                    Product pp = new Product() { Price = 10, Name = "cd" };
                    she.AddProduct(pp);
                    s.Save(she);

                    uow.Commit();
                }
                Console.ReadKey();
            }
            catch (Exception e)
            { }
        }
    }
}
