using PlantCare.API.DTO.Abstractions;

namespace PlantCare.API.DTO
{
    public class PlantTaskDTO : BaseDTO
    {
        public int PlantId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime DueDate { get; set; }
        public bool CompletionStatus { get; set; }
        public PlantDTO Plant { get; set; }
        public TaskTypeDTO TaskType { get; set; }
    }
}
