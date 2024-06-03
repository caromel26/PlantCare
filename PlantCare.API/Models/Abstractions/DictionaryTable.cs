using System.ComponentModel.DataAnnotations;

namespace PlantCare.API.Models.Abstractions
{
    public abstract class DictionaryTable : BaseDatatable
    {
        [Required(ErrorMessage = "This should have a name!")]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
