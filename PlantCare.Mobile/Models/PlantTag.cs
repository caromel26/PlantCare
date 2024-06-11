using PlantCare.Mobile.Models.Abstractions;

namespace PlantCare.Mobile.Models
{
    public class PlantTag : BaseModel
    {
        public int PlantId { get; set; }
        public int TagId { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
