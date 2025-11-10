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
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Unable to load dashboard data.");
            }

            return View(model);
        }
    }
}
