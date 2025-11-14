using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BankManagementSystem.Client.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericHttpClient _client;
        private readonly IToastNotification _nToastNotify;

        public EmployeeController(IGenericHttpClient client,IToastNotification nToastNotify)
        {
            _client = client;
            _nToastNotify = nToastNotify;
        }

        public async Task<ActionResult> Index()
        {
            List<EmployeeResponse> employees = new List<EmployeeResponse>();
            employees = await _client.GetAsync<List<EmployeeResponse>>(ApiConstant.GetAllEmployees);
            return View(employees);
        }

        public async Task<ActionResult> List(string? name, string? phone)
        {
            List<EmployeeResponse> employees = new();

            try
            {
                var queryParams = $"?name={name}&phone={phone}";
                employees = await _client.GetAsync<List<EmployeeResponse>>($"{ApiConstant.GetAllEmployeesBySearch}{queryParams}");
            }
            catch
            {
                _nToastNotify.AddErrorToastMessage("Fetching failed");
            }

            return View("Index", employees);
        }
        public async Task<ActionResult> Details(int id)
        {
            EmployeeResponse employee = new EmployeeResponse();
            employee = await _client.GetAsync<EmployeeResponse>($"{ApiConstant.GetEmployeeById}?id={id}");
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<ActionResult> Create(EmployeeResponse employeeResponse)
        {
            try
            {
                await _client.PostAsync<EmployeeResponse>(ApiConstant.AddEmployee, employeeResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            EmployeeResponse employee = new EmployeeResponse();
            employee = await _client.GetAsync<EmployeeResponse>($"{ApiConstant.GetEmployeeById}?id={id}");
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<ActionResult> ConfirmEdit(EmployeeResponse employeeResponse)
        {
            try
            {
                await _client.PutAsync<EmployeeResponse>(ApiConstant.UpdateEmployee, employeeResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            EmployeeResponse employee = new EmployeeResponse();
            employee = await _client.GetAsync<EmployeeResponse>($"{ApiConstant.GetEmployeeById}?id={id}");
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            try
            {
                EmployeeResponse employee = new EmployeeResponse();
                employee = await _client.DeleteAsync<EmployeeResponse>($"{ApiConstant.DeleteEmployee}?id={id}");
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
