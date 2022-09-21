using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI
{
     class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
           // CategoryTest();

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EFCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
                Console.WriteLine($"Category Names: {category.CategoryName}");
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EFProductDal(), new CategoryManager(new EFCategoryDal()));

            var result = productManager.GetAll();

            if(result.IsSuccess)
            {
                foreach (var product in result.Data)
                    Console.WriteLine($"Product: {product.ProductName} ------ Category: {product.ProductId}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
    }
}