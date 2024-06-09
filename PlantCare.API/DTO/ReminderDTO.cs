using PlantCare.API.DTO.Abstractions;

namespace PlantCare.API.DTO
{
    public class ReminderDTO : DictionaryDTO
    {
        public int PlantId { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
        public PlantDTO Plant { get; set; }
    }
}
