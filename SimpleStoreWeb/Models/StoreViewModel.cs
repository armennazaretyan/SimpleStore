using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleStoreWeb.Models
{
    public class StoreViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}