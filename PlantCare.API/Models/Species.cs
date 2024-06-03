using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlantCare.API.Models
{
    [Table("Species")]
    public partial class Species : DictionaryTable
    {
        public Species()
        {
            Plants = new HashSet<Plant>();
        }

        [Display(Name = "Watering frequency")]
        public string? WateringFrequency { get; set; }

        [Display(Name = "Sunlight requirements")]
        public string? SunlightRequirements { get; set; }

        [InverseProperty("Species")]
        public virtual ICollection<Plant> Plants { get; set; }
    }

}
