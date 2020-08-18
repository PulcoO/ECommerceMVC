using ECommerceMVC.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Interfaces
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<Product_Image>> GetProduct_Image();
        Task<Product_Image> GetProduct_Image(int Product_ImageId);
        Task<Product_Image> GetProduct_ImageByTitle(string Title);
        Task<Product_Image> AddProduct_Image(Product_Image Product_Image);
        Task<Product_Image> UpdateProduct_Image(Product_Image Product_Image);
        Task<Product_Image> DeleteProduct_Image(int Product_ImageId);

    }
}
