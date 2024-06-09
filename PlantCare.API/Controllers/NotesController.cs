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
    public class NotesController : ControllerBase
    {
        private readonly PlantCareContext _context;

        public NotesController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotes()
        {
            var notes = await _context.Notes.Include(n => n.Plant).ToListAsync();

            var noteDtos = notes.Select(note => new NoteDTO
            {
                Id = note.Id,
                Name = note.Name,
                Description = note.Description,
                PlantId = note.PlantId,
                Plant = new PlantDTO
                {
                    Id = note.Plant.Id,
                    Name = note.Plant.Name,
                    Description = note.Plant.Description,
                    LastWateringDate = note.Plant.LastWateringDate,
                    SpeciesId = note.Plant.SpeciesId,
                    Species = new SpeciesDTO
                    {
                        Id = note.Plant.Species.Id,
                        Name = note.Plant.Species.Name,
                        Description = note.Plant.Species.Description,
                        WateringFrequency = note.Plant.Species.WateringFrequency ?? "",
                        SunlightRequirements = note.Plant.Species.SunlightRequirements ?? "",
                    }
                }
            }).ToList();

            return Ok(noteDtos);
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDTO>> GetNote(int id)
        {
            var note = await _context.Notes.Include(n => n.Plant).FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            var noteDTO = new NoteDTO
            {
                Id = note.Id,
                Name = note.Name,
                Description = note.Description,
                PlantId = note.PlantId,
                Plant = new PlantDTO
                {
                    Id = note.Plant.Id,
                    Name = note.Plant.Name,
                    Description = note.Plant.Description,
                    LastWateringDate = note.Plant.LastWateringDate,
                    SpeciesId = note.Plant.SpeciesId,
                    Species = new SpeciesDTO
                    {
                        Id = note.Plant.Species.Id,
                        Name = note.Plant.Species.Name,
                        Description = note.Plant.Species.Description,
                        WateringFrequency = note.Plant.Species.WateringFrequency ?? "",
                        SunlightRequirements = note.Plant.Species.SunlightRequirements ?? "",
                    }
                }
            };

            return Ok(noteDTO);
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, NoteDTO noteDTO)
        {
            if (id != noteDTO.Id)
            {
                return BadRequest();
            }

            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            note.Name = noteDTO.Name;
            note.Description = noteDTO.Description;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NoteDTO>> PostNote(NoteDTO noteDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = new Note
            {
                Name = noteDTO.Name,
                Description = noteDTO.Description
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, noteDTO);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            note.IsActive = false;
            note.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
