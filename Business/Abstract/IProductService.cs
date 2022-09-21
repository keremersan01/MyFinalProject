using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<ProductDetailsDTO>> GetProductDetails();
        IResult Add(Product product);
        IDataResult<Product> GetProductById(int id);
        IResult Update(Product product);
    }
}
