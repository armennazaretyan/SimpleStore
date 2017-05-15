using BusinessLayer.Interfaces;
using SimpleStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SimpleStoreWeb.Controllers
{
    [Authorize]
    public class ProductDetailController : BaseController
    {
        private ICategoryService categoryService;
        private IProductService productService;

        public ProductDetailController(ICategoryService _categoryService, IProductService _productService)
        {
            categoryService = _categoryService;
            productService = _productService;
        }

        // GET: ProductDetail
        public ActionResult Index(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel();

            var product = productService.GetDetails(id);
            productViewModel.ProductName = product.Name;
            productViewModel.Price = product.Price;
            productViewModel.ImagePath = product.ImagePath;
            productViewModel.CategoryName = categoryService.GetByID(product.CategoryID).Name;

            return View(productViewModel);
        }
    }
}