using ECommerce.Models.Comments;
using ECommerce.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Text;

namespace ECommerce.Models.Products
{
    [Table("Product")]
    public class Product
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(6)]
        public string Name { get; set; }
        [Required]
        public string Description_Short { get; set; }
        [Required]
        public string Description_Long { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Price { get; set; }
        //[Required]
        //public Color PrincipalColor { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Manufacturer Manufacturer { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ManufacturerId { get; set; }
        

        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Lenght { get; set; }
        
        public ICollection<Product_Image> Product_Images { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public ICollection<Product_Order> Product_Orders { get; set; }

    }
}
