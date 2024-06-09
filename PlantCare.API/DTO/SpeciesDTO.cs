using PlantCare.API.DTO.Abstractions;

namespace PlantCare.API.DTO
{
    public class SpeciesDTO : DictionaryDTO
    {
        public string WateringFrequency { get; set; }
        public string SunlightRequirements { get; set; }
    }
}
