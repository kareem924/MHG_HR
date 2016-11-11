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
   public class VacationTypeService: IVacationTypes
    {
        private readonly IRepository<VacationsType> _vacationsRepository;

        public VacationTypeService(IRepository<VacationsType> vacationsRepository)
        {
            _vacationsRepository = vacationsRepository;
        }

        public IEnumerable<VacationsType> GetAllVacationsType()
        {
            return _vacationsRepository.Table;
        }
    }
}
