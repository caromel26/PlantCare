using PlantCare.API.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlantCare.API.Models
{
    [Table("Plants")]
    public partial class Plant : DictionaryTable
    {
        public int SpeciesId { get; set; }
        public DateTime? LastWateringDate { get; set; }

        [ForeignKey("SpeciesId")]
        [InverseProperty("Plants")]
        public virtual Species Species { get; set; }

        [InverseProperty("Plant")]
        public virtual ICollection<PlantTag> PlantTags { get; set; }

        [InverseProperty("Plant")]
        public virtual ICollection<Note> Notes { get; set; }

        [InverseProperty("Plant")]
        public virtual ICollection<Image> Images { get; set; }

        [InverseProperty("Plant")]
        public virtual ICollection<PlantTask> PlantTasks { get; set; }

        [InverseProperty("Plant")]
        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
