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
    public class ClientRepository<T> : IClientRepository<T> where T : class
    {
        private readonly EserviceContext _context;
        private readonly DbSet<T> _dbSet;
        public ClientRepository(EserviceContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
    }
}
