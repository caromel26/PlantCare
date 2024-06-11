using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("PlantTasks")]
    public partial class PlantTask : BaseDatatable
    {
        public int PlantId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime DueDate { get; set; }
        public bool CompletionStatus { get; set; } = false;

        [ForeignKey("PlantId")]
        public virtual Plant Plant { get; set; }

        [ForeignKey("TaskTypeId")]
        public virtual TaskType TaskType { get; set; }
    }
}
