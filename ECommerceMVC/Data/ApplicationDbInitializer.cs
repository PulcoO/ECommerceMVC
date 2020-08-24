using ECommerceMVC.Models.Clients;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<Client> userManager)
        {
            if (userManager.FindByEmailAsync("oominimathoo@gmail.com").Result == null)
            {
                Client user = new Client
                {
                    UserName = "Administration",
                    NormalizedUserName = "Administration".ToUpper(),
                    FirstName = "Mathieu",
                    LastName = "Veys",
                    Email = "oominimathoo@gmail.com",
                    NormalizedEmail = "oominimathoo@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = "0668320471"
                    //UserName = "oominimathoo@gmail.com",
                };

                IdentityResult result = userManager.CreateAsync(user, "0Guernica1937?").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
    }
}
