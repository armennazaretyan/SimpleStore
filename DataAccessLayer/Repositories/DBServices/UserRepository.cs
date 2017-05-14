using DataAccessLayer.EntityModel;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.DBServices
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public SimpleStoreEntities SimpleStoreContext
        {
            get { return context as SimpleStoreEntities; }
        }

        public UserRepository(SimpleStoreEntities context)
            : base(context)
        {

        }

        public User Get(string name, string password)
        {
            return SimpleStoreContext.Users.FirstOrDefault(m => m.LoginName == name && m.Password == password);
        }

        public User Get(string name)
        {
            return SimpleStoreContext.Users.FirstOrDefault(m => m.LoginName == name);
        }
    }
}
