using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Context: DB tabloları ile proje classlarını bağlamak.
    public class NorthwindContext : DbContext
    {
        // projeyi hangi db ile bağladığımızı gösterdiğimiz method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // sql case - insensitive olduğu için connection stringde büyük küçük harf duyarsız.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }

        // hangi tablo hangi entity ile ilişkili bunu set ediyoruz.
        public DbSet<Product> Products{ get; set; }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
