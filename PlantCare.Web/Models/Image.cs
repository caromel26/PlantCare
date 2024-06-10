using PlantCare.Web.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Web.Models
{
    public class Image : BaseModel
    {
        public int PlantId { get; set; }
        public string? ImageUrl { get; set; }
        public Plant Plant { get; set; }
    }
}
