using Azure.Core;
using KPBrokers.Submission.Quote.UI.Models;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;

namespace KPBrokers.Submission.Quote.UI.Services.Concretes
{
    public class IdentityService : PageModel, IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ISenderEmailService _emailSender;
        private readonly ILogger<IdentityService> _logger;
        private IUrlHelper urlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="userStore">The user store.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="logger">The logger.</param>
        public IdentityService(UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager, ILogger<IdentityService> logger, ISenderEmailService emailSender, IUrlHelper _urlHelper)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailStore = GetEmailStore();
            _emailSender = emailSender;
            this.urlHelper = _urlHelper;
        }

        /// <summary>
        /// Registers the user account.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        public async Task<string> RegisterUserAccount(AccountContact contact, HttpContext httpContext)
        {
            var user = CreateUser();
            var password = GenerateSecurePassword();
            await _userStore.SetUserNameAsync(user, contact.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, contact.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                var userId = await _userManager.GetUserIdAsync(user);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var createdUser = await _userManager.FindByIdAsync(userId);
                if (createdUser != null)
                {
                    await _userManager.AddToRoleAsync(createdUser, contact.ContactRole);
                    if (contact.ContactRole == "Broker" && contact.IsAdmin == true)
                    {
                        await _userManager.AddToRoleAsync(createdUser, "Administrator");
                    }
                }

                var callbackUrl = urlHelper.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = string.Empty },
                    protocol: httpContext.Request.Scheme);

                var emailModel = new EmailEntity()
                {
                    EmailTo = contact.Email,
                    EmailSubject = "Please comfirm your email",
                    EmailBody = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                };
                //send an email to user
                await _emailSender.SendEmailAsync(emailModel);

                return ("Account created");
            }
            return ("Error has occurred while creating user account");
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <returns></returns>
        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        /// <summary>
        /// Gets the email store.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">The default UI requires a user store with email support.</exception>
        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }

        /// <summary>
        /// Generates a secure, random password with a specified length and allowed characters.
        /// </summary>
        /// <returns></returns>
        private string GenerateSecurePassword()
        {
            var random = new Random();
            var length = 12;

            // Characters to include: uppercase, lowercase, digits, and special characters
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_-+=<>?";

            var password = new List<char>();
            password.Add(upperChars[random.Next(upperChars.Length)]);
            password.Add(lowerChars[random.Next(lowerChars.Length)]);
            password.Add(digits[random.Next(digits.Length)]);
            password.Add(specialChars[random.Next(specialChars.Length)]);

            const string allChars = upperChars + lowerChars + digits + specialChars;
            password.AddRange(Enumerable.Range(0, length - password.Count)
                .Select(_ => allChars[random.Next(allChars.Length)]));

            password = password.OrderBy(_ => random.Next()).ToList();

            return new string(password.ToArray());
        }

        /// <summary>
        /// Gets all user account details.
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountContact>> GetAllUserAccountDetails()
        {
            var identityUsers = await _userManager.Users.ToListAsync();
            var userList = new List<AccountContact>();

            if (identityUsers != null)
            {
                foreach (var identityUser in identityUsers)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);
                    bool isAdmin = await _userManager.IsInRoleAsync(identityUser, "Administrator");

                    var accountContact = new AccountContact
                    {
                        UserId = identityUser.Id,
                        CompanyName = "",
                        FullName = identityUser.UserName,
                        Email = identityUser.Email,
                        Phone = identityUser.PhoneNumber,
                        AccessFailedCount = identityUser.AccessFailedCount,
                        LockUser = identityUser.LockoutEnabled,
                        IsMFAEnabled = identityUser.TwoFactorEnabled,
                        LockoutEndDate = identityUser.LockoutEnd,
                        ConfirmedEmail = identityUser.EmailConfirmed,
                        ContactRole = await GetUserRole(identityUser),
                        IsAdmin = isAdmin
                    };

                    userList.Add(accountContact);
                }
            }
            return userList;
        }

        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private async Task<string> GetUserRole(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.Where(x => x != "Administrator").FirstOrDefault();
            return role;
        }

        /// <summary>
        /// Gets the current login user role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> GetCurrentLoginUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                return string.Empty;
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.Where(x => x != "Administrator").FirstOrDefault();
            return role;
        }

        /// <summary>
        /// Updates the user account.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserAccount(AccountContact model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var isUserBroker = await _userManager.IsInRoleAsync(user, "Broker");

            if (user == null)
                return false;

            if (model.LockUser != user.LockoutEnabled)
                user.LockoutEnabled = model.LockUser;

            if (user.TwoFactorEnabled != model.IsMFAEnabled)
                user.TwoFactorEnabled = model.IsMFAEnabled;

            if (user.EmailConfirmed != model.ConfirmedEmail)
                user.EmailConfirmed = model.ConfirmedEmail;

            if (isUserBroker)
            {
                var admin = await _userManager.IsInRoleAsync(user, "Administrator");
                var roleResult = (!admin && model.IsAdmin)
                                   ? await _userManager.AddToRoleAsync(user, "Administrator")
                                   : await _userManager.RemoveFromRoleAsync(user, "Administrator");

                if (!roleResult.Succeeded)
                    return false;
            }

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<bool> ResetUserPassword(AccountContact model)
        {
            string password = string.Empty;
            var user = await _userManager.FindByIdAsync(model.UserId!);
            if (user == null)
                return false;

            password = (!string.IsNullOrEmpty(model.Password)) ? model.Password : GenerateSecurePassword();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, password);

            if (resetResult.Succeeded)
            {
                var emailModel = new EmailEntity()
                {
                    EmailTo = model.Email!,
                    EmailSubject = "Quote Submission Password Reset",
                    EmailBody = $"Your password has been reset by the system administrator. Please see the new password '{password}', and you are required to change your password once you log in"
                };
                await _emailSender.SendEmailAsync(emailModel);
                return resetResult.Succeeded;
            }

            return false;
        }
    }
}
