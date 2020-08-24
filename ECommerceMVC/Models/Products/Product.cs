using ECommerceMVC.Models.Comments;
using ECommerceMVC.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Text;

namespace ECommerceMVC.Models.Products
{
    [Table("Product")]
    public class Product
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [Required, Display(Name = "Name")]
        public string Name { get; set; }


        [Required, Display(Name = "Short Description")]
        public string Description_Short { get; set; }


        [Required, Display(Name = "Long Description")]
        public string Description_Long { get; set; }


        [Required, Display(Name = "Weight")]
        public double Weight { get; set; }


        [Required, Display(Name = "Price")]
        public double Price { get; set; }


        [EnumDataType(typeof(Color))]
        [Required, Display(Name = "Principal Color")]
        public virtual Color PrincipalColor { get; set; }


        //Impossible de mettre required ici sinon il me fait chier ce con de datacontext ! DEBILOS !
        public virtual Manufacturer Manufacturer { get; set; }


        [Required, Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        

        public double? Height { get; set; }

        public double? Width { get; set; }

        public double? Lenght { get; set; }
        
        public virtual ICollection<Product_Image> Product_Images { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
        
        public virtual ICollection<Product_Order> Product_Orders { get; set; }

    }
}
