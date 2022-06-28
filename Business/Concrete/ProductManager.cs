using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

        public IResult Add(Product product)
        {
            if(product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            // iş kodları
            // yetkisi var mı? 
            // geçerse return et gibi.

            if (DateTime.Now.Hour == 15)
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);

           return new SuccessDataResult<List<Product>>(_productDal.GetAll(), true, Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByCategoryId(int id)
        {
            return new SuccessDataResult<List< Product >>(_productDal.GetAll(product => product.CategoryId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(int min, int max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(
                product => product.UnitPrice >= min && product.UnitPrice <= max));
        }

        public IDataResult<Product> GetProductById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(product => product.ProductId == id));
        }

        public IDataResult<List<ProductDetailsDTO>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailsDTO>>(_productDal.GetProductDetails());
        }
    }
}
