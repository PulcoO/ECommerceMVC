using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceMVC.Data;
using ECommerceMVC.Models.Products;
using System.ComponentModel;
using System.Drawing;
using ECommerceMVC.Interfaces;
using ECommerceMVC.Models.ViewModels;

namespace ECommerceMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ApplicationDbContext _context;
        private readonly List<Models.Products.Color> Colors = new List<Models.Products.Color>();

        public ProductsController(IProductRepository productRepository, IManufacturerRepository manufacturerRepository, ApplicationDbContext context)
        {
            this._productRepository = productRepository;
            this._manufacturerRepository = manufacturerRepository;
            this._context = context;
        }

        // GET: Products
        public async Task<ActionResult<ICollection<Product>>> Index()
        {
            return View(await _productRepository.GetProducts());
        }
        public ActionResult ShoppingList()
        {
            return View("ShoppingList", _context.Products);
        }
        // GET: Products/Details/5
        public async Task<ActionResult<Product>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        // GET: Products/DetailsCard/5
        public async Task<ActionResult<Product>> DetailsCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct((int)id);
            if (product == null)
            {
                return NotFound();
            }
            var upSellProduct = _context.Products.Find(11);

            ViewBag.upsellProduct = upSellProduct;

            return View("DetailsCard",product);
        }

        // GET: Products/Create/
        public IActionResult Create(int? manufacturerId )
        {
            if(manufacturerId > 0)
            {
                ViewBag.Manufacturer = manufacturerId;
            }
            else
            {
                List<Manufacturer> Manufacturers = new List<Manufacturer>();
                ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            }
            ViewBag.Colors = new SelectList(Colors);
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description_Short,Description_Long,Weight,Price,PrincipalColor,ManufacturerId,Height,Width,Lenght")] Product product, string redirection)
        {
            //reusir un instancier a 0 une Icollection de productImage
            //ICollection<Product_Image> product_Images = new ICollection<Product_Image>();
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // PUT: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description_Short,Description_Long,Weight,Price,PrincipalColor,ManufacturerId,Height,Width,Lenght")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET : Products/OnGetPartialTop5
        public async Task<ActionResult<ICollection<DTOProductWithQuantity>>> OnGetPartialTop5()
        {
            IEnumerable<DAOProductIdQuantitySum> result = await _productRepository.Top5();
            // rentrer la donnée dans un tableau : deux valeurs => products et products Id;
            // construire un iEnumérable des 5 produits en même temps et aller les peuplers correctement
            // ou creer une ViewClass avec trois entrée : id product quantity ! et envoyer ca ! changer le type dans la view as well
            List<DTOProductWithQuantity> model = new List<DTOProductWithQuantity>();

            foreach (var item in result)
            {
                var product = await _productRepository.GetProduct(item.ProductId);
                DTOProductWithQuantity DTO = new DTOProductWithQuantity
                {
                    product = product,
                    quantity = item.QuantitySum
                };
                model.Add(DTO);
        
            };

            return PartialView("_Products__Top5_Charts", model);


        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
