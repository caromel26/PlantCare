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
    public class PlantTagsController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public PlantTagsController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: api/PlantTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantTag>>> GetPlantTags()
        {
            return await _context.PlantTags.ToListAsync();
        }

        // GET: api/PlantTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantTag>> GetPlantTag(int id)
        {
            var plantTag = await _context.PlantTags.FindAsync(id);

            if (plantTag == null)
            {
                return NotFound();
            }

            return plantTag;
        }

        // PUT: api/PlantTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantTag(int id, PlantTag plantTag)
        {
            if (id != plantTag.Id)
            {
                return BadRequest();
            }

            _context.Entry(plantTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantTagExists(id))
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

        // POST: api/PlantTags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlantTag>> PostPlantTag(PlantTag plantTag)
        {
            _context.PlantTags.Add(plantTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantTag", new { id = plantTag.Id }, plantTag);
        }

        // DELETE: api/PlantTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantTag(int id)
        {
            var plantTag = await _context.PlantTags.FindAsync(id);
            if (plantTag == null)
            {
                return NotFound();
            }

            _context.PlantTags.Remove(plantTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantTagExists(int id)
        {
            return _context.PlantTags.Any(e => e.Id == id);
        }
    }
}
