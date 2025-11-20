using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using BankManagementSystem.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Client.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IGenericHttpClient _client;

        public DashboardController(IGenericHttpClient client)
        {
            _client = client;
        }

        // GET: Dashboard/Index
        public async Task<IActionResult> Index()
        {
            DashboardViewModel model = new DashboardViewModel();

            try
            {
                model = await _client.GetAsync<DashboardViewModel>(ApiConstant.GetDashboard);
                if (User.IsInRole("Customer"))
                {
                    var customerId = int.Parse(User.FindFirst("CustomerId")?.Value ?? "0");
                    if (customerId > 0)
                    {
                        var transactions = await _client.GetAsync<List<TransactionResponse>>(
                            $"{ApiConstant.GetTransactionsByCustomerId}?customerId={customerId}"
                        );
                        model.Transactions = transactions;

                        var accounts = await _client.GetAsync<List<AccountResponse>>(
                            $"{ApiConstant.GetAccountsByCustomer}?customerId={customerId}"
                        );
                        model.Accounts = accounts;
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Unable to load dashboard data.");
            }

            return View(model);
        }
    }
}
