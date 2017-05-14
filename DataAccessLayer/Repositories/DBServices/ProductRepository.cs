using DataAccessLayer.EntityModel;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.DBServices
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public SimpleStoreEntities SimpleStoreContext
        {
            get { return context as SimpleStoreEntities; }
        }

        public ProductRepository(SimpleStoreEntities context)
            : base(context)
        {

        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            return SimpleStoreContext.Products.Where(w => w.CategoryID == categoryId).ToList();
        }

        public Product GetDetails(int id)
        {
            return SimpleStoreContext.Products.Where(w => w.ID == id).SingleOrDefault();
        }
    }
}
