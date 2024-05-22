using CourseBookingSystem.BLL.Interfaces;
using CourseBookingSystem.BLL.Repositories;
using CourseBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBookingSystem.BLL.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly EserviceContext _context;

        public EserviceContext Context => _context;
        public IUserRepository Users {  get; private set; }

        public IClientRepository<Client> ClientRepository { get; }

        public IClientRequestRepository ClientRequests { get; }

      
        public UnitOfWork(EserviceContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            ClientRepository = new ClientRepository<Client>(context);
            ClientRequests = new ClientRequestRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
