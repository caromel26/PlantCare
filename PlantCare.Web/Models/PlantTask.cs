using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantCare.Web.Models.Abstractions;

namespace PlantCare.Web.Models
{
    public class PlantTask : BaseModel
    {
        public int PlantId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime? DueDate { get; set; }
        public bool CompletionStatus { get; set; }
        public TaskType TaskType { get; set; }
        public Plant Plant { get; set; }
    }
}
