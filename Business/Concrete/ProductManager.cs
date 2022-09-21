using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        ICategoryService _categoryService;
        // Dependency Injection With Constructor
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // Business codes
            // bir categoride max 10 ürün olabilir.
            // Aynı isimde ürün eklenemez

            IResult results = BusinessRules.Run(CheckIfProductCountForCategoryCorrect(product.CategoryId),
                CheckIfProductNameIsExists(product.ProductName), CheckIfCategoryLimitExceeded());
            if (results != null)
            {
                return results;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        public IDataResult<List<Product>> GetAll()
        {
            // iş kodları
            // yetkisi var mı? 
            // geçerse return et gibi.

            if (DateTime.Now.Hour == 23)
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), true, Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(product => product.CategoryId == id));
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

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountForCategoryCorrect(product.CategoryId).IsSuccess)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductCountForCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
                return new ErrorResult(Messages.ProductCountForSameCategoryIsInvalid);
            return new SuccessResult();
        }
        private IResult CheckIfProductNameIsExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName);
            if (result != null)
                return new ErrorResult(Messages.ProductNameIsAlreadyExist);
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll().Data.Count;
            if(result > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
