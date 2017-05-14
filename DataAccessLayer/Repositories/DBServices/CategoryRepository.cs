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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public SimpleStoreEntities SimpleStoreContext
        {
            get { return context as SimpleStoreEntities; }
        }


        public CategoryRepository(SimpleStoreEntities context)
            : base(context)
        {

        }
    }
}
