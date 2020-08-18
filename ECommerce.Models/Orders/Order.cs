using ECommerce.Models.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.Models.Orders
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string AdressDeliveryStreetName { get; set; }
        [Required]
        public string AdressDeliveryPostalCode { get; set; }
        [Required]
        public string AdressDeliveryCountry { get; set; }
        [Required]
        public string AdressDeliveryCity { get; set; }
        public string AdressDeliveryOption_1 { get; set; }
        public string AdressDeliveryOption_2 { get; set; }

        [Required]
        public string AdressFacturationStreetName { get; set; }
        [Required]
        public string AdressFacturationPostalCode { get; set; }
        [Required]
        public string AdressFacturationCountry { get; set; }
        [Required]
        public string AdressFacturationCity { get; set; }       
        public string AdressFacturationOption_1 { get; set; }
        public string AdressFacturationOption_2 { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double DeliveryCost { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public Client Client { get; set; }

        public ICollection<Product_Order> OrderDetails { get; set; }

        //[Required]
        //public string ClientId { get; set; }

    }
}
