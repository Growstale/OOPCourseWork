using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouseWork.Context;
using Microsoft.Extensions.Options;
using CouseWork;
using CouseWork.Data.Repositories;
using System.Configuration;

namespace CouseWork.Data
{
    public class UnitOfWork
    {
        private ApplicationDbContext _context;
        private UserRepository userRepository;
        private EventRepository eventRepository;
        private EventScheduleRepository eventScheduleRepository;
        private LocationRepository locationRepository;
        private CategoryRepository categoryRepository;
        private SectorRowRepository sectorRowRepository;
        private CommentsRepository commentsRepository;
        private OrganizerRepository organizerRepository;
        private ShoppingCartRepository shoppingCartRepository;
        private TicketsRepository ticketsRepository;
        private SalesRepository salesRepository;
        private TicketRefundRepository ticketRefundRepository;
        private OrganizerQuestionRepository organizerQuestionRepository;
        private UserQuestionRepository userQuestionRepository;

        private bool disposed = false;

        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null) userRepository = new UserRepository(_context); return userRepository;
            }
        }
        public EventRepository EventRepository
        {
            get
            {
                if (eventRepository == null) eventRepository = new EventRepository(_context); return eventRepository;
            }
        }
        public EventScheduleRepository EventScheduleRepository
        {
            get
            {
                if (eventScheduleRepository == null) eventScheduleRepository = new EventScheduleRepository(_context); return eventScheduleRepository;
            }
        }

        public LocationRepository LocationRepository
        {
            get
            {
                if (locationRepository == null) locationRepository = new LocationRepository(_context); return locationRepository;
            }
        }
        public CategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null) categoryRepository = new CategoryRepository(_context); return categoryRepository;
            }
        }
        public SectorRowRepository SectorRowRepository
        {
            get
            {
                if (sectorRowRepository == null) sectorRowRepository = new SectorRowRepository(_context); return sectorRowRepository;
            }
        }
        public CommentsRepository CommentsRepository
        {
            get
            {
                if (commentsRepository == null) commentsRepository = new CommentsRepository(_context); return commentsRepository;
            }
        }
        public OrganizerRepository OrganizerRepository
        {
            get
            {
                if (organizerRepository == null) organizerRepository = new OrganizerRepository(_context); return organizerRepository;
            }
        }
        public ShoppingCartRepository ShoppingCartRepository
        {
            get
            {
                if (shoppingCartRepository == null) shoppingCartRepository = new ShoppingCartRepository(_context); return shoppingCartRepository;
            }
        }
        public TicketsRepository TicketsRepository
        {
            get
            {
                if (ticketsRepository == null) ticketsRepository = new TicketsRepository(_context); return ticketsRepository;
            }
        }
        public SalesRepository SalesRepository
        {
            get
            {
                if (salesRepository == null) salesRepository = new SalesRepository(_context); return salesRepository;
            }
        }
        public TicketRefundRepository TicketRefundRepository
        {
            get
            {
                if (ticketRefundRepository == null) ticketRefundRepository = new TicketRefundRepository(_context); return ticketRefundRepository;
            }
        }
        public OrganizerQuestionRepository OrganizerQuestionRepository
        {
            get
            {
                if (organizerQuestionRepository == null) organizerQuestionRepository = new OrganizerQuestionRepository(_context); return organizerQuestionRepository;
            }
        }
        public UserQuestionRepository UserQuestionRepository
        {
            get
            {
                if (userQuestionRepository == null) userQuestionRepository = new UserQuestionRepository(_context); return userQuestionRepository;
            }
        }

        public UnitOfWork()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TicketSalesDb"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            _context = new ApplicationDbContext(optionsBuilder.Options);

        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}