using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Abstract
{
   public interface IOfficalHolidaysService
    {
        void DeleteOfficalHoliday(OfficalHolidays officalHoliday);
        void InsertOfficalHoliday(OfficalHolidays officalHoliday);
        void UpdateOfficalHoliday(OfficalHolidays officalHoliday);
        IEnumerable<OfficalHolidays> GetAllOfficalHolidays();
    }
}
