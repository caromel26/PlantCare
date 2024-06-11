using PlantCare.Mobile.Models.Abstractions;

namespace PlantCare.Mobile.Models
{
    public class Species : DictionaryModel
    {
        public string? WateringFrequency { get; set; }
        public string? SunlightRequirements { get; set; }
    }
}
