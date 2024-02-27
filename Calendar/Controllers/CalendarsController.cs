using Calendar.Models;
using Calendar.Services;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : ControllerBase
    {
        private readonly ICalendarService _context;
        private readonly ILogger<CalendarsController> _logger;

        public CalendarsController(ICalendarService context, ILogger<CalendarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve all Calendars.
        /// </summary>
        // GET: api/Calendars
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ConsultantsCalendars>>> GetAllCalendars()
        {
            try
            {
                var consultants = await _context.GetAllAsync();

                if (consultants == null)
                {
                    _logger.LogInformation("No Consultant found");

                    return NotFound();
                }

                _logger.LogInformation($"List of Calendars retrieved successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok(consultants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Calendars");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieve a specific Calendar by ID.
        /// </summary>
        /// <param name="id">Calendar ID</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsultantsCalendars>> GetCalendarById(int id)
        {
            try
            {
                var consultant = await _context.GetByIdAsync(id);

                if (consultant == null)
                {
                    _logger.LogInformation($"Calendar {id} not found");

                    return NotFound();
                }

                _logger.LogInformation($"Calendar {id} retrieved successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok(consultant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving Calendar {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create a new Calendar.
        /// </summary>
        /// <param name="consultant">Calendar object</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConsultantsCalendars>> CreateCalendar(ConsultantsCalendars consultant)
        {
            try
            {
                if (consultant == null)
                {
                    _logger.LogError("Invalid Calendar data");

                    return BadRequest();
                }

                await _context.CreateAsync(consultant);
                _logger.LogInformation($"New Calendar created successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return CreatedAtAction(nameof(GetCalendarById), new { id = consultant.Id }, consultant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new Calendar");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update a Calendar.
        /// </summary>
        /// <param name="id">Calendar ID</param>
        /// <param name="consultant">Calendar object</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCalendar(int id, ConsultantsCalendars consultant)
        {
            try
            {
                if (id != consultant?.Id)
                {
                    _logger.LogError($"Calendar {id} mismatch");

                    return BadRequest();
                }

                await _context.UpdateAsync(consultant);
                _logger.LogInformation($"Calendar {id} updated successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating Calendar {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete a Calendar by ID.
        /// </summary>
        /// <param name="id">Calendar ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCalendar(int id)
        {
            try
            {
                await _context.DeleteAsync(id);
                _logger.LogInformation($"Calendar {id} deleted successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Calendar {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
