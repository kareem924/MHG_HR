using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Service.Abstract
{
  public  interface IDependencyAgendaService
    {
        void DeleteDependencyAgenda(DependencyAgenda dependencyagenda);
        void InsertDependencyAgenda(DependencyAgenda dependencyagenda);
        void UpdateDependencyAgenda(DependencyAgenda dependencyagenda);
        IEnumerable<DependencyAgenda> GetAllDependencyAgendas();

    }
}
