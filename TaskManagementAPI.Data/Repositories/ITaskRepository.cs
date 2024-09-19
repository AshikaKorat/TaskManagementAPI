using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.Core.Models;

namespace TaskManagementAPI.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TB_Task>> GetAllTasksAsync(
            string sortBy = "Id",
            string filter = null,
            int pageNumber = 1,
            int pageSize = 10);
        Task<TB_Task> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TB_Task task);
        Task UpdateTaskAsync(TB_Task task);
        Task DeleteTaskAsync(int id);
    }
}
