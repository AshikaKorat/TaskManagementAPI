using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Core.Models;
using TaskManagementAPI.Data.Repositories;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TasksController(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks(string sortBy = "Id", string filter = null, int pageNumber = 1, int pageSize = 10)
        {
            var tasks = await _repository.GetAllTasksAsync(sortBy, filter, pageNumber, pageSize);
            var taskModels = _mapper.Map<IEnumerable<TaskModel>>(tasks);

            return Ok(taskModels);
        }
        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTask(int id)
        {
            var task = await _repository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var taskModel = _mapper.Map<TaskModel>(task);
            return Ok(taskModel);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskModel>> PostTask(TaskModel taskModel)
        {
            var task = _mapper.Map<TB_Task>(taskModel);
            await _repository.AddTaskAsync(task);
            taskModel.Id = task.Id;

            return CreatedAtAction(nameof(GetTask), new { id = taskModel.Id }, taskModel);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskModel taskModel)
        {
            if (id != taskModel.Id)
            {
                return BadRequest();
            }

            var existingTask = await _repository.GetTaskByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            _mapper.Map(taskModel, existingTask);

            await _repository.UpdateTaskAsync(existingTask);

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var existingTask = await _repository.GetTaskByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            await _repository.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
