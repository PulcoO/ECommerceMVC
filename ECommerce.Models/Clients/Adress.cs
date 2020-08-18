using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.Models.Clients
{
    [Table("Adress")]
    public class Adress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public string Option_1 { get; set; }
        public string Option_2 { get; set; }
        [Required]
        public Client Client { get; set; }
        //[ForeignKey("ClientFK")]
        //[Required]
        //public string ClientId { get; set; }

    }
}
