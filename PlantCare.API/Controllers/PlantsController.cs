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
    public class PlantsController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public PlantsController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: api/Plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDTO>>> GetPlants()
        {
            var plants = await _context.Plants.Where(x => x.IsActive).ToListAsync();

            var speciesIds = plants.Select(p => p.SpeciesId).Distinct().ToList();
            var speciesDictionary = await _context.Species
                                                .Where(s => speciesIds.Contains(s.Id))
                                                .ToDictionaryAsync(s => s.Id);

            var plantDTOs = plants.Select(p => new PlantDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                LastWateringDate = p.LastWateringDate,
                SpeciesId = p.SpeciesId,
                Species = new SpeciesDTO
                {
                    Id = p.Species.Id,
                    Name = p.Species.Name,
                    Description = p.Species.Description,
                    WateringFrequency = p.Species.WateringFrequency ?? "",
                    SunlightRequirements = p.Species.SunlightRequirements ?? "",
                }
            }).ToList();

            return Ok(plantDTOs);
        }


        // GET: api/Plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantDTO>> GetPlant(int id)
        {
            var plant = await _context.Plants
                    .Include(x => x.Species).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (plant == null)
            {
                return NotFound();
            }

            var plantDTO = new PlantDTO()
            {
                Id = plant.Id,
                Name = plant.Name,
                Description = plant.Description,
                LastWateringDate = plant.LastWateringDate,
                SpeciesId = plant.SpeciesId,
                Species = new SpeciesDTO
                {
                    Id = plant.Species.Id,
                    Name = plant.Species.Name,
                    Description = plant.Species.Description,
                    WateringFrequency = plant.Species.WateringFrequency ?? "",
                    SunlightRequirements = plant.Species.SunlightRequirements ?? ""
                }
            };

            return Ok(plantDTO);
        }

        // PUT: api/Plants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlant(int id, PlantDTO plantDTO)
        {
            if (id != plantDTO.Id)
            {
                return BadRequest();
            }

            var plant = await _context.Plants.Where(x => x.Id == id).Include(x => x.Species).FirstOrDefaultAsync();
            if (plant == null)
            {
                return NotFound();
            }

            plant.Name = plantDTO.Name;
            plant.Description = plantDTO.Description;
            plant.LastWateringDate = plantDTO.LastWateringDate;
            plant.SpeciesId = plantDTO.SpeciesId;
            plant.Species = _context.Species.Find(plantDTO.SpeciesId);
            // Update other properties as needed

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
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

        // POST: api/Plants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlantDTO>> PostPlant(PlantDTO plantDTO)
        {
            var species = await _context.Species.FindAsync(plantDTO.SpeciesId);
            if (species == null)
            {
                return BadRequest("Invalid SpeciesId.");
            }

            var plant = new Plant
            {
                Name = plantDTO.Name,
                SpeciesId = plantDTO.SpeciesId,
                Description = plantDTO.Description,
                LastWateringDate = plantDTO.LastWateringDate,
                Species = species
            };

            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlant", new { id = plant.Id }, plantDTO);
        }



        // DELETE: api/Plants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var plant = await _context.Plants.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (plant == null)
            {
                return NotFound();
            }

            var reminders = await _context.Reminders.Where(r => r.PlantId == id).ToListAsync();
            _context.Reminders.RemoveRange(reminders);

            plant.IsActive = false;
            plant.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool PlantExists(int id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}
