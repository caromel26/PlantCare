using PlantCare.Mobile.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Mobile.Models
{
    public class PlantTag : BaseModel
    {
        public int PlantId { get; set; }
        public int TagId { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
