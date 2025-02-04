using KPBrokers.Submission.Quote.UI.Controllers;
using KPBrokers.Submission.Quote.UI.Models.Entities;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using KPBrokers.Submission.Quote.UI.Services.Caching;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace KPBrokers.Submission.Quote.UI.Helpers
{
	public static class UIHelpers
	{
        /// <summary>
        /// Currents the login user.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns></returns>
        public static string CurrentLoginUser(this IHtmlHelper htmlHelper)
		{
			IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
			var currentUserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			return GetCurrentUserName(currentUserId);
		}

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private static string GetCurrentUserName(string userId)
		{
			ICacheService cache = new RuntimeCacheService();
			if (cache.Exists(userId))
			{
				var userStoredData = (UserAccount)cache.Get(userId);
				return userStoredData.Fullname;
			}
			return string.Empty;			
		}

        /// <summary>
        /// Currents the login user role.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns></returns>
        public static string CurrentLoginUserRole(this IHtmlHelper htmlHelper)
        {
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var currentUserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return GetCurrentUserRole(currentUserId);
        }
        /// <summary>
        /// Gets the current user role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private static string GetCurrentUserRole(string userId)
        {
            ICacheService cache = new RuntimeCacheService();
            if (cache.Exists(userId))
            {
                var userStoredData = (UserAccount)cache.Get(userId);
                return userStoredData.Role;
            }
            return string.Empty;
        }
    }
}
