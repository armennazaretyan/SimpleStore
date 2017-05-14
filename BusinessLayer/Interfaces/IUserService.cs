using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        bool ValidateUser(string name, string password);
        UserModel Get(string name, string password);
        UserModel Get(string name);
        bool CreateUser(string name, string password);
    }
}
