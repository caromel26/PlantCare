using PlantCare.Web.Models.Abstractions;

namespace PlantCare.Web.Models
{
    public class Image : BaseModel
    {
        public int PlantId { get; set; }
        public string? ImageUrl { get; set; }
        public Plant Plant { get; set; }
    }
}
