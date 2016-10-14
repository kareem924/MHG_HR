using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Service.Abstract
{
    public interface ITasksService
    {
        void DeleteTask(Tasks task);
        Tasks GetTaskById(int taskId);
        void Inserttask(Tasks task);
        void Updatetask(Tasks task);
        IEnumerable<Tasks> GetAlltasks();
        IEnumerable<Tasks> GetAlltaskByUserId(string userId);
    }
}
