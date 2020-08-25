
using ECommerceMVC.Models.Clients;
using ECommerceMVC.Models.Orders;
using ECommerceMVC.Models.Products;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Services
{
    public class CommentsService
    {
        public static Boolean AllowComment (Client user, int productRecherche)
        {
            //if user or product envoyer une exeption
            foreach (Order order in user.Order)
            {
                foreach (Product_Order productOrder in order.OrderDetails)
                {
                    if (productOrder.ProductId == productRecherche)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
