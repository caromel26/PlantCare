using PlantCare.API.DTO.Abstractions;

namespace PlantCare.API.DTO
{
    public class PlantTagDTO : BaseDTO
    {
        public int PlantId { get; set; }
        public int TagId { get; set; }
        public PlantDTO Plant { get; set; }
        public TagDTO Tag { get; set; }
    }
}
