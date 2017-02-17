using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
    public class TasksService:ITasksService
    {
        private readonly IRepository<Tasks> _tasksRepository;

        public TasksService(IRepository<Tasks> tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }
        public void DeleteTask(Tasks task)
        {
            _tasksRepository.Delete(task);
        }

        public Tasks GetTaskById(int taskId)
        {
            return _tasksRepository.GetById(taskId);
        }

        public void Inserttask(Tasks task)
        {
            if (task == null)
                throw new ArgumentNullException("borrow");
            _tasksRepository.Insert(task);
        }

        public void Updatetask(Tasks task)
        {
            if (task == null)
                throw new ArgumentNullException("borrow");
            _tasksRepository.Update(task);
        }

        public IEnumerable<Tasks> GetAlltasks()
        {
            return _tasksRepository.Table;
        }

        public IEnumerable<Tasks> GetAlltaskByUserId(int userId)
        {
            return _tasksRepository.Table.Where(x => x.UserId == userId);
        }
    }
}
