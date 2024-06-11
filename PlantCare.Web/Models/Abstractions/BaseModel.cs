namespace PlantCare.Web.Models.Abstractions
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
    }
}
