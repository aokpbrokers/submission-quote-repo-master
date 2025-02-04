using KPBrokers.Submission.Quote.UI.Models;
using KPBrokers.Submission.Quote.UI.Models.DocuBox;
using KPBrokers.Submission.Quote.UI.Models.Entities;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using KPBrokers.Submission.Quote.UI.Services.Caching;
using KPBrokers.Submission.Quote.UI.Services.Security;
using KPBrokers.Submission.Quote.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using Twilio.TwiML.Voice;

namespace KPBrokers.Submission.Quote.UI.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class AdminController : ControllerBase
	{
		private readonly ILogger _logger;
		private IIdentityService _identityService;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IClientFactoryService _clientFactoryService;
		private readonly ICacheService _cacheService;

		/// <summary>
		/// Initializes a new instance of the <see cref="AdminController"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="identityService">The identity service.</param>
		/// <param name="clientFactoryService">The client factory service.</param>
		public AdminController(ILogger<AdminController> logger, IIdentityService identityService, IClientFactoryService clientFactoryService, ICacheService cacheService, UserManager<IdentityUser> userManager)
		:base (cacheService, clientFactoryService, userManager)
		{
			_logger = logger;
			_identityService = identityService;
			_clientFactoryService = clientFactoryService;
			_cacheService = cacheService;
			_userManager = userManager;
		}

		/// <summary>
		/// Defaut home page for admin.
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		#region  Agent Management
		/// <summary>
		/// Agentses this instance.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Agents()
		{
			var model = new BrokerViewModel();
			try
			{
				var lookup = await GetSystemLookupData();
				var countries = lookup.Countries.ToList();
				if (countries == null)
				{
					ModelState.AddModelError("Countries", "Error has occurred while getting countries from the api");
					return View(model);
				}
				var agentRequestUrl = "admin/agent/getagents";
				var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(agentRequestUrl);
				var agentCompanies = JsonSerializer.Deserialize<List<Agent>>(jsonString,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				if (agentCompanies == null)
				{
					ModelState.AddModelError("Agent list", "Error has occurred while getting countries from the api");
					model.Agents = new List<Agent>();
					return View(model);
				}

				model.Agents = agentCompanies;
				model.Countries = countries.Select(c => new Microsoft.AspNetCore.Mvc.Rendering
											   .SelectListItem
				{
					Value = c.CountryId.ToString(),
					Text = c.CountryName
				}).ToList();

				if (TempData["Message"] != null)
					model.DisplayMessage = TempData["Message"]?.ToString();

				return View(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return View("Error");
			}
		}

		/// <summary>
		/// Creates the agent.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAgent(BrokerViewModel model)
		{
			if (model.Company == null)
				TempData["Message"] = ToastResultMessage("Error has occurred while validating your request the agent detail are missing", false);
            
			string action=string.Empty;

            var _model = new BrokerViewModel();
			try
			{
				var agent = new Agent();
				if (model.Agent != null)
				{
					model.Agent.IsActive = true;
					model.Agent.CreatedBy = CurrentUserAccountSroredData.UserId;
					model.Agent.CreatedDate = DateTime.Now;
					model.Agent.UpdatedBy = CurrentUserAccountSroredData.UserId;
					model.Agent.UpdatedDate = DateTime.Now;

					agent = model.Agent;
					action = "agents";
                }
				else
				{
					var agentDetail = new Agent()
					{
						SecondaryAgentId = model.Company.Id,
						Name = model.Company.Name,
						DBA = model.Company.DBA,
						IsActive = true,
						AddressLine1 = model.Company.AddressLine1,
						AddressLine2 = model.Company.AddressLine2,
						City = model.Company.City,
						State = model.Company.State,
						PostalCode = model.Company.PostalCode,
						CountryId = 1,
						CreatedBy = CurrentUserAccountSroredData.UserId,
						CreatedDate = DateTime.Now,
						UpdatedBy = CurrentUserAccountSroredData.UserId,
						UpdatedDate = DateTime.Now
					};
					agent = agentDetail;
					action = "agentsearch";

                }
				string url = "admin/agent/createagent";
				var result = await _clientFactoryService.ExecutePostRequestAsync(url, agent);
				TempData["Message"] = (!string.IsNullOrEmpty(result) && Convert.ToInt32(result) > 0) ?
									  ToastResultMessage($"The Agent {agent.Name} has been successfully created.", true)
									  : ToastResultMessage("An error has occurred while executing your request", false);
				_model.Companies = new List<Company>();				

				return RedirectToAction(action);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return View("Error");
			}
		}


		/// <summary>
		/// Agents the search.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult AgentSearch()
		{
			var model = new BrokerViewModel()
			{
				Companies = new List<Company>()
			};

            if (TempData["Message"] != null)
				model.DisplayMessage = TempData["Message"].ToString();

			return View(model);
		}

		/// <summary>
		/// Agents the search.
		/// </summary>
		/// <param name="keyword">The keyword.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AgentSearch(IFormCollection field)
		{
			var model = new BrokerViewModel();
			var keyword = field["txtAgentSearch"];
			try
			{
				var companies = new List<Company>();
				var url = $"admin/agent/search?companyName={keyword}";
				var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(url);
				if (!string.IsNullOrEmpty(jsonString))
				{
					var agentCompanies = JsonSerializer.Deserialize<List<AgentCompany>>(jsonString,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

					if (agentCompanies != null)
					{
						foreach (var agentCompany in agentCompanies)
						{
							companies.Add(new Company
							{
								Id = agentCompany.AgentId,
								Name = agentCompany.AgentName,
								DBA = agentCompany.DBA,
								AccountEmail = agentCompany.AccountEmail,
								AddressId = agentCompany.AddressId,
								AddressLine1 = agentCompany.AddressLine1,
								AddressLine2 = agentCompany.AddressLine2,
								City = agentCompany.City,
								State = agentCompany.State,
								PostalCode = agentCompany.PostalCode,
								Country = agentCompany.Country,
								Role = "Agent"
							});
						}
						model.Companies = companies;
					}
					return View(model);
				}

				model.Companies = new List<Company>();
				model.DisplayMessage = $"Could not find any agent with the name of {keyword}";

				return View(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return View("Error");
			}
		}

		/// <summary>
		/// Edits the agent.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditAgent(BrokerViewModel model)
		{
			if (model.Agent == null)
			{
				ModelState.AddModelError("Agent", "The agent model coudn't be validated, please make sure all of the fields have values");
				return RedirectToAction("agents");
			}

			try
			{
				model.Agent.UpdatedBy = CurrentUserAccountSroredData.UserId;
				model.Agent.UpdatedDate = DateTime.Now;

				var agentRequestUrl = "admin/agent/updateagent";
				var jsonStringResult = await _clientFactoryService.ExecutePostRequestAsync(agentRequestUrl, model.Agent);

				if (!string.IsNullOrEmpty(jsonStringResult) && Convert.ToInt32(jsonStringResult) > 0)
				{
					TempData["Message"] = ToastResultMessage("The agent detail has been successfully updated", true);
				}
				else
				{
					TempData["Message"] = ToastResultMessage("Error has occurred while executing your request", false);
				}

				return RedirectToAction("agents");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return View("Error");
			}
		}

		/// <summary>
		/// Agents the contacts.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> AgentContacts(string id)
		{
			var model = new BrokerViewModel();

			var decrypted = EncryptorHelper.Decrypt(id);
			if (String.IsNullOrEmpty(decrypted))
			{
				ModelState.AddModelError("AgentContacts", "Agent id was not provided as a parameter");
				TempData["Message"] = ToastResultMessage("Error has occurred while executing your request", false);
				return RedirectToAction("agents");
			}

			try
			{
				var splitId = decrypted.Split('|');
				int agentId = Convert.ToInt32(splitId[0]);
				int docuboxAgentId = Convert.ToInt32(string.IsNullOrEmpty(splitId[1]) ? 0 : splitId[1]);
				string agentName = splitId[2];

				var lookup = await GetSystemLookupData();
				var titles = lookup.Titles;

				var agentRequestUrl = $"admin/agentcontact/contacts?agentId={agentId}";
				var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(agentRequestUrl);
				var agentcontacts = JsonSerializer.Deserialize<List<AgentContact>>(jsonString,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				model.AgentContacts = (agentcontacts != null && agentcontacts.Count > 0) ? agentcontacts : [];

				if (docuboxAgentId > 0)
				{
					var docuboxAgentRequestUrl = $"admin/agent/contacts?agentId={docuboxAgentId}";
					var docuboxJsonString = await _clientFactoryService.ExecuteGetRequestAsync(docuboxAgentRequestUrl);
					var docuboxAgentcontacts = JsonSerializer.Deserialize<List<DocuboxAgentContact>>(docuboxJsonString,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

					model.DocuboxAgentContacts = (docuboxAgentcontacts != null && docuboxAgentcontacts.Count > 0) ?
						 docuboxAgentcontacts : [];
				}
				else
				{
					model.DocuboxAgentContacts = [];
				}

				model.Titles = titles.Select(c => new Microsoft.AspNetCore.Mvc.Rendering
											   .SelectListItem
				{
					Value = c.TitleId.ToString(),
					Text = c.TitleName
				}).ToList();

				model.Agent = new Agent { Name = agentName, AgentId = agentId, SecondaryAgentId = docuboxAgentId };
				model.UserAccount = new AccountContact { CompanyId = agentId, ContactRole = "Agent", CompanyName=agentName, CompanySecondaryId=docuboxAgentId };
				if (TempData["Message"] != null)
					model.DisplayMessage = TempData["Message"].ToString();

				//filter broker view model
				var _model = FilterAgentContacts(model);

				return View(_model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return View("Error");
			}
		}

        /// <summary>
        /// Filters the contacts.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private BrokerViewModel FilterAgentContacts(BrokerViewModel model)
		{
			if (model.DocuboxAgentContacts.Count == 0)
				return model;

			if (model.AgentContacts.Count == 0)
				return model;

			foreach(var dbxContact in model.DocuboxAgentContacts)
			{
				foreach(var contact in model.AgentContacts)
				{
					if (dbxContact.AgentContactId == contact.SecondaryAgentContactId)
						dbxContact.InSystem = true;
				}
			}
			return model;
		}		

        /// <summary>
        /// Updates the agent contact.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateAgentContact(BrokerViewModel model)
		{
			try
			{
				var contactUser = await _userManager.FindByEmailAsync(model.AgentContact.Email);
				if (contactUser == null)
				{
                    ToastResultMessage($"Error has occurred while locating user account for {model.AgentContact.Email}", false);
                    return RedirectToAction("agentcontacts");
                }

				var httpRequestAgentContact = new AgentContact
				{
					AgentContactId = model.AgentContact.AgentContactId,
					IdentityId = contactUser.Id.ToString(),
					TitleId = model.AgentContact.TitleId,
					FirstName = model.AgentContact.FirstName,
					LastName = model.AgentContact.LastName,
					Email = model.AgentContact.Email,
					Phone = model.AgentContact.Phone,
					IsActive = model.AgentContact.IsActive,
					UpdatedBy = CurrentUserAccountSroredData.UserId,
					UpdatedDate = DateTime.Now
				};

				var agentContactAddRequestUrl = "admin/agentcontact/updateagentcontact";
				var jsonString = await _clientFactoryService.ExecutePostRequestAsync(agentContactAddRequestUrl, httpRequestAgentContact);
				int result = (!string.IsNullOrEmpty(jsonString)) ? Convert.ToInt32(jsonString) : 0;

				TempData["Message"] = (result > 0)
										? ToastResultMessage("Angent contact has been successfully updated", true)
										: ToastResultMessage("Error has occurred while updating the agent contact", false);

				return RedirectToAction("agentcontacts", new { id = EncryptorHelper.Encrypt(model.Agent.AgentId.ToString() + "|" + model.Agent.SecondaryAgentId.ToString() +"|"+model.Agent.Name) });
			}
			catch(Exception ex)
			{
				_logger?.LogError(ex, ex.Message);
				return View("Error");
			}
        }

        /// <summary>
        /// Inserts the agent contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <param name="identityId">The identity identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while executing 'InsertAgentContact' function</exception>
        private async Task<int> InsertAgentContact(AccountContact contact, string identityId)
		{
			var httpRequestAgentContact = new AgentContact
			{
				SecondaryAgentContactId = contact.SecondaryId,
				AgentId=contact.CompanyId,
				IdentityId = identityId,
				TitleId = contact.TitleId,
				FirstName = contact.FirstName,
				LastName = contact.LastName,
				Email = contact.Email,
				Phone = contact.Phone,
				IsActive = true,
				CreatedBy = CurrentUserAccountSroredData.UserId,
				CreatedDate = DateTime.Now,
				UpdatedBy = CurrentUserAccountSroredData.UserId,
				UpdatedDate = DateTime.Now
			};

			var agentContactAddRequestUrl = "admin/agentcontact/addagentcontact";            
            var jsonString = await _clientFactoryService.ExecutePostRequestAsync(agentContactAddRequestUrl, httpRequestAgentContact);

			if (!string.IsNullOrEmpty(jsonString))
				return Convert.ToInt32(jsonString);
			throw new Exception("Error has occurred while executing 'InsertAgentContact' function");
        }

		#endregion		

		#region Common

		/// <summary>
		/// Creates the account.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(BrokerViewModel model)
        {
            if (model.UserAccount == null)
                TempData["Message"] = ToastResultMessage("The input validation has failed, please ensure that all input are filled in.", false);

            try
            {
                var account = new AccountContact
                {
                    ContactRole = model.UserAccount.ContactRole,
                    CompanyId = model.UserAccount.CompanyId,
					CompanyName = model.UserAccount.CompanyName,
					CompanySecondaryId = model.UserAccount.CompanySecondaryId,
					SecondaryId = model.UserAccount.SecondaryId,
                    TitleId = model.UserAccount.TitleId,
                    FirstName = model.UserAccount.FirstName,
                    LastName = model.UserAccount.LastName,
                    Email = model.UserAccount.Email,
                    Phone = model.UserAccount.Phone,
                    IsAdmin = (model.UserAccount.IsAdmin) ? true : false
                };

                var result = await _identityService.RegisterUserAccount(account, HttpContext);
                int userAccountResult = 0;
                string action = string.Empty;

				if (!string.IsNullOrEmpty(result) && result== "Account created")
                {
					var createdUser = await _userManager.FindByEmailAsync(model.UserAccount.Email);
					var setupResult = await SetupAccountAction(model, account, createdUser);
					if (setupResult != null)
					{
						action = setupResult.Action;
						userAccountResult = setupResult.UserAccountResult;
					}
				}

                TempData["Message"] = (result == "Account created" && userAccountResult > 0)
                    ? ToastResultMessage($"The {model.UserAccount.ContactRole} contact detail has been successfully created", true)
                    : ToastResultMessage($"Error has occurred while creating the {model.UserAccount.ContactRole} contact ", false);

				var actionUrl = (!string.IsNullOrEmpty(action) ? action : ActionFilter(model.UserAccount.ContactRole));
                return RedirectToAction(actionUrl, new { id = EncryptorHelper.Encrypt(account.CompanyId.ToString() + "|" + account.CompanySecondaryId.ToString()+"|"+account.CompanyName) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View("Error");
            }
        }

        /// <summary>
        /// Setups the account action.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="account">The account.</param>
        /// <param name="createdUser">The created user.</param>
        /// <returns></returns>
        private async Task<dynamic> SetupAccountAction(BrokerViewModel model, AccountContact account, IdentityUser? createdUser)
        { 
			string action = string.Empty;
			int userAccountResult = 0;
            switch (model.UserAccount.ContactRole.ToLower())
            {
                case "agent":
                    action = "agentcontacts";
                    userAccountResult = await InsertAgentContact(account, createdUser.Id);
                    break;
                case "broker":
                    action = "brokercontacts";
                    userAccountResult = await InsertBrokerContact(account, createdUser.Id);
                    break;
                case "carrier":
                    action = "carriercontacts";
                    userAccountResult = await InsertCarrierContact(account, createdUser.Id);
                    break;
            }
			return  new { Action = action, UserAccountResult = userAccountResult };
        }

        /// <summary>
        /// Actions the filter.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        private string ActionFilter(string role)
		{
			string action=string.Empty;
            switch (role.ToLower())
            {
                case "agent":
                    action = "agentcontacts";                  
                    break;
                case "broker":
                    action = "brokercontacts";                  
                    break;
                case "carrier":
                    action = "carriercontacts";                 
                    break;
            }
			return action;
        }
		#endregion

		#region  Broker Management
		/// <summary>
		/// Brokerses this instance.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Brokers()
		{
			var model = new BrokerViewModel();
			try
			{
				var lookup = await GetSystemLookupData();
				var countries = lookup.Countries.ToList();
				if (countries == null)
				{
					ModelState.AddModelError("Countries", "Error has occurred while getting countries from the api");
					return View(model);
				}
				var brokerRequestUrl = "admin/broker/getbrokers";
				var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(brokerRequestUrl);
				var brokerCompanies = JsonSerializer.Deserialize<List<Broker>>(jsonString,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				if (brokerCompanies == null)
				{
					ModelState.AddModelError("Broker list", "Error has occurred while getting countries from the api");
					model.Brokers = new List<Broker>();
					return View(model);
				}

				model.Brokers = brokerCompanies;
				model.Countries = countries.Select(c => new Microsoft.AspNetCore.Mvc.Rendering
											   .SelectListItem
				{
					Value = c.CountryId.ToString(),
					Text = c.CountryName
				}).ToList();

				if (TempData["Message"] != null)
					model.DisplayMessage = TempData["Message"]?.ToString();

				return View(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return View("Error");
			}
		}

		/// <summary>
		/// Creates the broker.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateBroker(BrokerViewModel model)
		{
			if (model.Company == null)
				TempData["Message"] = ToastResultMessage("Error has occurred while validating your request the broker detail are missing", false);

			string action = string.Empty;

			var _model = new BrokerViewModel();
			try
			{
				var broker = new Broker();
				if (model.Broker != null)
				{
					model.Broker.IsActive = true;
					model.Broker.CreatedBy = CurrentUserAccountSroredData.UserId;
					model.Broker.CreatedDate = DateTime.Now;
					model.Broker.UpdatedBy = CurrentUserAccountSroredData.UserId;
					model.Broker.UpdatedDate = DateTime.Now;

					broker = model.Broker;
					action = "brokers";
				}
				else
				{
					var brokerDetail = new Broker()
					{
						SecondaryBrokerId = model.Company.Id,
						Name = model.Company.Name,
						DBA = model.Company.DBA,
						IsActive = true,
						AddressLine1 = model.Company.AddressLine1,
						AddressLine2 = model.Company.AddressLine2,
						City = model.Company.City,
						State = model.Company.State,
						PostalCode = model.Company.PostalCode,
						CountryId = 1,
						CreatedBy = CurrentUserAccountSroredData.UserId,
						CreatedDate = DateTime.Now,
						UpdatedBy = CurrentUserAccountSroredData.UserId,
						UpdatedDate = DateTime.Now
					};
					broker = brokerDetail;
					action = "brokersearch";

				}
				string url = "admin/broker/createbroker";
				var result = await _clientFactoryService.ExecutePostRequestAsync(url, broker);
				TempData["Message"] = (!string.IsNullOrEmpty(result) && Convert.ToInt32(result) > 0) ?
									  ToastResultMessage($"The Broker {broker.Name} has been successfully created.", true)
									  : ToastResultMessage("An error has occurred while executing your request", false);
				_model.Companies = new List<Company>();

				return RedirectToAction(action);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return View("Error");
			}
		}


		/// <summary>
		/// Brokers the search.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult BrokerSearch()
		{
			var model = new BrokerViewModel()
			{
				Companies = new List<Company>()
			};

			if (TempData["Message"] != null)
				model.DisplayMessage = TempData["Message"].ToString();

			return View(model);
		}

		/// <summary>
		/// Brokers the search.
		/// </summary>
		/// <param name="keyword">The keyword.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> BrokerSearch(IFormCollection field)
		{
			var model = new BrokerViewModel();
			var keyword = field["txtBrokerSearch"];
			try
			{
				var companies = new List<Company>();
				var url = $"admin/broker/search?brokerCompanyName={keyword}";
				var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(url);
				if (!string.IsNullOrEmpty(jsonString))
				{
					var brokerCompanies = JsonSerializer.Deserialize<List<BrokerCompany>>(jsonString,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

					if (brokerCompanies != null)
					{
						foreach (var brokerCompany in brokerCompanies)
						{
							companies.Add(new Company
							{
								Id = brokerCompany.BrokerId,
								Name = brokerCompany.BrokerName,
								DBA = brokerCompany.DBA,
								AccountEmail = brokerCompany.AccountEmail,
								AddressId = brokerCompany.AddressId,
								AddressLine1 = brokerCompany.AddressLine1,
								AddressLine2 = brokerCompany.AddressLine2,
								City = brokerCompany.City,
								State = brokerCompany.State,
								PostalCode = brokerCompany.PostalCode,
								Country = brokerCompany.Country,
								Role = "Broker"
							});
						}
						model.Companies = companies;
					}
					return View(model);
				}

				model.Companies = new List<Company>();
				model.DisplayMessage = $"Could not find any broker with the name of {keyword}";

				return View(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return View("Error");
			}
		}

		/// <summary>
		/// Edits the broker.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditBroker(BrokerViewModel model)
		{
			if (model.Broker == null)
			{
				ModelState.AddModelError("Broker", "The broker model coudn't be validated, please make sure all of the fields have values");
				return RedirectToAction("brokers");
			}

			try
			{
				model.Broker.UpdatedBy = CurrentUserAccountSroredData.UserId;
				model.Broker.UpdatedDate = DateTime.Now;

				var brokerRequestUrl = "admin/broker/updatebroker";
				var jsonStringResult = await _clientFactoryService.ExecutePostRequestAsync(brokerRequestUrl, model.Broker);

				if (!string.IsNullOrEmpty(jsonStringResult) && Convert.ToInt32(jsonStringResult) > 0)
				{
					TempData["Message"] = ToastResultMessage("The broker detail has been successfully updated", true);
				}
				else
				{
					TempData["Message"] = ToastResultMessage("Error has occurred while executing your request", false);
				}

				return RedirectToAction("brokers");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return View("Error");
			}
		}

		/// <summary>
		/// Brokers the contacts.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> BrokerContacts(string id)
		{
			var model = new BrokerViewModel();

			var decrypted = EncryptorHelper.Decrypt(id);
			if (String.IsNullOrEmpty(decrypted))
			{
				ModelState.AddModelError("BrokerContacts", "Broker id was not provided as a parameter");
				TempData["Message"] = ToastResultMessage("Error has occurred while executing your request", false);
				return RedirectToAction("brokers");
			}

			try
			{
				var splitId = decrypted.Split('|');
				int brokerId = Convert.ToInt32(splitId[0]);
				int docuboxBrokerId = Convert.ToInt32(string.IsNullOrEmpty(splitId[1]) ? 0 : splitId[1]);
				string brokerCompanyName = splitId[2];

				var lookup = await GetSystemLookupData();
				var titles = lookup.Titles;

				var brokerRequestUrl = $"admin/brokercontact/contacts?brokerId={brokerId}";
				var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(brokerRequestUrl);
				var brokercontacts = JsonSerializer.Deserialize<List<BrokerContact>>(jsonString,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				model.BrokerContacts = (brokercontacts != null && brokercontacts.Count > 0) ? brokercontacts : [];

				if (docuboxBrokerId > 0)
				{
					var docuboxBrokerRequestUrl = $"admin/broker/contacts?brokerId={docuboxBrokerId}";
					var docuboxJsonString = await _clientFactoryService.ExecuteGetRequestAsync(docuboxBrokerRequestUrl);
					var docuboxBrokercontacts = JsonSerializer.Deserialize<List<DocuboxBrokerContact>>(docuboxJsonString,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

					model.DocuboxBrokerContacts = (docuboxBrokercontacts != null && docuboxBrokercontacts.Count > 0) ?
						 docuboxBrokercontacts : [];
				}
				else
				{
					model.DocuboxBrokerContacts = [];
				}

				model.Titles = titles.Select(c => new Microsoft.AspNetCore.Mvc.Rendering
											   .SelectListItem
				{
					Value = c.TitleId.ToString(),
					Text = c.TitleName
				}).ToList();

				model.Broker = new Broker { BrokerId = brokerId, SecondaryBrokerId = docuboxBrokerId, Name=brokerCompanyName };
				model.UserAccount = new AccountContact { CompanyId = brokerId, ContactRole = "Broker", CompanyName= brokerCompanyName, CompanySecondaryId=docuboxBrokerId };
				if (TempData["Message"] != null)
					model.DisplayMessage = TempData["Message"].ToString();

                var _model = FilterBrokerContacts(model);

                return View(_model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return View("Error");
			}
		}

		/// <summary>
		/// Inserts the broker contact.
		/// </summary>
		/// <param name="contact">The contact.</param>
		/// <param name="identityId">The identity identifier.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">Error has occurred while executing 'InsertBrokerContact' function</exception>
		private async Task<int> InsertBrokerContact(AccountContact contact, string identityId)
		{
			var httpRequestBrokerContact = new BrokerContact
			{
				BrokerId = contact.CompanyId,
				IdentityId = identityId,
				SecondaryBrokerContactId = contact.SecondaryId,
				TitleId = contact.TitleId,
				FirstName = contact.FirstName,
				LastName = contact.LastName,
				Email = contact.Email,
				Phone = contact.Phone,
				IsActive = true,
				CreatedBy = CurrentUserAccountSroredData.UserId,
				CreatedDate = DateTime.Now,
				UpdatedBy = CurrentUserAccountSroredData.UserId,
				UpdatedDate = DateTime.Now
			};

			var brokerContactAddRequestUrl = "admin/brokercontact/addbrokercontact";
			var jsonString = await _clientFactoryService.ExecutePostRequestAsync(brokerContactAddRequestUrl, httpRequestBrokerContact);

			if (!string.IsNullOrEmpty(jsonString))
				return Convert.ToInt32(jsonString);
			throw new Exception("Error has occurred while executing 'InsertBrokerContact' function");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateBrokerContact(BrokerViewModel model)
		{
			try
			{
				var contactUser = await _userManager.FindByEmailAsync(model.BrokerContact.Email);
				if (contactUser == null)
				{
					ToastResultMessage($"Error has occurred while locating user account for {model.BrokerContact.Email}", false);
					return RedirectToAction("brokercontacts");
				}

				var httpRequestBrokerContact = new BrokerContact
				{
					BrokerContactId = model.BrokerContact.BrokerContactId,
					IdentityId = contactUser.Id.ToString(),
					TitleId = model.BrokerContact.TitleId,
					FirstName = model.BrokerContact.FirstName,
					LastName = model.BrokerContact.LastName,
					Email = model.BrokerContact.Email,
					Phone = model.BrokerContact.Phone,
					IsActive = model.BrokerContact.IsActive,
					UpdatedBy = CurrentUserAccountSroredData.UserId,
					UpdatedDate = DateTime.Now
				};

				var brokerContactAddRequestUrl = "admin/brokercontact/updatebrokercontact";
				var jsonString = await _clientFactoryService.ExecutePostRequestAsync(brokerContactAddRequestUrl, httpRequestBrokerContact);
				int result = (!string.IsNullOrEmpty(jsonString)) ? Convert.ToInt32(jsonString) : 0;

				TempData["Message"] = (result > 0)
										? ToastResultMessage("Broker contact has been successfully updated", true)
										: ToastResultMessage("Error has occurred while updating the broker contact", false);

				return RedirectToAction("brokercontacts", new { id = EncryptorHelper.Encrypt(model.Broker.BrokerId.ToString() + "|" + model.Broker.SecondaryBrokerId.ToString() + "|" + model.Broker.Name) });
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, ex.Message);
				return View("Error");
			}
		}


        private BrokerViewModel FilterBrokerContacts(BrokerViewModel model)
        {
            if (model.DocuboxBrokerContacts.Count == 0)
                return model;

            if (model.BrokerContacts.Count == 0)
                return model;

            foreach (var dbxContact in model.DocuboxBrokerContacts)
            {
                foreach (var contact in model.BrokerContacts)
                {
                    if (dbxContact.BrokerContactId == contact.SecondaryBrokerContactId)
                        dbxContact.InSystem = true;
                }
            }
            return model;
        }

        #endregion

        #region  Carrier Management

        /// <summary>
        /// Carrierses this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Carriers()
        {
            var model = new BrokerViewModel();
            try
            {
                var lookup = await GetSystemLookupData();
                var countries = lookup.Countries.ToList();
                if (countries == null)
                {
                    ModelState.AddModelError("Countries", "Error has occurred while getting countries from the api");
                    return View(model);
                }
                var carrierRequestUrl = "admin/carrier/getcarriers";
                var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(carrierRequestUrl);
                var carrierCompanies = JsonSerializer.Deserialize<List<Carrier>>(jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (carrierCompanies == null)
                {
                    ModelState.AddModelError("Carrier list", "Error has occurred while getting countries from the api");
                    model.Carriers = new List<Carrier>();
                    return View(model);
                }

                model.Carriers = carrierCompanies;
                model.Countries = countries.Select(c => new Microsoft.AspNetCore.Mvc.Rendering
                                               .SelectListItem
                {
                    Value = c.CountryId.ToString(),
                    Text = c.CountryName
                }).ToList();

                if (TempData["Message"] != null)
                    model.DisplayMessage = TempData["Message"]?.ToString();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View("Error");
            }
        }

        /// <summary>
        /// Creates the carrier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCarrier(BrokerViewModel model)
        {
            if (model.Company == null)
                TempData["Message"] = ToastResultMessage("Error has occurred while validating your request the carrier detail are missing", false);

            string action = string.Empty;

            var _model = new BrokerViewModel();
            try
            {
                var carrier = new Carrier();
                if (model.Carrier != null)
                {
                    model.Carrier.IsActive = true;
                    model.Carrier.CreatedBy = CurrentUserAccountSroredData.UserId;
                    model.Carrier.CreatedDate = DateTime.Now;
                    model.Carrier.UpdatedBy = CurrentUserAccountSroredData.UserId;
                    model.Carrier.UpdatedDate = DateTime.Now;

                    carrier = model.Carrier;
                    action = "carriers";
                }
                else
                {
                    var carrierDetail = new Carrier()
                    {
                        SecondaryCarrierId = model.Company.Id,
                        Name = model.Company.Name,
                        DBA = model.Company.DBA,
                        IsActive = true,
                        AddressLine1 = model.Company.AddressLine1,
                        AddressLine2 = model.Company.AddressLine2,
                        City = model.Company.City,
                        State = model.Company.State,
                        PostalCode = model.Company.PostalCode,
                        CountryId = 1,
                        CreatedBy = CurrentUserAccountSroredData.UserId,
                        CreatedDate = DateTime.Now,
                        UpdatedBy = CurrentUserAccountSroredData.UserId,
                        UpdatedDate = DateTime.Now
                    };
                    carrier = carrierDetail;
                    action = "carriersearch";
                }
                string url = "admin/carrier/createcarrier";
                var result = await _clientFactoryService.ExecutePostRequestAsync(url, carrier);
                TempData["Message"] = (!string.IsNullOrEmpty(result) && Convert.ToInt32(result) > 0) ?
                                      ToastResultMessage($"The Carrier {carrier.Name} has been successfully created.", true)
                                      : ToastResultMessage("An error has occurred while executing your request", false);
                _model.Companies = new List<Company>();

                return RedirectToAction(action);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View("Error");
            }
        }


        /// <summary>
        /// Carriers the search.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CarrierSearch()
        {
            var model = new BrokerViewModel()
            {
                Companies = new List<Company>()
            };

            if (TempData["Message"] != null)
                model.DisplayMessage = TempData["Message"].ToString();

            return View(model);
        }

        /// <summary>
        /// Carriers the search.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarrierSearch(IFormCollection field)
        {
            var model = new BrokerViewModel();
            var keyword = field["txtCarrierSearch"];
            try
            {
                var companies = new List<Company>();
                var url = $"admin/carrier/search?carrierCompanyName={keyword}";
                var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(url);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    var carrierCompanies = JsonSerializer.Deserialize<List<CarrierCompany>>(jsonString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (carrierCompanies != null)
                    {
                        foreach (var carrierCompany in carrierCompanies)
                        {
                            companies.Add(new Company
                            {
                                Id = carrierCompany.CarrierId,
                                Name = carrierCompany.CarrierName,
                                DBA = carrierCompany.DBA,
                                AccountEmail = carrierCompany.AccountEmail,
                                AddressId = carrierCompany.AddressId,
                                AddressLine1 = carrierCompany.AddressLine1,
                                AddressLine2 = carrierCompany.AddressLine2,
                                City = carrierCompany.City,
                                State = carrierCompany.State,
                                PostalCode = carrierCompany.PostalCode,
                                Country = carrierCompany.Country,
                                Role = "Carrier"
                            });
                        }
                        model.Companies = companies;
                    }
                    return View(model);
                }

                model.Companies = new List<Company>();
                model.DisplayMessage = $"Could not find any carrier with the name of {keyword}";

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View("Error");
            }
        }

        /// <summary>
        /// Edits the carrier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCarrier(BrokerViewModel model)
        {
            if (model.Carrier == null)
            {
                ModelState.AddModelError("Carrier", "The carrier model coudn't be validated, please make sure all of the fields have values");
                return RedirectToAction("carriers");
            }

            try
            {
                model.Carrier.UpdatedBy = CurrentUserAccountSroredData.UserId;
                model.Carrier.UpdatedDate = DateTime.Now;

                var carrierRequestUrl = "admin/carrier/updatecarrier";
                var jsonStringResult = await _clientFactoryService.ExecutePostRequestAsync(carrierRequestUrl, model.Carrier);

                if (!string.IsNullOrEmpty(jsonStringResult) && Convert.ToInt32(jsonStringResult) > 0)
                {
                    TempData["Message"] = ToastResultMessage("The carrier detail has been successfully updated", true);
                }
                else
                {
                    TempData["Message"] = ToastResultMessage("Error has occurred while executing your request", false);
                }

                return RedirectToAction("carriers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        /// <summary>
        /// Carriers the contacts.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CarrierContacts(string id)
        {
            var model = new BrokerViewModel();

            var decrypted = EncryptorHelper.Decrypt(id);
            if (String.IsNullOrEmpty(decrypted))
            {
                ModelState.AddModelError("CarrierContacts", "Carrier id was not provided as a parameter");
                TempData["Message"] = ToastResultMessage("Error has occurred while executing your request", false);
                return RedirectToAction("carriers");
            }

            try
            {
                var splitId = decrypted.Split('|');
                int carrierId = Convert.ToInt32(splitId[0]);
                int docuboxCarrierId = Convert.ToInt32(string.IsNullOrEmpty(splitId[1]) ? 0 : splitId[1]);
				string carrierName = splitId[2];

                var lookup = await GetSystemLookupData();
                var titles = lookup.Titles;

                var carrierRequestUrl = $"admin/carriercontact/contacts?carrierId={carrierId}";
                var jsonString = await _clientFactoryService.ExecuteGetRequestAsync(carrierRequestUrl);
                var carriercontacts = JsonSerializer.Deserialize<List<CarrierContact>>(jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                model.CarrierContacts = (carriercontacts != null && carriercontacts.Count > 0) ? carriercontacts : [];

                if (docuboxCarrierId > 0)
                {
                    var docuboxCarrierRequestUrl = $"admin/carrier/contacts?carrierId={docuboxCarrierId}";
                    var docuboxJsonString = await _clientFactoryService.ExecuteGetRequestAsync(docuboxCarrierRequestUrl);
                    var docuboxCarriercontacts = JsonSerializer.Deserialize<List<DocuboxCarrierContact>>(docuboxJsonString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    model.DocuboxCarrierContacts = (docuboxCarriercontacts != null && docuboxCarriercontacts.Count > 0) ?
                         docuboxCarriercontacts : [];
                }
                else
                {
                    model.DocuboxCarrierContacts = [];
                }

                model.Titles = titles.Select(c => new Microsoft.AspNetCore.Mvc.Rendering
                                               .SelectListItem
                {
                    Value = c.TitleId.ToString(),
                    Text = c.TitleName
                }).ToList();

                model.Carrier = new Carrier { CarrierId = carrierId, SecondaryCarrierId = docuboxCarrierId, Name=carrierName };
                model.UserAccount = new AccountContact { CompanyId = carrierId, ContactRole = "Carrier", CompanySecondaryId=docuboxCarrierId };
                if (TempData["Message"] != null)
                    model.DisplayMessage = TempData["Message"].ToString();

                var _model = FilterCarrierContacts(model);
                return View(_model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        /// <summary>
        /// Filters the carrier contacts.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private BrokerViewModel FilterCarrierContacts(BrokerViewModel model)
        {
            if (model.DocuboxCarrierContacts.Count == 0)
                return model;

            if (model.CarrierContacts.Count == 0)
                return model;

            foreach (var dbxContact in model.DocuboxCarrierContacts)
            {
                foreach (var contact in model.CarrierContacts)
                {
                    if (dbxContact.CarrierContactId == contact.SecondaryCarrierContactId)
                        dbxContact.InSystem = true;
                }
            }
            return model;
        }

        /// <summary>
        /// Inserts the carrier contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <param name="identityId">The identity identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while executing 'InsertCarrierContact' function</exception>
        private async Task<int> InsertCarrierContact(AccountContact contact, string identityId)
        {
            var httpRequestCarrierContact = new CarrierContact
            {
                CarrierId = contact.CompanyId,
                IdentityId = identityId,
				SecondaryCarrierContactId = (contact.SecondaryId > 0)?contact.SecondaryId : null,
                TitleId = contact.TitleId,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone,
                IsActive = true,
                CreatedBy = CurrentUserAccountSroredData.UserId,
                CreatedDate = DateTime.Now,
                UpdatedBy = CurrentUserAccountSroredData.UserId,
                UpdatedDate = DateTime.Now
            };

            var carrierContactAddRequestUrl = "admin/carriercontact/addcarriercontact";
            var jsonString = await _clientFactoryService.ExecutePostRequestAsync(carrierContactAddRequestUrl, httpRequestCarrierContact);

            if (!string.IsNullOrEmpty(jsonString))
                return Convert.ToInt32(jsonString);
            throw new Exception("Error has occurred while executing 'InsertCarrierContact' function");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarrierContact(BrokerViewModel model)
        {
            try
            {
                var contactUser = await _userManager.FindByEmailAsync(model.CarrierContact.Email);
                if (contactUser == null)
                {
                    ToastResultMessage($"Error has occurred while locating user account for {model.CarrierContact.Email}", false);
                    return RedirectToAction("agentcontacts");
                }

                var httpRequestCarrierContact = new CarrierContact
                {
                    CarrierContactId = model.CarrierContact.CarrierContactId,
                    IdentityId = contactUser.Id.ToString(),
                    TitleId = model.CarrierContact.TitleId,
                    FirstName = model.CarrierContact.FirstName,
                    LastName = model.CarrierContact.LastName,
                    Email = model.CarrierContact.Email,
                    Phone = model.CarrierContact.Phone,
                    IsActive = model.CarrierContact.IsActive,
                    UpdatedBy = CurrentUserAccountSroredData.UserId,
                    UpdatedDate = DateTime.Now
                };

                var carrierContactAddRequestUrl = "admin/carriercontact/updatecarriercontact";
                var jsonString = await _clientFactoryService.ExecutePostRequestAsync(carrierContactAddRequestUrl, httpRequestCarrierContact);
                int result = (!string.IsNullOrEmpty(jsonString)) ? Convert.ToInt32(jsonString) : 0;

                TempData["Message"] = (result > 0)
                                        ? ToastResultMessage("Carrier contact has been successfully updated", true)
                                        : ToastResultMessage("Error has occurred while updating the carrier contact", false);

                return RedirectToAction("carriercontacts", new { id = EncryptorHelper.Encrypt(model.Carrier.CarrierId.ToString() + "|" + model.Carrier.SecondaryCarrierId.ToString() + "|" + model.Carrier.Name) });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                return View("Error");
            }
        }

		#endregion

		#region Account Settings

		/// <summary>
		/// Accounts the settings.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> AccountSettings()
		{
			var model = new BrokerViewModel();

			try
			{
				var lookup = await GetSystemLookupData();
				var titles = lookup.Titles;

				model.UserAccounts = await _identityService.GetAllUserAccountDetails();			
				
				model.Titles = titles.Select(c => new Microsoft.AspNetCore.Mvc.Rendering
							   .SelectListItem
				{
					Value = c.TitleId.ToString(),
					Text = c.TitleName
				}).ToList();

				if (TempData["Message"] != null)
					model.DisplayMessage = TempData["Message"].ToString();

				return View(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while fetching user accounts.");
				return View("Error");
			}
		}

        /// <summary>
        /// Updates the user account.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserAccount(BrokerViewModel model)
        {
            if (model.UserAccount == null)
            {
                TempData["Message"] = ToastResultMessage("Invalid user data provided. Please ensure all fields are completed correctly.", false);
                return RedirectToAction("accountsettings");
            }

            try
            {
                var result = await _identityService.UpdateUserAccount(model.UserAccount);
                TempData["Message"] = (result) 
					                   ? ToastResultMessage("User details have been successfully updated.", true)
									   : ToastResultMessage("Error has occurred while updating user account, please inform system admin if this continues.", false);
				               
                return RedirectToAction("accountsettings");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        /// <summary>
        /// Resets the user password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetUserPassword(BrokerViewModel model)
		{
			if (model.UserAccount == null)
			{
				TempData["Message"] = ToastResultMessage("The password reset model couldn't be validated. Please ensure all required fields are provided.", false);
				return RedirectToAction("accountsettings");
			}

			try
			{
				var result = await _identityService.ResetUserPassword(model.UserAccount);

				TempData["Message"] = (result)
											 ? ToastResultMessage("The user password has been successfuly reset.", true)
											 : ToastResultMessage("Error has occurred while executing the password reset.", false);

				return RedirectToAction("accountsettings");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return View("Error");
			}
		}
        #endregion
    }
}

