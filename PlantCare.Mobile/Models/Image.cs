using PlantCare.Mobile.Models.Abstractions;

namespace PlantCare.Mobile.Models
{
    public class Image : BaseModel
    {
        public int PlantId { get; set; }
        public string? ImageUrl { get; set; }
        public Plant Plant { get; set; }
    }
}
