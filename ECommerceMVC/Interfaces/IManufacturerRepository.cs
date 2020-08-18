using ECommerceMVC.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<Manufacturer>> GetManufacturers();
        Task<Manufacturer> GetManufacturer(int ManufacturerId);
        Task<Manufacturer> GetManufacturerByName(string ManufacturerName);
        Task<Manufacturer> AddManufacturer(Manufacturer Manufacturer);
        Task<Manufacturer> UpdateManufacturer(Manufacturer Manufacturer);
        Task<Manufacturer> DeleteManufacturer(int ManufacturerId);
    }
}