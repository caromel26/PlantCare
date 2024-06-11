using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantCare.API.DTO;
using PlantCare.API.Models;
using PlantCare.API.Models.Context;

namespace PlantCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public SpeciesController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: api/Species
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeciesDTO>>> GetSpecies()
        {
            var species = await _context.Species.Where(x => x.IsActive == true).ToListAsync();

            var speciesDTOs = species.Select(s => new SpeciesDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                WateringFrequency = s.WateringFrequency??"",
                SunlightRequirements = s.SunlightRequirements??"",
            }).ToList();

            return Ok(speciesDTOs);
        }

        // GET: api/Species/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpeciesDTO>> GetSpecies(int id)
        {
            var species = await _context.Species.FindAsync(id);

            if (species == null)
            {
                return NotFound();
            }

            var speciesDTO = new SpeciesDTO
            {
                Id = species.Id,
                Name = species.Name,
                Description = species.Description,
                WateringFrequency = species.WateringFrequency ?? "",
                SunlightRequirements = species.SunlightRequirements ?? "",
            };

            return Ok(speciesDTO);
        }

        // PUT: api/Species/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecies(int id, SpeciesDTO speciesDTO)
        {
            if (id != speciesDTO.Id)
            {
                return BadRequest();
            }

            var species = await _context.Species.FindAsync(id);
            if (species == null)
            {
                return NotFound();
            }

            species.Name = speciesDTO.Name;
            species.Description = speciesDTO.Description;
            species.WateringFrequency = speciesDTO.WateringFrequency;
            species.SunlightRequirements = speciesDTO.SunlightRequirements;

            _context.Entry(species).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeciesExists(id))
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

        // POST: api/Species
        [HttpPost]
        public async Task<ActionResult<SpeciesDTO>> PostSpecies(SpeciesDTO speciesDTO)
        {
            var species = new Species
            {
                Name = speciesDTO.Name,
                Description = speciesDTO.Description,
                WateringFrequency = speciesDTO.WateringFrequency,
                SunlightRequirements = speciesDTO.SunlightRequirements,
                // Add other properties as needed
            };

            _context.Species.Add(species);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecies", new { id = species.Id }, speciesDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecies(int id)
        {
            var species = await _context.Species.FindAsync(id);
            if (species == null)
            {
                return NotFound();
            }

            species.IsActive = false;
            species.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpeciesExists(int id)
        {
            return _context.Species.Any(e => e.Id == id);
        }
    }
}
