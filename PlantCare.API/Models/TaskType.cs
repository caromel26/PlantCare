using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlantCare.API.Models
{
    [Table("TaskTypes")]
    public partial class TaskType : DictionaryTable
    {
        public TaskType()
        {
            PlantTasks = [];
        }

        [InverseProperty("TaskType")]
        public virtual ICollection<PlantTask> PlantTasks { get; set; }
    }
}
