using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using KPBrokers.Submission.Quote.Common.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KPBrokers.Submission.Quote.API.Utilities;

namespace KPBrokers.Submission.Quote.API.Controllers
{
    // Controller for handling authentication and broker-related requests
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusinessLogic _authentication;   
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;

        // Constructor injecting dependencies for authentication, broker operations, and logging
        public AuthenticationController(IAuthenticationBusinessLogic authentication, ILoggerService logger, IConfiguration configuration )
        {
            _authentication = authentication;            
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Gets API access token by providing the correct api credentials
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Index(string username, string password)
        {
            try
            {
                string ePassword = EncryptorHelper.Encrypt(password);
                var logonModel = await _authentication.GetRequesterServiceAccount(username, ePassword);

                if (logonModel == null)
                    return NotFound("Invalid user account credentials");

                return Ok(
                    new
                    {
                        Username = username,
                        Token = this.GenerateJwtToken(username, logonModel.BrokerId),
                        BrokerId = EncryptorHelper.Encrypt(logonModel.BrokerId.ToString()),
                        Expires = DateTime.Now.AddMinutes(120)
                    });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest("Error has occurred while executing your request");
            }
        }

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns></returns>
        private string GenerateJwtToken(string username, int agentId)
        {
            string jwtKey = _configuration["Jwt:Key"]??string.Empty;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, username),
                new Claim(JwtRegisteredClaimNames.UniqueName, agentId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
