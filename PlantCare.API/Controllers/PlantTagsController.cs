using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantCare.API.DTO;
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
        public async Task<ActionResult<IEnumerable<PlantTagDTO>>> GetPlantTags()
        {
            var plantTags = await _context.PlantTags.Where(x => x.IsActive == true)
                .Include(x => x.Tag)
                .Include(x => x.Plant)
                .ThenInclude(x => x.Species)
                .ToListAsync();

            var plantTagDTOs = plantTags.Select(plantTag => new PlantTagDTO
            {
                Id = plantTag.Id,
                TagId = plantTag.TagId,
                PlantId = plantTag.PlantId,
                Plant = new PlantDTO
                {
                    Id = plantTag.Plant.Id,
                    Name = plantTag.Plant.Name,
                    Description = plantTag.Plant.Description,
                    LastWateringDate = plantTag.Plant.LastWateringDate,
                    SpeciesId = plantTag.Plant.SpeciesId,
                    Species = new SpeciesDTO
                    {
                        Id = plantTag.Plant.Species.Id,
                        Name = plantTag.Plant.Species.Name,
                        Description = plantTag.Plant.Species.Description,
                        WateringFrequency = plantTag.Plant.Species.WateringFrequency ?? "",
                        SunlightRequirements = plantTag.Plant.Species.SunlightRequirements ?? "",
                    }
                },
                Tag = new TagDTO
                {
                    Id = plantTag.Tag.Id,
                    Name = plantTag.Tag.Name,
                    Description = plantTag.Tag.Description
                }
            }).ToList();

            return Ok(plantTagDTOs);
        }

        // GET: api/PlantTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantTagDTO>> GetPlantTag(int id)
        {
            var plantTag = await _context.PlantTags
                .Include(x => x.Tag)
                .Include(x => x.Plant)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (plantTag == null)
            {
                return NotFound();
            }

            var plantTagDTO = new PlantTagDTO
            {
                Id = plantTag.Id,
                TagId = plantTag.TagId,
                PlantId = plantTag.PlantId,
            };

            return Ok(plantTagDTO);
        }

        // PUT: api/PlantTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantTag(int id, PlantTagDTO plantTagDTO)
        {
            if (id != plantTagDTO.Id)
            {
                return BadRequest();
            }

            var plantTag = await _context.PlantTags.FindAsync(id);
            if (plantTag == null)
            {
                return NotFound();
            }

            plantTag.TagId = plantTagDTO.TagId;
            plantTag.PlantId = plantTagDTO.PlantId;
            // Update other properties as needed

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
        public async Task<ActionResult<PlantTagDTO>> PostPlantTag(PlantTagDTO plantTagDTO)
        {
            var plant = await _context.Plants.FindAsync(plantTagDTO.PlantId);
            var species = await _context.Species.FindAsync(plantTagDTO.Plant.SpeciesId);
            plant.Species = species;

            var tag = await _context.Tags.FindAsync(plantTagDTO.TagId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plantTag = new PlantTag
            {
                Id = plantTagDTO.Id,
                TagId = plantTagDTO.TagId,
                PlantId = plantTagDTO.PlantId,
                Plant = plant,
                Tag = tag,
            };

            _context.PlantTags.Add(plantTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantTag", new { id = plantTag.Id }, plantTagDTO);
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

            plantTag.IsActive = false;
            plantTag.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantTagExists(int id)
        {
            return _context.PlantTags.Any(e => e.Id == id);
        }
    }
}