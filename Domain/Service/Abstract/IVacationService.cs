using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Service.Abstract
{
  public  interface IVacationService
    {
        void DeleteVacation(Vacation vacation);
        Vacation GetVacationById(int taskId);
        void InsertVacation(Vacation vacation);
        void UpdateVacation(Vacation vacation);
        IEnumerable<Vacation> GetAllVacations();
        IEnumerable<Vacation> GetAllVacationByUserId(int userId, byte typeId);
        

    }
}
