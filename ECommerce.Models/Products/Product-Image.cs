using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.Models.Products
{
    [Table("Product_Images")]
    public class Product_Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Product Product { get; set; }
        //[Required]
        //public int ProductId { get; set; }
    }
}
