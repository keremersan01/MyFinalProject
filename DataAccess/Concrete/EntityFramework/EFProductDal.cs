using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDal : EFEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailsDTO> GetProductDetails()
        {
            using(var context = new NorthwindContext())
            {
                var result = from product in context.Products
                               join category in context.Categories
                               on product.CategoryId equals category.CategoryId
                               select new ProductDetailsDTO()
                               {
                                   ProductID = product.ProductId,
                                   ProductName = product.ProductName,
                                   CategoryName = category.CategoryName,
                                   UnitsInStock = product.UnitsInStock
                               };

                return result.ToList();
            }
        }
    }
}
