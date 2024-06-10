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
    public class RemindersController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public RemindersController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: api/Reminders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReminderDTO>>> GetReminders()
        {
            var reminders = await _context.Reminders.Include(x => x.Plant).ThenInclude(x => x.Species).ToListAsync();

            var remindersDTOs = reminders.Select(reminder => new ReminderDTO
            {
                Id = reminder.Id,
                Name = reminder.Name,
                Description = reminder.Description,
                PlantId = reminder.PlantId,
                Plant = new PlantDTO
                {
                    Id = reminder.Plant.Id,
                    Name = reminder.Plant.Name,
                    Description = reminder.Plant.Description,
                    LastWateringDate = reminder.Plant.LastWateringDate,
                    SpeciesId = reminder.Plant.SpeciesId,
                    Species = new SpeciesDTO
                    {
                        Id = reminder.Plant.Species.Id,
                        Name = reminder.Plant.Species.Name,
                        Description = reminder.Plant.Species.Description,
                        WateringFrequency = reminder.Plant.Species.WateringFrequency ?? "",
                        SunlightRequirements = reminder.Plant.Species.SunlightRequirements ?? "",
                    }
                }
            }).ToList();

            return Ok(remindersDTOs);
        }

        // GET: api/Reminders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderDTO>> GetReminder(int id)
        {
            var reminder = await _context.Reminders.Include(n => n.Plant).FirstOrDefaultAsync(x => x.Id == id);

            if (reminder == null)
            {
                return NotFound();
            }

            var reminderDTO = new ReminderDTO
            {
                Id = reminder.Id,
                Name = reminder.Name,
                Description = reminder.Description,
                PlantId = reminder.PlantId,
                Plant = new PlantDTO
                {
                    Id = reminder.Plant.Id,
                    Name = reminder.Plant.Name,
                    Description = reminder.Plant.Description,
                    LastWateringDate = reminder.Plant.LastWateringDate,
                    SpeciesId = reminder.Plant.SpeciesId,
                    Species = new SpeciesDTO
                    {
                        Id = reminder.Plant.Species.Id,
                        Name = reminder.Plant.Species.Name,
                        Description = reminder.Plant.Species.Description,
                        WateringFrequency = reminder.Plant.Species.WateringFrequency ?? "",
                        SunlightRequirements = reminder.Plant.Species.SunlightRequirements ?? "",
                    }
                }
            };
            return Ok(reminderDTO);
        }

        // PUT: api/Reminders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminder(int id, ReminderDTO reminderDTO)
        {
            if (id != reminderDTO.Id)
            {
                return BadRequest();
            }

            var reminder = await _context.Reminders.FindAsync(id);

            if (reminder == null)
            {
                return NotFound();
            }

            reminder.Name = reminderDTO.Name;
            reminder.Description = reminderDTO.Description;
            reminder.PlantId = reminderDTO.PlantId;

            await _context.SaveChangesAsync();

            return Ok(reminderDTO);
        }

        // POST: api/Reminders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reminder>> PostReminder(ReminderDTO reminderDTO)
        {
            var plant = await _context.Plants.FindAsync(reminderDTO.PlantId);
            var species = await _context.Species.FindAsync(reminderDTO.Plant.SpeciesId);
            plant.Species = species;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reminder = new Reminder
            {
                Name = reminderDTO.Name,
                Description = reminderDTO.Description,
                PlantId = reminderDTO.PlantId,
                Plant = plant
            };

            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReminder", new { id = reminder.Id }, reminderDTO);

        }

        // DELETE: api/Reminders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }

            reminder.IsActive = false;
            reminder.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReminderExists(int id)
        {
            return _context.Reminders.Any(e => e.Id == id);
        }
    }
}
