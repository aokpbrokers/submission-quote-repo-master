namespace KPBrokers.Submission.Quote.UI.Models
{
    /// <summary>
    /// Represents the data model for user login information.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the username entered by the user.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password entered by the user.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be remembered on the device.
        /// </summary>
        public bool RememberMe { get; set; }
    }

}
