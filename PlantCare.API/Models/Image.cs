using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("Images")]
    public partial class Image : BaseDatatable
    {
        public int PlantId { get; set; }
        public string? ImageUrl { get; set; }

        [ForeignKey("PlantId")]
        [InverseProperty("Images")]
        public virtual Plant Plant { get; set; }
    }
}
