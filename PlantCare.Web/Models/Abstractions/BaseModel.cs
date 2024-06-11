using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Web.Models.Abstractions
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
    }
}
