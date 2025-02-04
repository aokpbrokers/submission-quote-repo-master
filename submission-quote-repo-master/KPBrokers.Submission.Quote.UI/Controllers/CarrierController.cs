using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPBrokers.Submission.Quote.UI.Controllers
{
   // [Authorize(Roles ="Carrier")]
    public class CarrierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
