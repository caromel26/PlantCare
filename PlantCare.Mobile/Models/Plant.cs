using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantCare.Mobile.Models.Abstractions;

namespace PlantCare.Mobile.Models
{
    public class Plant : DictionaryModel
    {
        public int? SpeciesId { get; set; }
        public DateTime? LastWateringDate { get; set; }
    }
}
