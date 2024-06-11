using PlantCare.API.Models.Context;

namespace PlantCare.API.BusinessLogic
{
    public class PlantCareService
    {
        private readonly PlantCareContext _context;

        public PlantCareService(PlantCareContext context)
        {
            _context = context;
        }

        // Business logic method to water a plant
        public void WaterPlant(int plantId)
        {
            var plant = _context.Plants.Find(plantId);
            if (plant != null)
            {
                plant.LastWateringDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
