using CouseWork.Context;
using CouseWork.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CouseWork.Data.Repositories
{
    public class BaseRepository<T> where T : class
    {
        private ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context) => _context = context;

        public bool CheckLoginExists(string login)
        {
            var loginExists = _context.Users.Any(u => u.Login == login)
                  || _context.Organizers.Any(o => o.CompanyName == login)
                  || _context.Managers.Any(m => m.Login == login);
            return loginExists;
        }
        public bool CheckEmailExists(string email)
        {
            var emailExists = _context.Users.Any(u => u.Email == email)
                              || _context.Organizers.Any(o => o.Email == email)
                              || _context.Managers.Any(m => m.Email == email);

            return emailExists;
        }
        public bool CheckPhoneExists(string phone)
        {
            var phoneExists = _context.Users.Any(u => u.Phone == phone)
                              || _context.Organizers.Any(o => o.Phone == phone)
                              || _context.Managers.Any(m => m.Phone == phone);
            return phoneExists;
        }
        public int CheckNextId()
        {
            var maxUserId = _context.Users.Any() ? _context.Users.Max(u => u.UserID) : 0;
            var maxOrganizerId = _context.Organizers.Any() ? _context.Organizers.Max(o => o.OrganizerID) : 0;
            var maxManagerId = _context.Managers.Any() ? _context.Managers.Max(m => m.ManagerID) : 0;
            var maxId = Math.Max(maxUserId, Math.Max(maxOrganizerId, maxManagerId));
            return maxId + 1;
        }
        public int CheckNextQuestionId()
        {
            var maxUserQuestionId = _context.UserQuestions.Max(e => (int?)e.QuestionID) ?? 1;
            var maxOrganizerQuestionId = _context.OrganizerQuestions.Max(e => (int?)e.QuestionID) ?? 1;
            var maxId = Math.Max(maxUserQuestionId, maxOrganizerQuestionId);
            return maxId + 1;
        }
        public int GetManagerWithLeastQuestions()
        {
            var managerQuestions = _context.Managers
                .Select(manager => new
                {
                    ManagerID = manager.ManagerID,
                    UserQuestionsCount = _context.UserQuestions.Count(uq => uq.ManagerID == manager.ManagerID),
                    OrganizerQuestionsCount = _context.OrganizerQuestions.Count(oq => oq.ManagerID == manager.ManagerID)
                })
                .Select(managerWithCounts => new
                {
                    ManagerID = managerWithCounts.ManagerID,
                    TotalQuestions = managerWithCounts.UserQuestionsCount + managerWithCounts.OrganizerQuestionsCount
                })
                .OrderBy(m => m.TotalQuestions)
                .FirstOrDefault();

            return managerQuestions?.ManagerID ?? 0;
        }


        public int? FindRoleId(int userId)
        {
            var userRoleId = _context.Users
                .Where(u => u.UserID == userId)
                .Select(u => (int?)u.RoleID)
                .FirstOrDefault();

            if (userRoleId != null)
            {
                return userRoleId;
            }

            var managerRoleId = _context.Managers
                .Where(m => m.ManagerID == userId)
                .Select(m => (int?)m.RoleID)
                .FirstOrDefault();
            if (managerRoleId != null)
            {
                return managerRoleId;
            }

            var organizerRoleId = _context.Organizers
                .Where(o => o.OrganizerID == userId)
                .Select(o => (int?)o.RoleID)
                .FirstOrDefault();
            if (organizerRoleId != null)
            {
                return organizerRoleId;
            }

            return null;
        }


        public bool AuthorizeUser(string username, string password)
        {
            try
            {
                string hashedPassword = HashPasswordWithSHA256(password);
                int? personId = null;
                string storedHashedPassword = null;

                var user = _context.Users.FirstOrDefault(u => u.Login == username);
                if (user != null)
                {

                    personId = user.UserID;
                    storedHashedPassword = user.Password;
                }

                if (personId == null)
                {
                    var manager = _context.Managers.FirstOrDefault(m => m.Login == username);
                    if (manager != null)
                    {
                        personId = manager.ManagerID;
                        storedHashedPassword = manager.Password;
                    }
                }

                if (personId == null)
                {
                    var organizer = _context.Organizers.FirstOrDefault(o => o.CompanyName == username);
                    if (organizer != null)
                    {
                        personId = organizer.OrganizerID;
                        storedHashedPassword = organizer.Password;
                    }
                }

                if (personId != null && storedHashedPassword == hashedPassword)
                {
                    UserSession.CurrentUserID = personId.Value;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{(string)Application.Current.Resources["item34"]}: {ex.Message}");
                return false;
            }
        }



        public bool AddNewUser(string login, string password, string fname, string sname, string email, string phone, bool organizerReg)
        {
            try
            {
                string hashedPassword = HashPasswordWithSHA256(password);

                if (!organizerReg)
                {
                    
                    var newUser = new Users
                    {
                        UserID = CheckNextId(),
                        Login = login,
                        Password = hashedPassword,
                        FirstName = fname,
                        LastName = sname,
                        Email = email,
                        Phone = phone,
                        RoleID = 1
                    };

                    _context.Users.Add(newUser);
                    
                    /*
                    var newManager = new Managers
                    {
                        ManagerID = CheckNextId(),
                        Login = login,
                        Password = hashedPassword,
                        FirstName = fname,
                        LastName = sname,
                        Email = email,
                        Phone = phone,
                        RoleID = 2,
                        DateOfEmployment = DateTime.Today
                    };

                    _context.Managers.Add(newManager);
                    */

                }
                else
                {
                    var newOrganizer = new Organizers
                    {
                        OrganizerID = CheckNextId(),
                        CompanyName = login,
                        Password = hashedPassword,
                        FirstName = fname,
                        LastName = sname,
                        Email = email,
                        Phone = phone,
                        RoleID = 3
                    };

                    _context.Organizers.Add(newOrganizer);

                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
                return false;
            }
        }
        public string HashPasswordWithSHA256(string password) // хеширование пароля
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
