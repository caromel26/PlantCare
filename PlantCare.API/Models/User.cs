using PlantCare.API.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlantCare.API.Models
{
    [Table("Users")]
    public partial class User : DictionaryTable
    {
        public User()
        {
            Plants = new HashSet<Plant>();
        }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Plant> Plants { get; set; }
    }

}
