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
    public class ImagesController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public ImagesController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetImages()
        {
            var images = await _context.Images.Include(x => x.Plant).ToListAsync(); // _context.Images.ToListAsync();

            var imageDtos = images.Select(image => new ImageDTO
            {
                Id = image.Id,
                PlantId = image.PlantId,
                ImageUrl = image.ImageUrl??"",
                Plant = new PlantDTO
                {
                    Id = image.Plant.Id,
                    Name = image.Plant.Name,
                    Description = image.Plant.Description,
                    LastWateringDate = image.Plant.LastWateringDate,
                    SpeciesId = image.Plant.SpeciesId,
                    Species = new SpeciesDTO
                    {
                        Id = image.Plant.Species.Id,
                        Name = image.Plant.Species.Name,
                        Description = image.Plant.Species.Description,
                        WateringFrequency = image.Plant.Species.WateringFrequency ?? "",
                        SunlightRequirements = image.Plant.Species.SunlightRequirements ?? "",
                    }
                }
            }).ToList();

            return Ok(imageDtos);
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDTO>> GetImage(int id)
        {
            var image = await _context.Images.Include(x => x.Plant).FirstOrDefaultAsync(x => x.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            var imageDTO = new ImageDTO
            {
                Id = image.Id,
                PlantId = image.PlantId,
                ImageUrl = image.ImageUrl ?? "",
                Plant = new PlantDTO
                {
                    Id = image.Plant.Id,
                    Name = image.Plant.Name,
                    Description = image.Plant.Description,
                    LastWateringDate = image.Plant.LastWateringDate,
                    SpeciesId = image.Plant.SpeciesId,
                    Species = new SpeciesDTO
                    {
                        Id = image.Plant.Species.Id,
                        Name = image.Plant.Species.Name,
                        Description = image.Plant.Species.Description,
                        WateringFrequency = image.Plant.Species.WateringFrequency ?? "",
                        SunlightRequirements = image.Plant.Species.SunlightRequirements ?? "",
                    }
                }
            };

            return Ok(imageDTO);
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, ImageDTO imageDTO)
        {
            if (id != imageDTO.Id)
            {
                return BadRequest();
            }

            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            image.ImageUrl = imageDTO.ImageUrl;

            await _context.SaveChangesAsync();
            return Ok(imageDTO);
        }

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageDTO>> PostImage(ImageDTO imageDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = new Image
            {
                PlantId = imageDTO.PlantId,
                ImageUrl = imageDTO.ImageUrl
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImage", new { id = image.Id }, imageDTO);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            image.IsActive = false;
            image.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
