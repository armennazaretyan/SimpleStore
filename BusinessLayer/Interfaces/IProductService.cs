using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IProductService
    {
        ProductModel GetDetails(int id);
        IEnumerable<ProductModel> GetByCategory(int categoryId);
    }
}
