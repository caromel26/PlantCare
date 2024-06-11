using PlantCare.Web.Models.Abstractions;

namespace PlantCare.Web.Models
{
    public class Note : DictionaryModel
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
    }
}
