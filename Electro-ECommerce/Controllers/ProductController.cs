using Electro_ECommerce.Models;
using Electro_ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Electro_ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }
        public IActionResult Index()
        {
            var products = _productRepository.GetAll().ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                _productRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productRepository.Update(product);
                    _productRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Delete(product);
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _productRepository.Find(e => e.ProductId == id).Any();
        }
    }
}