using PlantCare.API.DTO.Abstractions;

namespace PlantCare.API.DTO
{
    public class ImageDTO : BaseDTO
    {
        public int PlantId { get; set; }
        public string? ImageUrl { get; set; }
        public PlantDTO Plant { get; set; }
    }
}
