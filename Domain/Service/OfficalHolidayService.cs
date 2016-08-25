using Domain.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Service
{
  public  class OfficalHolidayService : IOfficalHolidaysService
    {
        private readonly IRepository<OfficalHolidays> _officalHolidayRepository;
        public OfficalHolidayService(IRepository<OfficalHolidays> officalHolidayRepository)
        {
            _officalHolidayRepository = officalHolidayRepository;
        }
        public void DeleteOfficalHoliday(OfficalHolidays officalHoliday)
        {
            _officalHolidayRepository.Delete(officalHoliday);
        }

        public IEnumerable<OfficalHolidays> GetAllOfficalHolidays()
        {
            return _officalHolidayRepository.Table;
        }

        public void InsertOfficalHoliday(OfficalHolidays officalHoliday)
        {
            if (officalHoliday == null)
                throw new ArgumentNullException("officalHoliday");
            _officalHolidayRepository.Insert(officalHoliday);
        }

        public void UpdateOfficalHoliday(OfficalHolidays officalHoliday)
        {
            if (officalHoliday == null)
                throw new ArgumentNullException("officalHoliday");
            _officalHolidayRepository.Update(officalHoliday);
        }
    }
}
