using KPBrokers.Submission.Quote.UI.Models;
using KPBrokers.Submission.Quote.UI.Models.DocuBox;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using KPBrokers.Submission.Quote.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KPBrokers.Submission.Quote.UI.Controllers
{
    [Authorize(Roles = "Broker,Administrator")]
    public class BrokerController : Controller
    {

        private readonly ILogger _logger;
        private IIdentityService _identityService;
        private readonly IClientFactoryService _clientFactoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrokerController"/> class.
        /// </summary>
        /// <param name="identityService">The identity service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="clientFactory">The client factory.</param>
        public BrokerController(IIdentityService identityService, ILogger<BrokerController> logger, IClientFactoryService clientFactoryService)
        {
            _identityService = identityService;
            _logger = logger;
            _clientFactoryService = clientFactoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new BrokerViewModel()
            {
                Submissions = new List<Models.Entities.Submission>()
            };

            return View(model);
        }
    }
}
       
