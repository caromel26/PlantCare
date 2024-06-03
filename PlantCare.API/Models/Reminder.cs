using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("Reminders")]
    public partial class Reminder : BaseDatatable
    {
        public int PlantId { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey("PlantId")]
        [InverseProperty("Reminders")]
        public virtual Plant Plant { get; set; }
    }
}
