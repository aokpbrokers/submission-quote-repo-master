using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPBrokers.Submission.Quote.UI.Controllers
{
    [Authorize(Roles ="Agent")]
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Quotes the submission.
        /// </summary>
        /// <returns></returns>
        public IActionResult QuoteSubmission()
        {
            return View();
        }
    }
}
