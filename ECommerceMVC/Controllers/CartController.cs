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
using ECommerceMVC.Interfaces;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ECommerceMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        private double TotalPrice = 0;
        private double TotalWeight = 0;
        private double TotalShippingPrice = 0;
        private double FreeShippingLast = 0;
        private const double ShippingTaxe = 0.4;
        private const double FreeShipping = 100.00;


        public CartController(IProductRepository productRepository, IManufacturerRepository manufacturerRepository, ApplicationDbContext context)
        {
            _context = context;
            _productRepository = productRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        // GET: Cart
        public IActionResult Index()
        {
            var ListToDeserialize = HttpContext.Session.GetString("ListCards");
            List<ProductCart> ListDeserialize = JsonConvert.DeserializeObject<List<ProductCart>>(ListToDeserialize);
            foreach (var item in ListDeserialize)
            {
                item.Product = _context.Products.Find(item.ProductId);
            }
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
            //impossible de le populate ici ! bug de serialize

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
                if (productCart.ProductId == productcartDeserialize.ProductId)
                {
                    productcartDeserialize.Quantity = productcartDeserialize.Quantity + productCart.Quantity;
                    isfind = true;
                    break;
                }
            }

            if (!isfind)
            {
                ListDeserialize.Add(productCart);
            }
            
            
            //resérialisation de la List
            var ListToSerialize = JsonConvert.SerializeObject(ListDeserialize);
            HttpContext.Session.SetString("ListCards", ListToSerialize);

            return RedirectToAction(nameof(Index));
        }
        //AjaxRespond
        [HttpGet]
        public IActionResult Ajax_Add(Product product)
        {
            //initialisation de la quantity a modifier par le parametre plus tard
            int quantity = 1;
            //si jamais la listCart n'existe pas ... créons la
            if (HttpContext.Session.GetString("ListCards") == null)
            {
                List<ProductCart> productCarts = new List<ProductCart>();
                var list = JsonConvert.SerializeObject(productCarts);
                HttpContext.Session.SetString("ListCards", list);
            }
            
            //extraction de la list
            var ListToDeserialize = HttpContext.Session.GetString("ListCards");
            List<ProductCart> ListDeserialize = JsonConvert.DeserializeObject<List<ProductCart>>(ListToDeserialize);
            bool isfind = false;
            //passer la list en revue et vérifier si trouvé, dans ce cas, ajouter une quantity
            foreach (ProductCart productcartDeserialize in ListDeserialize)
            {
                if (product.Id == productcartDeserialize.ProductId)
                {
                    productcartDeserialize.Quantity = productcartDeserialize.Quantity + quantity;
                    isfind = true;
                    break;
                }
            }
            if (!isfind)
            {
                //mapping du product en ProductCart
                ProductCart productCart = new ProductCart();
                //impossible de le populate ici ! bug de serializz
                productCart.Product = product;
                productCart.ProductId = product.Id;
                productCart.Quantity = quantity;
                ListDeserialize.Add(productCart);
            }
            //resérialisation de la List
            var ListToSerialize = JsonConvert.SerializeObject(ListDeserialize);
            HttpContext.Session.SetString("ListCards", ListToSerialize);
            //renvois les données sur une partial view
            return RedirectToAction(nameof(OnGetPartial));
        }
        //AjaxRespond
        [HttpGet]
        public IActionResult Ajax_Remove(Product product)
        {
            //initialisation de la quantity a modifier par le parametre plus tard
            int quantity = 1;
            //si jamais la listCart n'existe pas ... créons la
            if (HttpContext.Session.GetString("ListCards") == null)
            {
                return null;
            }

            //extraction de la list
            var ListToDeserialize = HttpContext.Session.GetString("ListCards");
            List<ProductCart> ListDeserialize = JsonConvert.DeserializeObject<List<ProductCart>>(ListToDeserialize);
            bool isfind = false;
            //passer la list en revue et vérifier si trouvé, dans ce cas, ajouter une quantity
            foreach (ProductCart productcartDeserialize in ListDeserialize)
            {
                if (product.Id == productcartDeserialize.ProductId)
                {
                    if (productcartDeserialize.Quantity <= quantity)
                    {
                        ListDeserialize.Remove(productcartDeserialize);
                        isfind = true;
                        break;
                    }
                    else
                    {
                        productcartDeserialize.Quantity = productcartDeserialize.Quantity - quantity;
                        isfind = true;
                        break;
                    }
                }
            }
            if (!isfind)
            {
                return BadRequest();
            }

            //resérialisation de la List
            var ListToSerialize = JsonConvert.SerializeObject(ListDeserialize);
            HttpContext.Session.SetString("ListCards", ListToSerialize);
            //renvois les données sur une partial view
            return RedirectToAction(nameof(OnGetPartial));
        }
        [HttpGet]
        public IActionResult OnGetPartial()
        {
            if (HttpContext.Session.GetString("ListCards") == null)
            {
                List<ProductCart> productCarts = new List<ProductCart>();
                var list = JsonConvert.SerializeObject(productCarts);
                HttpContext.Session.SetString("ListCards", list);
            }
            var ListToDeserialize = HttpContext.Session.GetString("ListCards");

      
            List<ProductCart> ListDeserialize = JsonConvert.DeserializeObject<List<ProductCart>>(ListToDeserialize);
            foreach (var item in ListDeserialize)
            {
                item.Product = _context.Products.Find(item.ProductId);
                this.TotalPrice = TotalPrice + item.Product.Price * item.Quantity;
                this.TotalWeight = TotalWeight + item.Product.Weight * item.Quantity;
            }
            //calcul du total shipping :
            this.TotalShippingPrice = this.TotalWeight * ShippingTaxe;
            this.FreeShippingLast = FreeShipping - TotalPrice;
            if (this.FreeShippingLast < 0)
            {
                this.FreeShippingLast = 0;
            }
            //recupération du upsellingProduct:

            var upSellProduct = _context.Products.Find(11);
            ViewBag.FreeShipping = this.FreeShippingLast;
            ViewBag.upsellProduct = upSellProduct;
            ViewBag.totalPrice = this.TotalPrice;
            ViewBag.totalShippingPrice = this.TotalShippingPrice;



            return PartialView("_ShoppingCart", ListDeserialize);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
