using ECommerceMVC.Models.Clients;
using ECommerceMVC.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceMVC.Models.Comments
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        
        private Client Client { get; set; }
        
        private Product Product { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public int ProductId { get; set; }


    }
}
