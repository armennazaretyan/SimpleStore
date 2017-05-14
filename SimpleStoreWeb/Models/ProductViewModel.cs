using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleStoreWeb.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}