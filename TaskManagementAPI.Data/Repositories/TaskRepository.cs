using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.Core.Data;
using TaskManagementAPI.Core.Models;

namespace TaskManagementAPI.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly TaskContext _context;

        public TaskRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TB_Task>> GetAllTasksAsync(string sortBy = "Id", string filter = null, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<TB_Task> query = _context.Tasks;

            // Filtering
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(t => t.Name.Contains(filter) || t.Description.Contains(filter));
            }

            // Sorting
            query = sortBy.ToLower() switch
            {
                "name" => query.OrderBy(t => t.Name),
                "duedate" => query.OrderBy(t => t.DueDate),
                "iscompleted" => query.OrderBy(t => t.IsCompleted),
                _ => query.OrderBy(t => t.Id),
            };

            // Pagination
            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return tasks;
        }

        public async Task<TB_Task> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddTaskAsync(TB_Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TB_Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
