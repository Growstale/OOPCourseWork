using CouseWork.Context;
using CouseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CouseWork.Data.Repositories
{
    public class OrganizerRepository : BaseRepository<Organizers>
    {
        ApplicationDbContext _context;
        public OrganizerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public Organizers? FindById(int id)
        {
            return _context.Organizers
                .FirstOrDefault(e => e.OrganizerID == id);
        }

        public ObservableCollection<Organizers> GetOrganizers()
        {
            var organizers = _context.Organizers
                .ToList();

            return new ObservableCollection<Organizers>(organizers);
        }
        public Organizers? UpdateOrganizer(int id, string login, string fname, string sname, string email, string phone)
        {
            var organizerEntity = FindById(id);

            if (organizerEntity != null)
            {
                organizerEntity.CompanyName = login;
                organizerEntity.FirstName = fname;
                organizerEntity.LastName = sname;
                organizerEntity.Email = email;
                organizerEntity.Phone = phone;

                _context.SaveChanges();
                return organizerEntity;
            }

            return null;
        }
        public bool CheckLoginExistsExceptCurrent(string login, int currentID)
        {
            var loginExists = _context.Users.Any(u => u.Login == login)
                          || _context.Organizers.Any(o => o.CompanyName == login && o.OrganizerID != currentID)
                          || _context.Managers.Any(m => m.Login == login);
            return loginExists;
        }
        public bool CheckEmailExistsExceptCurrent(string email, int currentID)
        {
            var emailExists = _context.Users.Any(u => u.Email == email)
                              || _context.Organizers.Any(o => o.Email == email && o.OrganizerID != currentID)
                              || _context.Managers.Any(m => m.Email == email);

            return emailExists;
        }
        public bool CheckPhoneExistsExceptCurrent(string phone, int currentID)
        {
            var phoneExists = _context.Users.Any(u => u.Phone == phone)
                              || _context.Organizers.Any(o => o.Phone == phone && o.OrganizerID != currentID)
                              || _context.Managers.Any(m => m.Phone == phone);
            return phoneExists;
        }

    }
}
