using BusinessLayer.Interfaces;
using SimpleStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleStoreWeb.Controllers
{
    [Authorize]
    public class ShoppingCardController : BaseController
    {
        private IProductService productService;
        private IOrderService orderService;
        private IUserService userService;

        public ShoppingCardController(IUserService _userService, IProductService _productService, IOrderService _orderService)
        {
            productService = _productService;
            orderService = _orderService;
            userService = _userService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            int userID = userService.Get(CurrentUserLoginName).ID;
            ShoppingCardViewModel shoppingCardViewModel = new ShoppingCardViewModel();
            shoppingCardViewModel.TotalPrice = 0;

            List<ShoppingCardInfoViewModel> list = new List<ShoppingCardInfoViewModel>();
            var shoppingCardModelList = orderService.GetUserShoppingCard(userID);
            foreach (var item in shoppingCardModelList)
            {
                var product = productService.GetDetails(item.ProductID);
                list.Add(new ShoppingCardInfoViewModel()
                {
                    ID = item.ID,
                    ProductID = product.ID,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = item.Quantity
                });

                shoppingCardViewModel.TotalPrice += product.Price * item.Quantity;
            }

            shoppingCardViewModel.ShoppingCardProducts = list;

            return View(shoppingCardViewModel);
        }

        public ActionResult RemoveProduct(int id)
        {
            int userID = userService.Get(CurrentUserLoginName).ID;
            orderService.RemoveProductFromShoppingCard(userID, productID: id);

            return RedirectToAction("Index");
        }
    }
}