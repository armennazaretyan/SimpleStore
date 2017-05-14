using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace BusinessLayer.InfoServices
{
    public class ProductService : BaseService, IProductService
    {
        public IEnumerable<ProductModel> GetByCategory(int categoryId)
        {
            List<ProductModel> lProductModel = new List<ProductModel>();

            var dbProducts = (categoryId == 0) ? 
                _UnitOfWork.Products.GetAll()
                :
                _UnitOfWork.Products.GetByCategory(categoryId);

            foreach (var item in dbProducts)
            {
                lProductModel.Add(new ProductModel() { ID = item.ID, CategoryID = item.CategoryID, Name = item.Name, Price = item.Price, ImagePath = item.ImagePath });
            }

            return lProductModel;
        }

        public ProductModel GetDetails(int id)
        {
            var productDBModel = _UnitOfWork.Products.GetDetails(id);
            ProductModel productModel = new ProductModel()
            {
                ID = productDBModel.ID,
                CategoryID = productDBModel.CategoryID,
                ImagePath = productDBModel.ImagePath,
                Name = productDBModel.Name,
                Price = productDBModel.Price
            };

            return productModel;
        }
    }
}
