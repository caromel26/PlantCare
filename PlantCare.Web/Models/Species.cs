using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantCare.Web.Models.Abstractions;

namespace PlantCare.Web.Models
{
    public class Species : DictionaryModel
    {
        public string? WateringFrequency { get; set; }
        public string? SunlightRequirements { get; set; }
    }
}
