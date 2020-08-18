using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceMVC.Data;
using ECommerceMVC.Models.Orders;
using ECommerceMVC.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ECommerceMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public IActionResult Index()
        {
            var ListToDeserialize = HttpContext.Session.GetString("ListCards");
            List<ProductCart> ListDeserialize = JsonConvert.DeserializeObject<List<ProductCart>>(ListToDeserialize);
            //ViewBag.ProductCarts = ListDeserialize;

            return View(ListDeserialize);
        }

        // Get: Cart/Add
        public IActionResult Add(Product product)
        {

            //initialisation de la quantity a modifier par le parametre plus tard
            int quantity = 1;

            //si jamais la listCart n'existe pas ... créons la
            if (HttpContext.Session.GetString("ListCards") == null )
            {
                List<ProductCart> productCarts = new List<ProductCart>();
                var list = JsonConvert.SerializeObject(productCarts);
                HttpContext.Session.SetString("ListCards", list);
            }
            //mapping du product en ProductCart
            ProductCart productCart = new ProductCart();
            productCart.Product = product;
            productCart.ProductId = product.Id;
            productCart.Quantity = quantity;

            //extraction de la list
            var ListToDeserialize = HttpContext.Session.GetString("ListCards");
            List<ProductCart> ListDeserialize = JsonConvert.DeserializeObject<List<ProductCart>>(ListToDeserialize);
            bool isfind = false;

            //passer la list en revue et vérifier si trouvé, dans ce cas, ajouter une quantity
            foreach(ProductCart productcartDeserialize in ListDeserialize)
            {
                if (productCart == productcartDeserialize)
                {
                    isfind = true;
                    productCart.Quantity = productCart.Quantity + quantity;
                }
            }

            //si non trouvé
            if (isfind == false)
            {
                ListDeserialize.Add(productCart);
            }
            //resérialisation de la List
            var ListToSerialize = JsonConvert.SerializeObject(ListDeserialize);
            HttpContext.Session.SetString("ListCards", ListToSerialize);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
