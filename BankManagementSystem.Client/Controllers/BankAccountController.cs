using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Client.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly IGenericHttpClient _client;

        public BankAccountController(IGenericHttpClient client)
        {
            _client = client;
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

        public ActionResult Create()
        {
            return View();
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
    }
}
