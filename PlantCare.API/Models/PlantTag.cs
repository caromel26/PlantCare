using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlantCare.API.Models
{
    [Table("PlantTags")]
    public partial class PlantTag : BaseDatatable
    {
        public int PlantId { get; set; }
        public int TagId { get; set; }

        [ForeignKey("PlantId")]
        public virtual Plant Plant { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
