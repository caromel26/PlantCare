using Microsoft.EntityFrameworkCore;
using PlantCare.API.Models;
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

        public async Task GenerateAutomaticReminders()
        {
            var tasks = await _context.PlantTasks
                .Include(t => t.Plant)  // Include Plant to access Plant.Name
                .Include(t => t.TaskType)  // Include TaskType to access TaskType.Name
                .Where(t => t.DueDate <= DateTime.Today && !t.CompletionStatus)
                .ToListAsync();

            foreach (var task in tasks)
            {
                var reminder = new Reminder
                {
                    PlantId = task.PlantId,
                    Name = "PlantReminder",  // Automatically set Name
                    Description = $"{task.Plant.Name} -> {task.TaskType.Name}"  // Automatically set Description
                };
                _context.Reminders.Add(reminder);
            }

            await _context.SaveChangesAsync();
        }

    }
}
