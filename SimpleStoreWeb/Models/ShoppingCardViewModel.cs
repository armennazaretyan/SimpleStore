using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleStoreWeb.Models
{
    public class ShoppingCardViewModel
    {
        public IEnumerable<ShoppingCardInfoViewModel> ShoppingCardProducts { get; set; }

        public decimal TotalPrice { get; set; }

        public ShoppingCardViewModel()
        {
            ShoppingCardProducts = new List<ShoppingCardInfoViewModel>();
        }
    }

    public class ShoppingCardInfoViewModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}