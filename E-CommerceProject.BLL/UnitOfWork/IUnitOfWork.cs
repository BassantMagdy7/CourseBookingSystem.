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
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        Task<int> SaveAsync();

        IClientRepository<Client> ClientRepository { get; }

        IClientRequestRepository ClientRequests { get; }

    }
}
