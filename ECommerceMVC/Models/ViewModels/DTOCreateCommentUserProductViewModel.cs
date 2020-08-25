using ECommerceMVC.Models.Clients;
using ECommerceMVC.Models.Comments;
using ECommerceMVC.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Models.ViewModels
{
    public class DTOCreateCommentUserProductViewModel
    {
        public Comment comment { get; set; }
        public Client client { get; set; }
        public Product product { get; set; }
    }
}
