using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouseWork.Context;
using CouseWork.Models;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Data;
using Microsoft.VisualBasic.ApplicationServices;
using System.Security.Cryptography;
using CouseWork.Data;
using CouseWork.Views;
using System.Collections.ObjectModel;


namespace CouseWork.Data.Repositories
{
    public class UserRepository : BaseRepository<Users>
    {
        ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public Users GetByID(int id)
        {
            return _context.Users.Find(id);
        }
        public ObservableCollection<Users> GetUsers()
        {
            var users = _context.Users
                .ToList();

            return new ObservableCollection<Users>(users);
        }
        public Users? UpdateUser(int id, string login, string fname, string sname, string email, string phone)
        {
            var userEntity = GetByID(id);

            if (userEntity != null)
            {
                userEntity.Login = login;
                userEntity.FirstName = fname;
                userEntity.LastName = sname;
                userEntity.Email = email;
                userEntity.Phone = phone;

                _context.SaveChanges();
                return userEntity;
            }

            return null;
        }
        public bool CheckLoginExistsExceptCurrent(string login, int currentID)
        {
            var loginExists = _context.Users.Any(u => u.Login == login && u.UserID != currentID)
                          || _context.Organizers.Any(o => o.CompanyName == login)
                          || _context.Managers.Any(m => m.Login == login);
            return loginExists;
        }
        public bool CheckEmailExistsExceptCurrent(string email, int currentID)
        {
            var emailExists = _context.Users.Any(u => u.Email == email && u.UserID != currentID)
                              || _context.Organizers.Any(o => o.Email == email)
                              || _context.Managers.Any(m => m.Email == email);

            return emailExists;
        }
        public bool CheckPhoneExistsExceptCurrent(string phone, int currentID)
        {
            var phoneExists = _context.Users.Any(u => u.Phone == phone && u.UserID != currentID)
                              || _context.Organizers.Any(o => o.Phone == phone)
                              || _context.Managers.Any(m => m.Phone == phone);
            return phoneExists;
        }
    }
}
