using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("Notes")]
    public partial class Note : DictionaryTable
    {
        public int PlantId { get; set; }

        [ForeignKey("PlantId")]
        [InverseProperty("Notes")]
        public virtual Plant Plant { get; set; }
    }
}
