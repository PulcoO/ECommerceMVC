using ECommerce.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.Models.Orders
{
    [Table("Product-Order")]
    public class Product_Order
    {
        [Key]
        public int Id { get; set; }
        
        //[ForeignKey("ProductFK")]
        [Required]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        //[ForeignKey("OrderFK")]
        [Required]
        public Order Order { get; set; }
        
        //public int OrderId { get; set; }
        //[Required]
        //public int Quantity { get; set; }
        [Required]
        public double ProductPrice { get; set; }


    }
}
