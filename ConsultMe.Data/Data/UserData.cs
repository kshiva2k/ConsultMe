using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;

namespace ConsultMe.Data.Data
{
    public class UserData : IUserRepository
    {
        consultmeContext context { get; }
        public UserData(consultmeContext _context)
        {
            context = _context;
        }
        public List<User> GetUsers()
        {
            var users = context.User
                     .Where(_ => _.Status == 1)
                     .ToList();
            return users;
        }
        public bool AddUser(User _user)
        {
            var user = new User
            {
                Usertypeid = _user.Usertypeid,
                Displayname = _user.Displayname,
                Password = _user.Password,
                Email = _user.Email,
                Mobilenumber = _user.Mobilenumber,
                Status = 1
            };
            context.User.Add(user);
            context.SaveChanges();
            return true;
        }
        public bool UpdateUser(User _user)
        {
            var user = context.User.Where(_ => _.Id.Equals(_user.Id)).FirstOrDefault();
            user.Displayname = _user.Displayname;
            user.Password = _user.Password;
            user.Email = _user.Email;
            user.Mobilenumber = _user.Mobilenumber;
            context.SaveChanges();
            return true;
        }
        public bool DeleteUser(int _id)
        {
            var user = context.User.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            user.Status = 0;
            context.SaveChanges();
            return true;
        }
    }
}
