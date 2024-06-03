using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<PlantTask>>> GetPlantTasks()
        {
            return await _context.PlantTasks.ToListAsync();
        }

        // GET: api/PlantTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantTask>> GetPlantTask(int id)
        {
            var plantTask = await _context.PlantTasks.FindAsync(id);

            if (plantTask == null)
            {
                return NotFound();
            }

            return plantTask;
        }

        // PUT: api/PlantTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantTask(int id, PlantTask plantTask)
        {
            if (id != plantTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(plantTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PlantTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlantTask>> PostPlantTask(PlantTask plantTask)
        {
            _context.PlantTasks.Add(plantTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantTask", new { id = plantTask.Id }, plantTask);
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

            _context.PlantTasks.Remove(plantTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantTaskExists(int id)
        {
            return _context.PlantTasks.Any(e => e.Id == id);
        }
    }
}
