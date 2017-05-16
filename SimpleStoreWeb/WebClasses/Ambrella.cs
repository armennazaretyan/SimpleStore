using BusinessLayer.Interfaces;
using SimpleStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SimpleStoreWeb.WebClasses
{
    public class Ambrella
    {
        public string CurrentUserLoginName
        {
            get
            {
                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName]; //Get the cookie by it's name
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                return ticket.Name; //You have the UserName!
            }
        }


        private ICategoryService categoryService;
        private IProductService productService;
        private IOrderService orderService;
        private IUserService userService;

        public Ambrella(IUserService _userService, ICategoryService _categoryService, IProductService _productService, IOrderService _orderService)
        {
            categoryService = _categoryService;
            productService = _productService;
            orderService = _orderService;
            userService = _userService;
        }

        public string Login(LoginViewModel model)
        {
            if (userService.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return "success";
            }
            else
            {
                return "Invalid username or password.";
            }
        }

        public bool Logout()
        {
            FormsAuthentication.SignOut();
            return true;
        }

        public string Register(RegisterViewModel model)
        {
            var membershipUser = userService.CreateUser(model.UserName, model.Password);

            if (membershipUser)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return "success";
            }
            else
            {
                return "User not created.";
            }
        }



        /// <summary>
        /// CONTROLLER: StoreController
        /// </summary>

        public StoreViewModel LoadStorePageData(int categoryID)
        {
            StoreViewModel storeViewModel = new StoreViewModel();
            storeViewModel.Products = productService.GetByCategory(categoryID);
            storeViewModel.Categories = categoryService.GetAll();

            return storeViewModel;
        }

        public bool IncreaseProductCount(int id)
        {
            int userID = userService.Get(CurrentUserLoginName).ID;
            return orderService.ChangeCountInShoppingCard(productService.GetDetails(id), userID, isIncrease: true);
        }

        public bool DecreaseProductCount(int id)
        {
            int userID = userService.Get(CurrentUserLoginName).ID;
            return orderService.ChangeCountInShoppingCard(productService.GetDetails(id), userID, isIncrease: false);
        }






        /// <summary>
        /// CONTROLLER: ShoppingCardController
        /// </summary>

        public ShoppingCardViewModel GetShoppingCardData()
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

            return shoppingCardViewModel;
        }

        public bool RemoveProduct(int id)
        {
            int userID = userService.Get(CurrentUserLoginName).ID;
            return orderService.RemoveProductFromShoppingCard(userID, productID: id);
        }





        /// <summary>
        /// CONTROLLER: ProductDetailController
        /// </summary>

        public ProductViewModel GetProductDetail(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel();

            var product = productService.GetDetails(id);
            productViewModel.ProductName = product.Name;
            productViewModel.Price = product.Price;
            productViewModel.ImagePath = product.ImagePath;
            productViewModel.CategoryName = categoryService.GetByID(product.CategoryID).Name;

            return productViewModel;
        }





        /// <summary>
        /// CONTROLLER: PaymentController
        /// </summary>

        public bool SubmitPayment(PaymentViewModel model)
        {
            int userID = userService.Get(CurrentUserLoginName).ID;

            return orderService.Add(new BusinessLayer.Models.OrderModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                State = model.State,
                PostalCode = model.PostalCode,
                Country = model.Country,
                Phone = model.Phone,
                Email = model.Email
            }, userID);
        }



    }
}
