using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.BusinessLogic.Concretes;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPBrokers.Submission.Quote.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class LookUpController : Controller
    {
        private readonly ILookUpBusinessLogic _lookupBusinessLogic;
        private readonly ILoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpController"/> class.
        /// </summary>
        /// <param name="lookUpBusinessLogic">The look up business logic.</param>
        /// <param name="logger">The logger.</param>
        public LookUpController(ILookUpBusinessLogic lookUpBusinessLogic, ILoggerService logger)
        {
            _lookupBusinessLogic = lookUpBusinessLogic;
            _logger = logger;

        }

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lookup/countries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _lookupBusinessLogic.GetCountriesAsync();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in controller while retrieving countries.");
                return StatusCode(500, "An error occurred while retrieving countries.");
            }
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lookup/statuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Status>> GetStatus()
        {
            try
            {
                var statuses = _lookupBusinessLogic.GetStatusAsync();
                return Ok(statuses);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in controller while retrieving statuses.");
                return StatusCode(500, "An error occurred while retrieving statuses.");
            }
        }

        /// <summary>
        /// Gets the titles.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lookup/titles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTitles()
        {
            try
            {
                var titles = await _lookupBusinessLogic.GetTitlesAsync();
                return Ok(titles);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in controller while retrieving titles.");
                return StatusCode(500, "An error occurred while retrieving titles.");
            }
        }

        /// <summary>
        /// Gets the coverages.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lookup/coverages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCoverages()
        {
            try
            {
                var coverages = await _lookupBusinessLogic.GetCoveragesAsync();
                return Ok(coverages);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in controller while retrieving coverages.");
                return StatusCode(500, "An error occurred while retrieving coverages.");
            }

        }

        /// <summary>
        /// Gets all lookup.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lookup/lookupentities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllLookup()
        {
            try
            {
                var lookup = await _lookupBusinessLogic.GetLookupDataAsync();
                return Ok(lookup);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in controller while retrieving coverages.");
                return StatusCode(500, "An error occurred while retrieving coverages.");
            }
        }
    }
}
