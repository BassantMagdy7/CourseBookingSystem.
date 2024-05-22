using CourseBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBookingSystem.BLL.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Service>> GetAllServices();
        Task<Service> GetCourseById(int id);
        Task<IEnumerable<ServiceDetail>> GetCourseDetailsById(int id);
        Task<(Service service, IEnumerable<ServiceDetail> serviceDetails)> GetDetailsById(int id);

    }
}
