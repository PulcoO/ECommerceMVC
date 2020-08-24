using ECommerceMVC.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceMVC.Models.Orders
{
    [Table("Product-Order")]
    public class Product_Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        public virtual Order Order { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }


    }
}
