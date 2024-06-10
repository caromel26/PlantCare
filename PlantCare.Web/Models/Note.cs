﻿using PlantCare.Web.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Web.Models
{
    public class Note : DictionaryModel
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
    }
}
