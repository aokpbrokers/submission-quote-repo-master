using KPBrokers.Submission.Quote.UI.Models;
using KPBrokers.Submission.Quote.UI.Models.Entities;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using KPBrokers.Submission.Quote.UI.Services.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;

namespace KPBrokers.Submission.Quote.UI.Controllers
{    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICacheService _cacheService;
        private readonly IClientFactoryService _clientFactoryService;
        private readonly IIdentityService _identityService;


        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userManage">The user manage.</param>
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICacheService cacheService, IClientFactoryService clientFactoryService, IIdentityService identityService)
        {
            _logger = logger;
            _userManager = userManager;
            _cacheService = cacheService;
            _clientFactoryService = clientFactoryService;
            _identityService = identityService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = this.User;
            await StoreAuthenticationData();
			return RedirectLoginUserToDashboard(currentUser);
        }

        /// <summary>
        /// Redirects the login user to dashboard.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        /// <returns></returns>
        private RedirectToActionResult RedirectLoginUserToDashboard(ClaimsPrincipal currentUser)
        {
            RedirectToActionResult redirect = RedirectToAction("index", "home");
            try
            {               

				if (currentUser.IsInRole("Broker"))
                {
                    redirect = RedirectToAction("index", "broker");
                }
                if (currentUser.IsInRole("Agent"))
                {
                    redirect = RedirectToAction("index", "agent");
                }
                if (currentUser.IsInRole("Carrier"))
                {
                    redirect = RedirectToAction("index", "carrier");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);               
            }
            return redirect;
        }

        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private async Task<string> GetUserRole(string userId)
        {
            var role = await _identityService.GetCurrentLoginUserRole(userId);
            return role;
        }

		private async Task StoreAuthenticationData()
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
					throw new Exception($"Error has occurred while locating authentication data for user {User.Identity.Name}");

				if (!_cacheService.Exists(user.Id))
				{
					string url = $"common/getuseraccount?userId={user.Id}";
					var jsonResult = await _clientFactoryService.ExecuteGetRequestAsync(url);

					if (!string.IsNullOrEmpty(jsonResult))
					{
						var userAccountData = JsonSerializer.Deserialize<UserAccount>(jsonResult,
							new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        userAccountData.Role = await GetUserRole(user.Id);

						_cacheService.Save(user.Id, userAccountData);
					}
				}
			}
		}
	}
}
