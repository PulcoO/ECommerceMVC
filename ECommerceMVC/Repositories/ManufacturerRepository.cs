using ECommerceMVC.Data;
using ECommerceMVC.Models.Products;
using ECommerceMVC.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {

        private readonly ApplicationDbContext appDbContext;

        public ManufacturerRepository(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Manufacturer> AddManufacturer(Manufacturer Manufacturer)
        {
            var result = await appDbContext.Manufacturers.AddAsync(Manufacturer);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Manufacturer> DeleteManufacturer(int ManufacturerId)
        {
            var result = await appDbContext.Manufacturers
                .FirstOrDefaultAsync(e => e.Id == ManufacturerId);
            if (result != null)
            {
                appDbContext.Manufacturers.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturers()
        {
            return await appDbContext.Manufacturers.Include(m => m.Products).ToListAsync();
        }

        public async Task<Manufacturer> GetManufacturer(int ManufacturerId)
        {
            return await appDbContext.Manufacturers
                .Include(m => m.Products)
                .FirstOrDefaultAsync(e => e.Id == ManufacturerId);
        }

        public async Task<Manufacturer> GetManufacturerByName(string ManufacturerName)
        {
            return await appDbContext.Manufacturers
                .Include(e => e.Products)
                .FirstOrDefaultAsync(e => e.Name == ManufacturerName);
        }

        public async Task<Manufacturer> UpdateManufacturer(Manufacturer Manufacturer)
        {
            var result = await appDbContext.Manufacturers
                .FirstOrDefaultAsync(e => e.Id == Manufacturer.Id);
            if (result != null)
            {
                result.Id = Manufacturer.Id;
                result.Name = Manufacturer.Name;
                result.Products = Manufacturer.Products;
                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
        
    }
}
