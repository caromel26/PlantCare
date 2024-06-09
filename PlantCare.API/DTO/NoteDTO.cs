using PlantCare.API.DTO.Abstractions;

namespace PlantCare.API.DTO
{
    public class NoteDTO : DictionaryDTO
    {
        public int PlantId { get; set; }
        public PlantDTO Plant { get; set; }
    }
}
