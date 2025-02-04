using Humanizer;
using KPBrokers.Submission.Quote.UI.Models;
using KPBrokers.Submission.Quote.UI.Models.Entities;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using KPBrokers.Submission.Quote.UI.Services.Caching;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KPBrokers.Submission.Quote.UI.Controllers
{
	public class ControllerBase : Controller
	{
		private readonly ICacheService _cacheService;
		private readonly IClientFactoryService _clientFactoryService;
		private readonly UserManager<IdentityUser> _userManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerBase"/> class.
		/// </summary>
		/// <param name="cacheService">The cache service.</param>
		/// <param name="clientFactoryService">The client factory service.</param>
		/// <param name="userManager">The user manager.</param>
		public ControllerBase(ICacheService cacheService, IClientFactoryService clientFactoryService, UserManager<IdentityUser> userManager)
		{
			_cacheService = cacheService;
			_clientFactoryService = clientFactoryService;
			_userManager = userManager;
		}

		protected string DisplayResultMessage(string message, bool result)
		{
			if (result)
				return ("<div class=\"alert alert-success\" role=\"alert\">"
					+ "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden = \"true\"> &times;</span></button>"
					+ message + "</strong></div>");
			return ("<div class=\"alert alert-danger\" role=\"alert\">"
				+ "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden = \"true\"> &times;</span></button>"
					+ message + "</strong></div>");
		}

		protected string ToastResultMessage(string message, bool result)
		{

			if (result)
				return ($"<div class=\"toast-container bottom-0 start-0 p-6\"><div class=\"toast\" id=\"liveToast\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\"><div class=\"toast-header\"><i class=\"icon-info\"></i><strong class=\"mr-auto\">Action Result</strong><button type=\"button\" class=\"ml-2 mb-1 close\" data-dismiss=\"toast\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button></div><div class=\"toast-body\"><span class='fw-semibold' style='color:forestgreen;'>{message}</span></div></div>\r\n </div>");
			return ($"<div class=\"toast-container bottom-0 start-0 p-6\"><div class=\"toast\" id=\"liveToast\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\"><div class=\"toast-header\"><i class=\"icon-info\"></i><strong class=\"mr-auto\">Action Result</strong><button type=\"button\" class=\"ml-2 mb-1 close\" data-dismiss=\"toast\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button></div><div class=\"toast-body\"><span class='fw-semibold' style='color:red;'>{message}</span></div></div>\r\n </div>");
		}

		private string CachedKey { get { return "KPBAPILOOKUP"; } }

		/// <summary>
		/// Gets the lookup.
		/// </summary>
		/// <param name="httpClientFactory">The HTTP client factory.</param>
		/// <param name="cache">The cache.</param>
		/// <returns></returns>
		protected async Task<StaticLookup> GetSystemLookupData()
		{
			if (!_cacheService.Exists(CachedKey))
			{
				var lookupUrl = "lookup/lookupentities";
				var jsonLookupResult = await _clientFactoryService.ExecuteGetRequestAsync(lookupUrl);
				var lookup = JsonSerializer.Deserialize<StaticLookup>(jsonLookupResult,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				_cacheService.Save(CachedKey, lookup);

				return lookup;
			}
			else
			{
				var storedLookup = (StaticLookup)_cacheService.Get(CachedKey);
				return storedLookup;
			}
		}

		/// <summary>
		/// Gets or sets the current user account srored data.
		/// </summary>
		/// <value>
		/// The current user account srored data.
		/// </value>
		protected UserAccount CurrentUserAccountSroredData
		{
			get
			{
				var userId = _userManager.GetUserId(this.User);
				if (string.IsNullOrEmpty(userId))
					throw new Exception("Error has occurred while locatind the current logging user data");
				var userAccount = (UserAccount)_cacheService.Get(userId);
				return userAccount;
			}
		}
	}
}
	
