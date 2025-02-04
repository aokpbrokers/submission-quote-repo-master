/*
 * Developer: Henry Tshobo
 * Date: 09/11/2024
 */
using KPBrokers.Submission.Quote.API.Models;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPBrokers.Submission.Quote.API.Controllers
{
    [Route("api/common")]
    [ApiController]
    [Authorize]
    public class CommonController : Controller
    {
        private IEmailService _emailService;
        private ILoggerService _logger;
        private readonly IUserAccountService _userAccountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonController"/> class.
        /// </summary>
        /// <param name="emailService">The email service.</param>
        /// <param name="logger">The logger.</param>
        public CommonController(IEmailService emailService, ILoggerService logger, IUserAccountService userAccountService)
        {
            _emailService = emailService;
            _logger = logger;
            _userAccountService = userAccountService;
        }

        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("sendemailasync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmailAsync(EmailEntity model)
        {
            if (model == null)
                return BadRequest("Invalid request, this request is missing json parameter");
            try
            {
                var result = await _emailService.SendEmailAsync(model.EmailTo, model.EmailSubject, model.EmailBody);
                if (result == "Ok")
                    return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

		/// <summary>
		/// Gets the user account by identifier.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		[HttpGet("getuseraccount")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetUserAccountById(string userId)
        {
			if (string.IsNullOrEmpty(userId))
				return BadRequest("Invalid request, this request is missing user id parameter");
			try
			{
                var result = await _userAccountService.GetUserAccountAsync(userId);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}			
		}
    }
}
