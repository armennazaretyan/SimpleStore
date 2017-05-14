using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleStoreWeb.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}