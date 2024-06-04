using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantCare.Mobile.Models.Abstractions;

namespace PlantCare.Mobile.Models
{
    public class PlantTask : BaseModel
    {
        public int PlantId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime DueDate { get; set; }
        public bool CompletionStatus { get; set; }
    }
}
