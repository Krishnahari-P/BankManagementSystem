using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using BankManagementSystem.Client.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BankManagementSystem.Client.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IGenericHttpClient _client;
        private readonly IToastNotification _nToastNotify;

        public CustomerController(IGenericHttpClient client, IToastNotification nToastNotify)
        {
            _client = client;
            _nToastNotify = nToastNotify;
        }

        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            List<CustomerResponse> customers = new List<CustomerResponse>();
            customers = await _client.GetAsync<List<CustomerResponse>>(ApiConstant.GetAllCustomers);
            return View(customers);
        }
        //public IActionResult List()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public async Task<IActionResult> ListJson(string? customerName, string? aadhar, string? status, string? phone)
        //{
        //    string apiUrl = $"{ApiConstant.GetCustomerBySearch}?name={customerName}&aadhar={aadhar}&status={status}&phone={phone}";
        //    var customers = await _client.GetAsync<List<CustomerResponse>>(apiUrl);
        //    return Json(new { data = customers });
        //}
        public async Task<ActionResult> List(string? customerName, string? aadhar, string? status, string? phone)
        {
            List<CustomerResponse> customers = new();

            try
            {
                var queryParams = $"?customerName={customerName}&aadhar={aadhar}&status={status}&phone={phone}";
                customers = await _client.GetAsync<List<CustomerResponse>>($"{ApiConstant.GetCustomerBySearch}{queryParams}");
            }
            catch
            {
                _nToastNotify.AddErrorToastMessage("Fetching failed");
            }

            return View("Index", customers);
        }

        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            CustomerResponse customer = new CustomerResponse();
            customer = await _client.GetAsync<CustomerResponse>($"{ApiConstant.GetCustomerById}?id={id}");
            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<ActionResult> Create(CustomerResponse customerResponse)
        {
            try
            {
                await _client.PostAsync<CustomerResponse>(ApiConstant.AddCustomer, customerResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CustomerResponse customer = new CustomerResponse();
            customer = await _client.GetAsync<CustomerResponse>($"{ApiConstant.GetCustomerById}?id={id}");
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<ActionResult> ConfirmEdit(CustomerResponse customerResponse)
        {
            try
            {
                await _client.PutAsync<CustomerResponse>(ApiConstant.UpdateCustomer, customerResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            CustomerResponse customer = new CustomerResponse();
            customer = await _client.GetAsync<CustomerResponse>($"{ApiConstant.GetCustomerById}?id={id}");
            return View(customer);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            try
            {
                CustomerResponse customer = new CustomerResponse();
                customer = await _client.DeleteAsync<CustomerResponse>($"{ApiConstant.DeleteCustomer}?id={id}");
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public async Task<IActionResult> Register(CustomerRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _client.PostAsync<CustomerRegistrationViewModel>(ApiConstant.CustomerRegistration, model);
                _nToastNotify.AddSuccessToastMessage("Registration Successfull");
                return RedirectToAction("Success");
            }
            catch
            {
                _nToastNotify.AddErrorToastMessage("Registration Unsuccessfull");
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
