namespace PlantCare.Mobile.Models.Abstractions
{
    public abstract class DictionaryModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
