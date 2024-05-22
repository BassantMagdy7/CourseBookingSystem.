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
    public class UserRepository : IUserRepository
    {
        private readonly EserviceContext _context;

        public UserRepository(EserviceContext Context)
        {
            _context = Context;

        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int userId)
        {
            var userToDelete = await _context.Users.FindAsync(userId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string UserName, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName && u.UserPassword == password);
               
                
        }

    }
}
