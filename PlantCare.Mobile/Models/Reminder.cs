using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantCare.Mobile.Models.Abstractions;

namespace PlantCare.Mobile.Models
{
    public class Reminder : BaseModel
    {
        public int PlantId { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
