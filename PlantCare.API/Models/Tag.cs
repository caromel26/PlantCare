﻿using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("Tags")]
    public partial class Tag : DictionaryTable
    {
        public Tag()
        {
            PlantTags = [];
        }

        [InverseProperty("Tag")]
        public virtual ICollection<PlantTag> PlantTags { get; set; }
    }
}
