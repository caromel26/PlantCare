using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("Reminders")]
    public partial class Reminder : DictionaryTable
    {
        public int PlantId { get; set; }

        [ForeignKey("PlantId")]
        [InverseProperty("Reminders")]
        public virtual Plant Plant { get; set; }
    }
}
