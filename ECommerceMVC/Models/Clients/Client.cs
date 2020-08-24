using ECommerceMVC.Models.Comments;
using ECommerceMVC.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ECommerceMVC.Models.Clients
{
    [Table("Client")]
    public class Client : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Adress> Adresses { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Order { get; set; }

    }
}
