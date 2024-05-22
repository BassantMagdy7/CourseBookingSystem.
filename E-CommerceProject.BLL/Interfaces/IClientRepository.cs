using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBookingSystem.BLL.Interfaces
{
    public interface IClientRepository<T>
    {
        void Add(T entity);
    }
}
