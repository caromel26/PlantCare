using Azure;
using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.Models
{
    [Table("PlantTags")]
    public partial class PlantTag : BaseDatatable
    {
        public int PlantId { get; set; }
        public int TagId { get; set; }

        [ForeignKey("PlantId")]
        [InverseProperty("PlantTags")]
        public virtual Plant Plant { get; set; }

        [ForeignKey("TagId")]
        [InverseProperty("PlantTags")]
        public virtual Tag Tag { get; set; }
    }
}
