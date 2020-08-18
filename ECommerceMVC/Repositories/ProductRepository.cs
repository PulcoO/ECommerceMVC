using ECommerceMVC.Data;
using ECommerceMVC.Interfaces;
using ECommerceMVC.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext appDbContext;

        public ProductRepository(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Product> AddProduct(Product Product)
        {
            var result = await appDbContext.Products.AddAsync(Product);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public Task<Product> DeleteProduct(int ProductId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await appDbContext.Products
                .Include(p => p.Comments).ThenInclude(c=>c)
                .Include(p => p.Product_Images)
                .Include(p => p.Product_Orders).ThenInclude(po => po.Order)
                .Include(p => p.Manufacturer)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(int ProductId)
        {
            return await appDbContext.Products
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(e => e.Id == ProductId);
        }

        public async Task<Product> GetProductByName(string ProductName)
        {
            return await appDbContext.Products
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(e => e.Name == ProductName);
        }

        public async Task<Product> UpdateProduct(Product Product)
        {
            var result = await appDbContext.Products
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(e => e.Id == Product.Id);
            if (result != null)
            {
                //implementer le mapping
                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
