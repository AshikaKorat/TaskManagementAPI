using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Core.Models;
using TaskManagementAPI.Data.Repositories;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }
        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return Ok(await _repository.GetAllTasksAsync());
        }
        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _repository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(TB_Task task)
        {
            await _repository.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TB_Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateTaskAsync(task);

            return NoContent();
        }
        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _repository.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
