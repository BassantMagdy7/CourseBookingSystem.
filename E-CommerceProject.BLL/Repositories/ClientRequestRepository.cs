using CourseBookingSystem.BLL.Interfaces;
using CourseBookingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBookingSystem.BLL.Repositories
{
    public class ClientRequestRepository:IClientRequestRepository
    {
        private readonly EserviceContext _context;

        public ClientRequestRepository(EserviceContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(ClientRequest clientRequest)
        {
            await _context.ClientRequests.AddAsync(clientRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClientRequest>> GetAllAsync()
        {
            return await _context.ClientRequests.ToListAsync();
        }

        public async Task<ClientRequest> GetByIdAsync(int id)
        {
            return await _context.ClientRequests.FindAsync(id);
        }

        public async Task<ClientRequest> GetByIdUpdateAsync(int id)
        {
            var clientRequest = await _context.ClientRequests
                .Include(cr => cr.Client)
                .FirstOrDefaultAsync(cr => cr.Id == id);

            if (clientRequest != null)
            {
                await _context.Entry(clientRequest)
                    .Reference(cr => cr.Client)
                    .LoadAsync();
            }

            return clientRequest;
        }

        public async Task UpdateAsync(ClientRequest clientRequest)
        {
            _context.Entry(clientRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

 
        public IEnumerable<Client> GetFiltered(Func<Client, bool> filter)
        {
            return _context.Clients.Where(filter);
        }

        public async Task<List<ClientRequest>> GetByClientIdAsync(int clientId)
        {
            // Query the database to retrieve client requests
            var clientRequests = await _context.ClientRequests
                .Include(cr => cr.Client)
                .Include(cr => cr.Service)
                .Where(cr => cr.ClientId == clientId)
                .ToListAsync();

            // Return the list of client requests
            return clientRequests;
        }
        public async Task LoadClientAsync(ClientRequest clientRequest)
        {
            await _context.Entry(clientRequest)
                .Reference(cr => cr.Client)
                .LoadAsync();
        }

        public async Task LoadServiceAsync(ClientRequest clientRequest)
        {
            await _context.Entry(clientRequest)
                .Reference(cr => cr.Service)
                .LoadAsync();
        }
       
        public async Task RemoveAsync(ClientRequest clientRequest)
        { 
            _context.Set<ClientRequest>().Remove(clientRequest);
            await _context.SaveChangesAsync();
        }
        
    }
}

