using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
    public class AgendaService : IAgendaService
    {
        private readonly IRepository<Agenda> _agendaRepository;

        public AgendaService(IRepository<Agenda> agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }
        public void DeleteAgenda(Agenda agenda)
        {
            _agendaRepository.Delete(agenda);
        }

        public Agenda GetAgendaById(int agendaId)
        {
            return _agendaRepository.GetById(agendaId);
        }

        public void InsertAgenda(Agenda agenda)
        {
            if (agenda == null)
                throw new ArgumentNullException("borrow");
            _agendaRepository.Insert(agenda);
        }

        public void UpdateAgenda(Agenda agenda)
        {
            if (agenda == null)
                throw new ArgumentNullException("borrow");
            _agendaRepository.Update(agenda);
        }


        public IEnumerable<Agenda> GetAllAgendas()
        {
            return _agendaRepository.Table;
        }

        public IEnumerable<Agenda> GetAllAgendaByUserId(int userId)
        {
            return _agendaRepository.Table.Where(x => x.UserId == userId);
        }
    }
}
