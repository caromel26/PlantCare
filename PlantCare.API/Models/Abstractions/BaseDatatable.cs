using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PlantCare.API.Models.Abstractions
{
    public abstract class BaseDatatable
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Active")]
        public bool IsActive { get; set; } = true;
        [DisplayName("Created date")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
    }
}
