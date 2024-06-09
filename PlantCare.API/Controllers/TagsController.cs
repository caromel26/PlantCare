using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantCare.API.DTO;
using PlantCare.API.Models;
using PlantCare.API.Models.Context;

namespace PlantCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public TagsController(PlantCareContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetTag(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            var tagDto = new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description,
            };

            return Ok(tagDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTags()
        {
            var tags = await _context.Tags.ToListAsync();
            var tagDtos = tags.Select(tag => new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description,
            }).ToList();

            return Ok(tagDtos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTag([FromBody] TagDTO tagDto)
        {
            if (tagDto == null)
            {
                return BadRequest("Tag entity is null.");
            }

            var entity = new Tag
            {
                Name = tagDto.Name,
                Description = tagDto.Description,
            };

            _context.Tags.Add(entity);
            await _context.SaveChangesAsync();

            return Ok("Tag created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTag(int id, [FromBody] TagDTO tagDto)
        {
            if (id == 0 || tagDto == null)
            {
                return BadRequest();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            tag.Name = tagDto.Name;
            tag.Description = tagDto.Description;

            await _context.SaveChangesAsync();
            return Ok("Tag updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            tag.IsActive = false;
            tag.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
