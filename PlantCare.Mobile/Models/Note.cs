using PlantCare.Mobile.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Mobile.Models
{
    public class Note : DictionaryModel
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
    }
}
