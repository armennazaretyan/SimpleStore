using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using Utilities.Extensions;

namespace BusinessLayer.InfoServices
{
    public class UserService : BaseService, IUserService
    {
        public bool ValidateUser(string name, string password)
        {
            var user = _UnitOfWork.Users.Get(name, password.GetMD5());
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public UserModel Get(string name)
        {
            var user = _UnitOfWork.Users.Get(name);

            UserModel userModel = new UserModel()
            {
                ID = user.ID,
                LoginName = user.LoginName
            };

            return userModel;
        }

        public UserModel Get(string name, string password)
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(string name, string password)
        {
            bool retVal = false;
            try
            {
                _UnitOfWork.Users.Insert(new DataAccessLayer.EntityModel.User() { LoginName = name, Password = password.GetMD5() });

                _UnitOfWork.Complete();

                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }
    }
}
