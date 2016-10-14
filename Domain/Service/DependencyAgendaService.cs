using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Service.Abstract
{
   public class DependencyAgendaService:IDependencyAgendaService
    {
        private readonly IRepository<DependencyAgenda> _dependencyRepository;

        public DependencyAgendaService(IRepository<DependencyAgenda> dependencyRepository)
        {
            _dependencyRepository = dependencyRepository;
        }

        public void DeleteDependencyAgenda(DependencyAgenda dependencyagenda)
        {
            _dependencyRepository.Delete(dependencyagenda);
        }

        public void InsertDependencyAgenda(DependencyAgenda dependencyagenda)
        {
            if (dependencyagenda == null)
                throw new ArgumentNullException("dependencyagenda");
            _dependencyRepository.Insert(dependencyagenda);
        }

        public void UpdateDependencyAgenda(DependencyAgenda dependencyagenda)
        {
            if (dependencyagenda == null)
                throw new ArgumentNullException("dependencyagenda");
            _dependencyRepository.Update(dependencyagenda);
        }

        public IEnumerable<DependencyAgenda> GetAllDependencyAgendas()
        {
            return _dependencyRepository.Table;
        }
    }
}
