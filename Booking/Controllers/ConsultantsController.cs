using Booking.Models;
using Booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantsController : ControllerBase
    {
        private readonly IConsultantService _context;
        private readonly ILogger<ConsultantsController> _logger;

        public ConsultantsController(IConsultantService context, ILogger<ConsultantsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve all Consultants.
        /// </summary>
        // GET: api/Consultants
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Consultant>>> GetAllConsultants()
        {
            try
            {
                var consultants = await _context.GetAllAsync();

                if (consultants == null)
                {
                    _logger.LogInformation("No Consultant found");

                    return NotFound();
                }

                _logger.LogInformation($"List of Consultants retrieved successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok(consultants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Consultants");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieve a specific Consultant by ID.
        /// </summary>
        /// <param name="id">Consultant ID</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Consultant>> GetConsultantById(int id)
        {
            try
            {
                var consultant = await _context.GetByIdAsync(id);

                if (consultant == null)
                {
                    _logger.LogInformation($"Consultant {id} not found");

                    return NotFound();
                }

                _logger.LogInformation($"Consultant {id} retrieved successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok(consultant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving Consultant {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create a new Consultant.
        /// </summary>
        /// <param name="consultant">Consultant object</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Consultant>> CreateConsultant(Consultant consultant)
        {
            try
            {
                if (consultant == null)
                {
                    _logger.LogError("Invalid Consultant data");

                    return BadRequest();
                }

                await _context.CreateAsync(consultant);
                _logger.LogInformation($"New Consultant created successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return CreatedAtAction(nameof(GetConsultantById), new { id = consultant.Id }, consultant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new Consultant");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update a Consultant.
        /// </summary>
        /// <param name="id">Consultant ID</param>
        /// <param name="consultant">Consultant object</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateConsultant(int id, Consultant consultant)
        {
            try
            {
                if (id != consultant?.Id)
                {
                    _logger.LogError($"Consultant {id} mismatch");

                    return BadRequest();
                }

                await _context.UpdateAsync(consultant);
                _logger.LogInformation($"Consultant {id} updated successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating Consultant {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete a Consultant by ID.
        /// </summary>
        /// <param name="id">Consultant ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteConsultant(int id)
        {
            try
            {
                await _context.DeleteAsync(id);
                _logger.LogInformation($"Consultant {id} deleted successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Consultant {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
