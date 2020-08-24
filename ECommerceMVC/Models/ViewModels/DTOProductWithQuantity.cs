using ECommerceMVC.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Models.ViewModels
{
    public class DTOProductWithQuantity
    {
        public Product product { get; set; }
        public int quantity { get; set; }
    }
}
