using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
namespace ConsultMe.Data.Repository
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public bool AddUser(User _user);
        public bool UpdateUser(User _user);
        public bool DeleteUser(int _id);
    }
}
