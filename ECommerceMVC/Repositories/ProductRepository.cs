using ECommerceMVC.Data;
using ECommerceMVC.Interfaces;
using ECommerceMVC.Models.Orders;
using ECommerceMVC.Models.Products;
using ECommerceMVC.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        // GET / ALL
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await appDbContext.Products.ToListAsync();
        }
        // GET / 1
        public async Task<Product> GetProduct(int ProductId)
        {
            return await appDbContext.Products
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(e => e.Id == ProductId);
        }
        // ADD / Product
        public async Task<Product> AddProduct(Product Product)
        {
            var result = await appDbContext.Products.AddAsync(Product);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        // DELETE // 1
        public async Task<Product> DeleteProduct(int ProductId)
        {
            var product = await appDbContext.Products.FindAsync(ProductId);
            if (product != null)
            {
                //implementer le mapping
                await appDbContext.SaveChangesAsync();

                return product;
            }
            appDbContext.Products.Remove(product);
            await appDbContext.SaveChangesAsync();
            return product;
        }
        // UPDATE / Product
        public async Task<Product> UpdateProduct(Product Product)
        {
            var result = await appDbContext.Products
                .FirstOrDefaultAsync(e => e.Id == Product.Id);
            if (result != null)
            {
                //implementer le mapping
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
        // GET / Name
        public async Task<Product> GetProductByName(string ProductName)
        {
            return await appDbContext.Products
                .FirstOrDefaultAsync(e => e.Name == ProductName);
        }

        // GET / Top5
        public async Task<IEnumerable<DAOProductIdQuantitySum>> Top5()
        {
            IEnumerable<DAOProductIdQuantitySum> query = await appDbContext.Set<Product_Order>()
                .GroupBy(x => x.ProductId)
                .Select(x => new DAOProductIdQuantitySum { ProductId = x.Key, QuantitySum = x.Sum(a => a.Quantity) })
                .OrderByDescending(x => x.QuantitySum)
                .Take(5)
                .ToListAsync();

            return query;
        }
    }
}
