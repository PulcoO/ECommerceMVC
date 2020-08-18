using System;
using System.Collections.Generic;
using System.Text;
using ECommerceMVC.Models.Clients;
using ECommerceMVC.Models.Comments;
using ECommerceMVC.Models.Orders;
using ECommerceMVC.Models.Products;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product_Image> Products_Images { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product_Order> OrderDetails { get; set; }
    }
}
