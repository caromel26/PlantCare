using PlantCare.API.DTO.Abstractions;

namespace PlantCare.API.DTO
{
    public class PlantDTO : DictionaryDTO
    {
        public int SpeciesId { get; set; }
        public DateTime? LastWateringDate { get; set; }
        public SpeciesDTO Species { get; set; }

        public ICollection<PlantTaskDTO> PlantTasks { get; set; }
        public ICollection<PlantTagDTO> PlantTags { get; set; }
    }
}