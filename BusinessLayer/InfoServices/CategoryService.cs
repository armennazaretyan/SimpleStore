using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace BusinessLayer.InfoServices
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryModel GetByID(int id)
        {
            var dbCategory = _UnitOfWork.Categories.GetByID(id);

            return new CategoryModel() { ID = dbCategory.ID, Name = dbCategory.Name };
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            List<CategoryModel> lCategoryModel = new List<CategoryModel>();

            var dbCategories = _UnitOfWork.Categories.GetAll();
            foreach (var item in dbCategories)
            {
                lCategoryModel.Add(new CategoryModel() { ID = item.ID, Name = item.Name });
            }

            return lCategoryModel;

        }
    }
}
