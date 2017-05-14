using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICategoryService
    {
        CategoryModel GetByID(int id);
        IEnumerable<CategoryModel> GetAll();
    }
}
