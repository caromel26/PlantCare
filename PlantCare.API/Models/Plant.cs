using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("Plants")]
    public partial class Plant : DictionaryTable
    {
        public Plant()
        {
            PlantTags = new HashSet<PlantTag>();
            Notes = new HashSet<Note>();
            Images = new HashSet<Image>();
            PlantTasks = new HashSet<PlantTask>();
            Reminders = new HashSet<Reminder>();
        }

        public int UserId { get; set; }
        public int? SpeciesId { get; set; }
        public DateTime? LastWateringDate { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Plants")]
        public virtual User User { get; set; }

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
