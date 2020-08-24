using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceMVC.Data;
using ECommerceMVC.Models.Orders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ECommerceMVC.Models.Products;
using ECommerceMVC.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ECommerceMVC.Models.Clients;

namespace ECommerceMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<Client> _userManager;

        public OrdersController(ApplicationDbContext context, IProductRepository productRepository, UserManager<Client> userManager)
        {
            _context = context;
            _productRepository = productRepository;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.CurrentUser = userId;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdressDeliveryStreetName,AdressDeliveryPostalCode,AdressDeliveryCountry,AdressDeliveryCity,AdressDeliveryOption_1,AdressDeliveryOption_2,AdressFacturationStreetName,AdressFacturationPostalCode,AdressFacturationCountry,AdressFacturationCity,AdressFacturationOption_1,AdressFacturationOption_2,ClientId")] Order order)
        {
            order.Client = await _userManager.FindByIdAsync(order.ClientId);
            order.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                var ListProductSession = HttpContext.Session.GetString("ListCards");
                List<ProductCart> productCarts = JsonConvert.DeserializeObject<List<ProductCart>>(ListProductSession);
                //initialisation de variable de calcul
                double totalWeight = 0;
                double shippingPrice = 0;
                double totalPrice = 0;                
                //enregistrement de l'order dans la base de donnée ! 
                _context.Add(order);
                await _context.SaveChangesAsync();
                //Order a maintenant un id ! 

                //conversion du Productcard sans id en Product-orders
                //et introduction dans la base de donnée de chacun relier avec orderId
                foreach (var item in productCarts)
                {
                    //calcul
                    totalWeight = totalWeight + item.Product.Weight * item.Quantity;
                    totalPrice = totalPrice + item.Product.Price * item.Quantity;
                    //creation d'un object Product_Order & populate
                    Product_Order product_Order = new Product_Order();
                    product_Order.OrderId = order.Id;
                    product_Order.ProductId = item.Product.Id;
                    //product_Order.Product = await _productRepository.GetProduct(product_Order.ProductId);
                    product_Order.Quantity = item.Quantity;
                    _context.Add<Product_Order>(product_Order);
                    await _context.SaveChangesAsync();
                }
                //finalisation des calcule
                shippingPrice = totalWeight * 1.2;
                totalPrice = totalPrice + shippingPrice;

                //Modification de Order dans la base de donnée ! 

                order.State = "payement accepted";
                order.Total = totalPrice;
                order.Weight = totalWeight;
                order.DeliveryCost = shippingPrice;

                _context.Update(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", order.ClientId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", order.ClientId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,AdressDeliveryStreetName,AdressDeliveryPostalCode,AdressDeliveryCountry,AdressDeliveryCity,AdressDeliveryOption_1,AdressDeliveryOption_2,AdressFacturationStreetName,AdressFacturationPostalCode,AdressFacturationCountry,AdressFacturationCity,AdressFacturationOption_1,AdressFacturationOption_2,Weight,DeliveryCost,Total,State,ClientId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", order.ClientId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
