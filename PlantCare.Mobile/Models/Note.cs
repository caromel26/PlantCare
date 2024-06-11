using PlantCare.Mobile.Models.Abstractions;

namespace PlantCare.Mobile.Models
{
    public class Note : DictionaryModel
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
    }
}
