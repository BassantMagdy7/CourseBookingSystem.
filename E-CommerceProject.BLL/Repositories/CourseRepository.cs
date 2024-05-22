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
    public class CourseRepository:ICourseRepository
    {
        private readonly EserviceContext _context;

        public CourseRepository(EserviceContext context)
        {
            _context = context;
        }
        //Get All Courses
        public async Task<List<Service>> GetAllServices()
        {
          return await _context.Services.ToListAsync();
        }
        //Get Courses By Id
        public async Task<Service> GetCourseById(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(c => c.ServiceId == id);
        }

        public async Task<IEnumerable<ServiceDetail>> GetCourseDetailsById(int id)
        {
            var courseDetails = await _context.ServiceDetails.Include(sd => sd.Service).Where(sd => sd.ServiceId == id).ToListAsync();
            return courseDetails;
        }

        public async Task<(Service service, IEnumerable<ServiceDetail> serviceDetails)> GetDetailsById(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(c => c.ServiceId == id);
            if (service == null)
                return (null, null);
            var serviceDetails = await _context.ServiceDetails.Where(sd => sd.ServiceId == id).ToListAsync();
            return (service, serviceDetails);
        }

    }
}
