using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantCare.API.DTO;
using PlantCare.API.Models;
using PlantCare.API.Models.Context;

namespace PlantCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypesController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public TaskTypesController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: api/TaskTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTypeDTO>>> GetTaskTypes()
        {
            var taskTypes = await _context.TaskTypes.Where(x => x.IsActive == true).ToListAsync();
            var taskTypesDtos = taskTypes.Select(taskType => new TaskTypeDTO
            {
                Id = taskType.Id,
                Name = taskType.Name,
                Description = taskType.Description,
            }).ToList();

            return Ok(taskTypesDtos);
        }

        // GET: api/TaskTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskType>> GetTaskType(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var taskType = await _context.TaskTypes.FindAsync(id);

            if (taskType == null)
            {
                return NotFound();
            }

            var taskTypeDto = new TaskTypeDTO
            {
                Id = taskType.Id,
                Name = taskType.Name,
                Description = taskType.Description,
            };

            return taskType;
        }

        // PUT: api/TaskTypes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTaskType(int id, [FromBody] TaskTypeDTO taskTypeDto)
        {
            if (id == 0 || taskTypeDto == null)
            {
                return BadRequest();
            }

            var taskType = await _context.TaskTypes.FindAsync(id);
            if (taskType == null)
            {
                return NotFound();
            }

            taskType.Name = taskTypeDto.Name;
            taskType.Description = taskTypeDto.Description;

            await _context.SaveChangesAsync();
            return Ok("Task updated successfully");
        }

        // POST: api/TaskTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateTaskType([FromBody] TaskTypeDTO taskTypeDto)
        {
            if (taskTypeDto == null)
            {
                return BadRequest("Task entity is null.");
            }

            var entity = new TaskType
            {
                Name = taskTypeDto.Name,
                Description = taskTypeDto.Description,
            };

            _context.TaskTypes.Add(entity);
            await _context.SaveChangesAsync();

            return Ok("Task created successfully.");
        }

        // DELETE: api/TaskTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskType(int id)
        {
            var taskType = await _context.TaskTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (taskType == null)
            {
                return NotFound();
            }

            taskType.IsActive = false;
            taskType.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
