using Microsoft.AspNetCore.Mvc;
using PlantCare.API.BusinessLogic;

namespace PlantCare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantCareController : ControllerBase
    {
        private readonly PlantCareService _plantCareService;

        public PlantCareController(PlantCareService plantCareService)
        {
            _plantCareService = plantCareService;
        }

        [HttpPost("water/{plantId}")]
        public IActionResult WaterPlant(int plantId)
        {
            try
            {
                _plantCareService.WaterPlant(plantId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("generate-reminders")]
        public async Task<IActionResult> GenerateAutomaticReminders()
        {
            try
            {
                await _plantCareService.GenerateAutomaticReminders();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
