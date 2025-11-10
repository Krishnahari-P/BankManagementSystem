using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Client.Controllers
{
    public class AccountTypeController : Controller
    {
        private readonly IGenericHttpClient _client;
        public AccountTypeController(IGenericHttpClient client)
        {
            _client = client;
        }

        public async Task<ActionResult> Index()
        {
            List<AccountTypeResponse> accountTypes = new List<AccountTypeResponse>();
            accountTypes = await _client.GetAsync<List<AccountTypeResponse>>(ApiConstant.GetAllAccountTypes);
            return View(accountTypes);
        }

        public async Task<ActionResult> Details(int id)
        {
            AccountTypeResponse accountType = new AccountTypeResponse();
            accountType = await _client.GetAsync<AccountTypeResponse>($"{ApiConstant.GetAccountTypeById}?id={id}");
            return View(accountType);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<ActionResult> Create(AccountTypeResponse accountTypeResponse)
        {
            try
            {
                await _client.PostAsync<AccountTypeResponse>(ApiConstant.AddAccountType, accountTypeResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            AccountTypeResponse accountType = new AccountTypeResponse();
            accountType = await _client.GetAsync<AccountTypeResponse>($"{ApiConstant.GetAccountTypeById}?id={id}");
            return View(accountType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<ActionResult> ConfirmEdit(AccountTypeResponse accountTypeResponse)
        {
            try
            {
                await _client.PutAsync<AccountTypeResponse>(ApiConstant.UpdateAccountType, accountTypeResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            AccountTypeResponse accountType = new AccountTypeResponse();
            accountType = await _client.GetAsync<AccountTypeResponse>($"{ApiConstant.GetAccountTypeById}?id={id}");
            return View(accountType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            try
            {
                AccountTypeResponse accountType = new AccountTypeResponse();
                accountType = await _client.DeleteAsync<AccountTypeResponse>($"{ApiConstant.DeleteAccountType}?id={id}");
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
