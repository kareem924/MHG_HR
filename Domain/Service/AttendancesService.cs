using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
   public class AttendancesService : IAttendancesService
    {
        private readonly IRepository<Attendance> _attendanceRepository;

        public AttendancesService(IRepository<Attendance> attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }
        public void InsertAttendance(Attendance attendance)
        {
            if (attendance == null)
                throw new ArgumentNullException("attendance");
            _attendanceRepository.Insert(attendance);
        }

        public IEnumerable<Attendance> GetAllAttendances()
        {
            return _attendanceRepository.Table;
        }
    }
}
