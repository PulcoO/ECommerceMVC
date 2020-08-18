using ECommerceMVC.Models.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceMVC.Models.Orders
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        public string AdressDeliveryStreetName { get; set; }
        
        public string AdressDeliveryPostalCode { get; set; }
        
        public string AdressDeliveryCountry { get; set; }
        
        public string AdressDeliveryCity { get; set; }

        public string AdressDeliveryOption_1 { get; set; }

        public string AdressDeliveryOption_2 { get; set; }

        
        public string AdressFacturationStreetName { get; set; }
        
        public string AdressFacturationPostalCode { get; set; }
        
        public string AdressFacturationCountry { get; set; }
        
        public string AdressFacturationCity { get; set; }       
        public string AdressFacturationOption_1 { get; set; }
        public string AdressFacturationOption_2 { get; set; }
        
        public double Weight { get; set; }
        
        public double DeliveryCost { get; set; }
        
        public double Total { get; set; }
     
        public string State { get; set; }
        
        public Client Client { get; set; }
        
        public string ClientId { get; set; }

        public ICollection<Product_Order> OrderDetails { get; set; }


    }
}
