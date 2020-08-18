using ECommerce.Models.Comments;
using ECommerce.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Models.Clients
{
    [Table("Client")]
    public class Client : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Adress> Adresses { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Order> Order { get; set; }

    }
}
