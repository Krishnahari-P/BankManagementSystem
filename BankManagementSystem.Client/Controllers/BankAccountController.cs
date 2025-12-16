using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BankManagementSystem.Client.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly IGenericHttpClient _client;
        private readonly IToastNotification _nToastNotify;

        public BankAccountController(IGenericHttpClient client,IToastNotification nToastNotify)
        {
            _client = client;
            _nToastNotify = nToastNotify;
        }

        public async Task<ActionResult> Index()
        {
            List<AccountResponse> accounts = new List<AccountResponse>();
            accounts = await _client.GetAsync<List<AccountResponse>>(ApiConstant.GetAllAccounts);
            return View(accounts);
        }

        public async Task<ActionResult> Details(int id)
        {
            AccountResponse account = new AccountResponse();
            account = await _client.GetAsync<AccountResponse>($"{ApiConstant.GetAccountById}?id={id}");
            return View(account);
        }

        public async Task<IActionResult> List(string? accountNumber)
        {
            try
            {
                var account = await _client.GetAsync<AccountResponse>(
                    $"{ApiConstant.GetAccountByAccountNumber}?accountNumber={accountNumber}"
                );

                if (account != null)
                    return View("Index", new List<AccountResponse> { account });
                else
                    _nToastNotify.AddErrorToastMessage("Account not found");
            }
            catch
            {
                _nToastNotify.AddErrorToastMessage("Fetching failed");
            }

            return View("Index", new List<AccountResponse>());
        }


        public async Task<IActionResult> Create()
        {
            var customerId = int.Parse(User.FindFirst("CustomerId")?.Value ?? "0");
            if (customerId == 0)
                return BadRequest("Invalid or missing customer information.");
            var customer = await _client.GetAsync<CustomerResponse>($"{ApiConstant.GetCustomerById}?id={customerId}");
            if (customer.Status == "Rejected")
            {
                _nToastNotify.AddErrorToastMessage("Your status is set to rejected. Contact Manager");
                return RedirectToAction("Index", "Dashboard");
            }
            var account = new AccountRequest();
            account.AccountTypeList=await GetAccountTypeList();
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> RequestAccount([FromBody] AccountRequest model)
        {
            try
            {
                var customerId = int.Parse(User.FindFirst("CustomerId")?.Value ?? "0");               
                model.CustomerId = customerId;
                var response = await _client.PostAsync<object>(ApiConstant.RequestAccount, model);
                _nToastNotify.AddSuccessToastMessage("Account request submitted successfully!");
                return Json(new { message = "Account request submitted successfully!" });
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"API error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<ActionResult> Create(AccountResponse accountResponse)
        {
            try
            {              
                await _client.PostAsync<AccountResponse>(ApiConstant.AddAccount, accountResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            AccountResponse account = new AccountResponse();
            account = await _client.GetAsync<AccountResponse>($"{ApiConstant.GetAccountById}?id={id}");
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<ActionResult> ConfirmEdit(AccountResponse accountResponse)
        {
            try
            {
                await _client.PutAsync<AccountResponse>(ApiConstant.UpdateAccount, accountResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            AccountResponse account = new AccountResponse();
            account = await _client.GetAsync<AccountResponse>($"{ApiConstant.GetAccountById}?id={id}");
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            try
            {
                AccountResponse account = new AccountResponse();
                account = await _client.DeleteAsync<AccountResponse>($"{ApiConstant.DeleteAccount}?id={id}");
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetAccountTypeList()
        {
            var accountTypes = await _client.GetAsync<List<AccountTypeResponse>>(ApiConstant.GetAllAccountTypes);

            return accountTypes.Select(x => new SelectListItem
            {
                Value = x.AccountTypeId.ToString(),
                Text = x.TypeName
            }).ToList();
        }
    }
}
