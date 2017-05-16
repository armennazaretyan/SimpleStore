using BusinessLayer.Interfaces;
using SimpleStoreWeb.Models;
using SimpleStoreWeb.WebClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleStoreWeb.Controllers
{
    [Authorize]
    public class StoreController : BaseController
    {
        private Ambrella ambrella;


        private ICategoryService categoryService;
        private IProductService productService;
        private IOrderService orderService;
        private IUserService userService;

        public StoreController(IUserService _userService, ICategoryService _categoryService, IProductService _productService, IOrderService _orderService)
        {
            categoryService = _categoryService;
            productService = _productService;
            orderService = _orderService;
            userService = _userService;

            ambrella = new Ambrella(userService, categoryService, productService, orderService);
        }

        // GET: Store
        public ActionResult Index()
        {
            //StoreViewModel storeViewModel = LoadPageData(0);

            //return View(storeViewModel);
            return View(ambrella.LoadStorePageData(0));
        }

        public ActionResult Categorty(int id)
        {
            //StoreViewModel storeViewModel = LoadPageData(id);

            //return View("Index", storeViewModel);
            return View("Index", ambrella.LoadStorePageData(id));
        }

        public ActionResult IncreaseProductCount(int id)
        {
            //int userID = userService.Get(CurrentUserLoginName).ID;
            //orderService.ChangeCountInShoppingCard(productService.GetDetails(id), userID, isIncrease: true);
            ambrella.IncreaseProductCount(id);
            return RedirectToAction("Index", "ShoppingCard");
        }

        public ActionResult DecreaseProductCount(int id)
        {
            //int userID = userService.Get(CurrentUserLoginName).ID;
            //bool isSucceed = orderService.ChangeCountInShoppingCard(productService.GetDetails(id), userID, isIncrease: false);
            bool isSucceed = ambrella.DecreaseProductCount(id);
            if (isSucceed)
            {
                return RedirectToAction("Index", "ShoppingCard");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //private StoreViewModel LoadPageData(int categoryID)
        //{
        //    StoreViewModel storeViewModel = new StoreViewModel();
        //    storeViewModel.Products = productService.GetByCategory(categoryID);
        //    storeViewModel.Categories = categoryService.GetAll();

        //    return storeViewModel;
        //}
    }
}