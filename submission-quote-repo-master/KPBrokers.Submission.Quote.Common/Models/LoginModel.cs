namespace KPBrokers.Submission.Quote.Common;

/// <summary>
/// Model representing user login credentials.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Gets or sets the username for login.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the password for login.
    /// </summary>
    public string? Password { get; set; }

    public int BrokerId { get; set; }
}
