using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
   public class VacationService:IVacationService
    {
        private readonly IRepository<Vacation> _vacationRepository;

        public VacationService(IRepository<Vacation> vacationRepository)
        {
            _vacationRepository = vacationRepository;
        } 
        public void DeleteVacation(Vacation vacation)
        {
            _vacationRepository.Delete(vacation);
        }

        public Vacation GetVacationById(int taskId)
        {
            return _vacationRepository.GetById(taskId);
        }

        public void InsertVacation(Vacation vacation)
        {
            if (vacation == null)
                throw new ArgumentNullException("vacation");
            _vacationRepository.Insert(vacation);
        }

        public void UpdateVacation(Vacation vacation)
        {
            if (vacation == null)
                throw new ArgumentNullException("vacation");
            _vacationRepository.Update(vacation);
        }

        public IEnumerable<Vacation> GetAllVacations()
        {
            return _vacationRepository.Table;
        }

        public IEnumerable<Vacation> GetAllVacationByUserId(int userId, byte typeId)
        {
            return _vacationRepository.Table.Where(x => x.EmployeeId == userId && x.TypeId==typeId);
        }
    }
}
