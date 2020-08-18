using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceMVC.Data;
using ECommerceMVC.Models.Products;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ECommerceMVC.Controllers
{
    public class Product_ImageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public Product_ImageController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Product_Image
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products_Images.Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Product_Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Image = await _context.Products_Images
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product_Image == null)
            {
                return NotFound();
            }

            return View(product_Image);
        }

        // GET: Product_Image/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Product_Image/Create/1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ImageFile,ProductId")] Product_Image product_Image)
        {
            if (ModelState.IsValid)
            {
                //save image to wwwroot/images
                string publicPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product_Image.ImageFile.FileName);
                string extension = Path.GetExtension(product_Image.ImageFile.FileName);
                //definir le nom de l'image
                product_Image.ImageName = fileName = product_Image.ProductId + fileName + DateTime.Now.ToString("fffssddmmyy") + extension;
                string path = Path.Combine(publicPath + "/Images/", fileName);
                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    await product_Image.ImageFile.CopyToAsync(fileStream);
                }
                //insert record
                _context.Add(product_Image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description_Long", product_Image.ProductId);
            return View(product_Image);
        }

        // GET: Product_Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Image = await _context.Products_Images.FindAsync(id);
            if (product_Image == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description_Long", product_Image.ProductId);
            return View(product_Image);
        }

        // POST: Product_Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ImageName,ProductId")] Product_Image product_Image)
        {
            if (id != product_Image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product_Image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product_ImageExists(product_Image.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description_Long", product_Image.ProductId);
            return View(product_Image);
        }

        // GET: Product_Image/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Image = await _context.Products_Images
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product_Image == null)
            {
                return NotFound();
            }

            return View(product_Image);
        }

        // POST: Product_Image/Delete/5
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product_Image = await _context.Products_Images.FindAsync(id);

            //delete image from wwwroot/images
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", product_Image.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                //delete the record if found
                System.IO.File.Delete(imagePath);
            }

            _context.Products_Images.Remove(product_Image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product_ImageExists(int id)
        {
            return _context.Products_Images.Any(e => e.Id == id);
        }
    }
}
