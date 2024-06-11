using PlantCare.Web.Models.Abstractions;

namespace PlantCare.Web.Models
{
    public class Species : DictionaryModel
    {
        public string? WateringFrequency { get; set; }
        public string? SunlightRequirements { get; set; }
    }
}
