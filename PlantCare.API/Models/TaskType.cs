using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("TaskTypes")]
    public partial class TaskType : DictionaryTable
    {
        public TaskType()
        {
            PlantTasks = new HashSet<PlantTask>();
        }

        [InverseProperty("TaskType")]
        public virtual ICollection<PlantTask> PlantTasks { get; set; }
    }
}
