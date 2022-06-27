using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        
        IProductDal _productDal;
        // Dependency Injection With Constructor
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            // iş kodları
            // yetkisi var mı? 
            // geçerse return et gibi.
           return _productDal.GetAll();
        }

        public List<Product> GetByCategoryId(int id)
        {
            return _productDal.GetAll(product => product.CategoryId == id);
        }

        public List<Product> GetByUnitPrice(int min, int max)
        {
            return _productDal.GetAll(
                product => product.UnitPrice >= min && product.UnitPrice <= max);
        }

        public List<ProductDetailsDTO> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
