using DataAccessLayer.EntityModel;
using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.InfoServices
{
    public class BaseService
    {
        protected IUnitOfWork _UnitOfWork { get; set; }
        public BaseService()
        {
            _UnitOfWork = new UnitOfWork(new SimpleStoreEntities());
        }
    }
}
