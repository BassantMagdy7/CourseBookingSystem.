using CourseBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBookingSystem.BLL.Interfaces
{
    public interface IClientRequestRepository
    {
        Task AddAsync(ClientRequest clientRequest);
        Task<IEnumerable<ClientRequest>> GetAllAsync();
        Task<ClientRequest> GetByIdAsync(int id);
        Task UpdateAsync(ClientRequest clientRequest);
        Task<ClientRequest> GetByIdUpdateAsync(int id);
        Task<List<ClientRequest>> GetByClientIdAsync(int clientId);
        IEnumerable<Client> GetFiltered(Func<Client, bool> filter);
        Task LoadClientAsync(ClientRequest clientRequest);
        Task LoadServiceAsync(ClientRequest clientRequest);
        Task RemoveAsync(ClientRequest clientRequest);
    }
}
