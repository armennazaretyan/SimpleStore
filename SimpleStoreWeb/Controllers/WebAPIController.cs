using BusinessLayer.Interfaces;
using SimpleStoreWeb.Models;
using SimpleStoreWeb.WebClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace SimpleStoreWeb.Controllers
{
    [Authorize]
    public class WebAPIController : ApiController
    {
        private Ambrella ambrella;

        //public string CurrentUserLoginName
        //{
        //    get
        //    {
        //        string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
        //        HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName]; //Get the cookie by it's name
        //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
        //        return ticket.Name; //You have the UserName!
        //    }
        //}

        private ICategoryService categoryService;
        private IProductService productService;
        private IOrderService orderService;
        private IUserService userService;

        public WebAPIController(IUserService _userService, ICategoryService _categoryService, IProductService _productService, IOrderService _orderService)
        {
            categoryService = _categoryService;
            productService = _productService;
            orderService = _orderService;
            userService = _userService;

            ambrella = new Ambrella(userService, categoryService, productService, orderService);
        }




        /// <summary>
        /// CONTROLLER: UserController
        /// </summary>

        [AllowAnonymous]
        public string PostLogin(LoginViewModel model)
        {
            //if (userService.ValidateUser(model.UserName, model.Password))
            //{
            //    FormsAuthentication.SetAuthCookie(model.UserName, true);
            //    return "success";
            //}
            //else
            //{
            //    return "Invalid username or password.";
            //}

            return ambrella.Login(model);
        }

        public bool GetLogout()
        {
            //FormsAuthentication.SignOut();
            //return true;
            return ambrella.Logout();
        }

        [AllowAnonymous]
        public string PostRegister(RegisterViewModel model)
        {
            //var membershipUser = userService.CreateUser(model.UserName, model.Password);

            //if (membershipUser)
            //{
            //    FormsAuthentication.SetAuthCookie(model.UserName, true);
            //    return "success";
            //}
            //else
            //{
            //    return "User not created.";
            //}
            return ambrella.Register(model);
        }




        /// <summary>
        /// CONTROLLER: StoreController
        /// </summary>

        // api/WebAPI/GetIncreaseProductCount?id=5
        public bool PostIncreaseProductCount(int id)
        {
            //int userID = userService.Get(CurrentUserLoginName).ID;
            //return orderService.ChangeCountInShoppingCard(productService.GetDetails(id), userID, isIncrease: true);
            return ambrella.IncreaseProductCount(id);
        }

        // api/WebAPI/GetDecreaseProductCount?id=5
        public bool PostDecreaseProductCount(int id)
        {
            //int userID = userService.Get(CurrentUserLoginName).ID;
            //return orderService.ChangeCountInShoppingCard(productService.GetDetails(id), userID, isIncrease: false);
            return ambrella.DecreaseProductCount(id);
        }






        /// <summary>
        /// CONTROLLER: ShoppingCardController
        /// </summary>

        public ShoppingCardViewModel GetShoppingCardData()
        {
            //int userID = userService.Get(CurrentUserLoginName).ID;

            //ShoppingCardViewModel shoppingCardViewModel = new ShoppingCardViewModel();
            //shoppingCardViewModel.TotalPrice = 0;

            //List<ShoppingCardInfoViewModel> list = new List<ShoppingCardInfoViewModel>();
            //var shoppingCardModelList = orderService.GetUserShoppingCard(userID);
            //foreach (var item in shoppingCardModelList)
            //{
            //    var product = productService.GetDetails(item.ProductID);
            //    list.Add(new ShoppingCardInfoViewModel()
            //    {
            //        ID = item.ID,
            //        ProductID = product.ID,
            //        ProductName = product.Name,
            //        Price = product.Price,
            //        Quantity = item.Quantity
            //    });

            //    shoppingCardViewModel.TotalPrice += product.Price * item.Quantity;
            //}

            //shoppingCardViewModel.ShoppingCardProducts = list;

            //return shoppingCardViewModel;
            return ambrella.GetShoppingCardData();
        }

        public bool PostRemoveProduct(int id)
        {
            //int userID = userService.Get(CurrentUserLoginName).ID;
            //return orderService.RemoveProductFromShoppingCard(userID, productID: id);
            return ambrella.RemoveProduct(id);
        }






        /// <summary>
        /// CONTROLLER: ProductDetailController
        /// </summary>

        public ProductViewModel GetProductDetail(int id)
        {
            //ProductViewModel productViewModel = new ProductViewModel();

            //var product = productService.GetDetails(id);
            //productViewModel.ProductName = product.Name;
            //productViewModel.Price = product.Price;
            //productViewModel.ImagePath = product.ImagePath;
            //productViewModel.CategoryName = categoryService.GetByID(product.CategoryID).Name;

            //return productViewModel;
            return ambrella.GetProductDetail(id);
        }





        /// <summary>
        /// CONTROLLER: PaymentController
        /// </summary>

        public bool PostSubmitPayment(PaymentViewModel model)
        {
            //int userID = userService.Get(CurrentUserLoginName).ID;

            //return orderService.Add(new BusinessLayer.Models.OrderModel()
            //{
            //    FirstName = model.FirstName,
            //    LastName = model.LastName,
            //    Address = model.Address,
            //    City = model.City,
            //    State = model.State,
            //    PostalCode = model.PostalCode,
            //    Country = model.Country,
            //    Phone = model.Phone,
            //    Email = model.Email
            //}, userID);
            return ambrella.SubmitPayment(model);
        }

    }
}
