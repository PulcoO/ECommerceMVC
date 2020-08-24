using System;
using System.Collections.Generic;
using System.Text;
using ECommerceMVC.Models.Clients;
using ECommerceMVC.Models.Comments;
using ECommerceMVC.Models.Orders;
using ECommerceMVC.Models.Products;
using Microsoft.AspNetCore.Identity;
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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Client>().HasData(
        //        new Client
        //        {
        //            Id = "53ceeaca-058e-4fb8-902f-eba523631489",
        //            UserName = "Administration",
        //            NormalizedUserName = "Administration".ToUpper(),
        //            FirstName = "Mathieu",
        //            LastName = "Veys",
        //            Email = "oominimathoo@gmail.com",
        //            NormalizedEmail = "oominimathoo@gmail.com".ToUpper(),
        //            EmailConfirmed = true,
        //            PasswordHash = "AQAAAAEAACcQAAAAEDYWa2oAngEKPJzFNdYiIktuLOlPkEMDAnIYXPIF2JuypIJGP8wmlUZoMlpzO6yP2g==",
        //            SecurityStamp = "VXY5D22WQ6OCWKQL23ZWJUT2QK3QD72I",
        //            PhoneNumber = "0668320471"
        //        }
        //    );
        //    modelBuilder.Entity<IdentityRole>().HasData(
        //        new IdentityRole { Name = "Super Administrator", NormalizedName = "SuperAdministrator".ToUpper() },
        //        new IdentityRole { Name = "Administrator", NormalizedName = "Administrator".ToUpper() },
        //        new IdentityRole { Name = "Moderator", NormalizedName = "Moderator".ToUpper() },
        //        new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() }
        //    );

        //}
    }
}
