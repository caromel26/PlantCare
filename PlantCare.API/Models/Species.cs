using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlantCare.API.Models
{
    [Table("Species")]
    public partial class Species : DictionaryTable
    {
        [Display(Name = "Watering frequency")]
        public string WateringFrequency { get; set; }

        [Display(Name = "Sunlight requirements")]
        public string SunlightRequirements { get; set; }

        [InverseProperty("Species")]
        public ICollection<Plant> Plants { get; set; }
    }

}
