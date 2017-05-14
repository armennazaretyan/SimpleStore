using DataAccessLayer.EntityModel;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.DBServices
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public SimpleStoreEntities SimpleStoreContext
        {
            get { return context as SimpleStoreEntities; }
        }

        public OrderRepository(SimpleStoreEntities context)
            : base(context)
        {

        }
    }
}
