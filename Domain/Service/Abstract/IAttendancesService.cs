using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Service.Abstract
{
    public interface IAttendancesService
    {
        void InsertAttendance(Attendance attendance);
        IEnumerable<Attendance> GetAllAttendances();
    }
}
