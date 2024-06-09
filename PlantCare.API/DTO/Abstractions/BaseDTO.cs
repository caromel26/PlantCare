namespace PlantCare.API.DTO.Abstractions
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
