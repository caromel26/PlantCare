using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantCare.API.DTO;
using PlantCare.API.Models;
using PlantCare.API.Models.Context;

namespace PlantCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantTasksController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public PlantTasksController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: api/PlantTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantTaskDTO>>> GetPlantTasks()
        {
            var plantTasks = await _context.PlantTasks
                .Include(x => x.TaskType)
                .Include(x => x.Plant)
                .ThenInclude(x => x.Species)
                .ToListAsync();

            var plantTaskDTOs = plantTasks.Select(plantTask => new PlantTaskDTO
            {
                Id = plantTask.Id,
                TaskTypeId = plantTask.TaskTypeId,
                PlantId = plantTask.PlantId,
                CompletionStatus = plantTask.CompletionStatus,
                DueDate = plantTask.DueDate,
                TaskType = new TaskTypeDTO
                {
                    Id = plantTask.TaskType.Id,
                    Name = plantTask.TaskType.Name,
                    Description = plantTask.TaskType.Description
                },

                Plant = new PlantDTO
                {
                    Id = plantTask.Plant.Id,
                    Name = plantTask.Plant.Name,
                    Description = plantTask.Plant.Description,
                    LastWateringDate = plantTask.Plant.LastWateringDate,
                    SpeciesId = plantTask.Plant.SpeciesId,
                    Species = new SpeciesDTO
                    {
                        Id = plantTask.Plant.Species.Id,
                        Name = plantTask.Plant.Species.Name,
                        Description = plantTask.Plant.Species.Description,
                        WateringFrequency = plantTask.Plant.Species.WateringFrequency ?? "",
                        SunlightRequirements = plantTask.Plant.Species.SunlightRequirements ?? "",
                    }
                }
            }).ToList();

            return Ok(plantTaskDTOs);
        }

        // GET: api/PlantTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantTaskDTO>> GetPlantTask(int id)
        {
            var plantTask = await _context.PlantTasks
                .Include(x => x.TaskType)
                .Include(x => x.Plant)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (plantTask == null)
            {
                return NotFound();
            }

            var plantTaskDTO = new PlantTaskDTO
            {
                Id = plantTask.Id,
                TaskTypeId = plantTask.TaskTypeId,
                PlantId = plantTask.PlantId,
                CompletionStatus = plantTask.CompletionStatus,
                DueDate = plantTask.DueDate,
            };

            return Ok(plantTaskDTO);
        }

        // PUT: api/PlantTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantTask(int id, PlantTaskDTO plantTaskDTO)
        {
            if (id != plantTaskDTO.Id)
            {
                return BadRequest();
            }

            var plantTask = await _context.PlantTasks
                .Include(x => x.TaskType)
                .Include(x => x.Plant)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (plantTask == null)
            {
                return NotFound();
            }
            
            plantTask.DueDate = plantTaskDTO.DueDate;
            plantTask.CompletionStatus = plantTaskDTO.CompletionStatus;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/PlantTasks
        [HttpPost]
        public async Task<ActionResult<PlantTaskDTO>> PostPlantTag(PlantTaskDTO plantTaskDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plantTask = new PlantTask
            {
                TaskTypeId = plantTaskDTO.TaskTypeId,
                PlantId = plantTaskDTO.PlantId,
                DueDate = plantTaskDTO.DueDate,
                CompletionStatus = plantTaskDTO.CompletionStatus,
            };

            _context.PlantTasks.Add(plantTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantTag", new { id = plantTask.Id }, plantTaskDTO);
        }

        // DELETE: api/PlantTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantTask(int id)
        {
            var plantTask = await _context.PlantTasks.FindAsync(id);
            if (plantTask == null)
            {
                return NotFound();
            }

            plantTask.IsActive = false;
            plantTask.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
