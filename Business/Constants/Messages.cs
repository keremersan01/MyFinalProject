using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "product is added!";
        public static string ProductNameInvalid = "Product Name is invalid!";
        public static string MaintenanceTime = "System is under maintenance";
        public static string ProductsListed = "Products are listed!";
        public static string ProductCountForSameCategoryIsInvalid = "Bir categoride maksimum 10 adet ürün bulunabilir!";
        public static string ProductNameIsAlreadyExist = "Bu ürün adı zaten kullanılıyor!";
        public static string CategoryLimitExceeded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
    }
}
