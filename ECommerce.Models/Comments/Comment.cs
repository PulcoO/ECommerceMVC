using ECommerce.Models.Clients;
using ECommerce.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.Models.Comments
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [Required]
        private Client Client { get; set; }
        [Required]
        private Product Product { get; set; }

        //[ForeignKey("ClientFK")]
        //[Required]
        //public string ClientId { get; set; }
        //[ForeignKey("ProductFk")]
        //[Required]
        //public int ProductId { get; set; }


    }
}
