using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Service.Abstract
{
  public  interface IAgendaService
    {
        void DeleteAgenda(Agenda agenda);
        Agenda GetAgendaById(int agendaId);
        void InsertAgenda(Agenda agenda);
        void UpdateAgenda(Agenda agenda);
        IEnumerable<Agenda> GetAllAgendas();
        IEnumerable<Agenda> GetAllAgendaByUserId(string userId);
    }
}
