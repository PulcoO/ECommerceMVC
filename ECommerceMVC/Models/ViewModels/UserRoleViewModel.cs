using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set;}
        //public int RolesId { get; set; } // le mieux reste de le stocker en viewbag
        public bool IsSelected { get; set; }
    }
}
