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

        // Business logic method to create a reminder
        public void CreateReminder(int plantId, DateTime reminderDate)
        {
            var reminder = new Reminder
            {
                PlantId = plantId,
                ReminderDate = reminderDate,
                IsCompleted = false
            };

            _context.Reminders.Add(reminder);
            _context.SaveChanges();
        }

        // Business logic method to complete a reminder
        public void CompleteReminder(int reminderId)
        {
            var reminder = _context.Reminders.Find(reminderId);
            if (reminder != null)
            {
                reminder.IsCompleted = true;
                _context.SaveChanges();
            }
        }
    }
}
