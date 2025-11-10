using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Client.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IGenericHttpClient _client;

        public TransactionController(IGenericHttpClient client)
        {
            _client = client;
        }

        public async Task<ActionResult> Index()
        {
            List<TransactionResponse> transactions = new List<TransactionResponse>();
            transactions = await _client.GetAsync<List<TransactionResponse>>(ApiConstant.GetAllTransactions);
            return View(transactions);
        }

        public async Task<ActionResult> Details(int id)
        {
            TransactionResponse transaction = new TransactionResponse();
            transaction = await _client.GetAsync<TransactionResponse>($"{ApiConstant.GetTransactionById}?id={id}");
            return View(transaction);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<ActionResult> Create(TransactionResponse transactionResponse)
        {
            try
            {
                await _client.PostAsync<TransactionResponse>(ApiConstant.AddTransaction, transactionResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            TransactionResponse transaction = new TransactionResponse();
            transaction = await _client.GetAsync<TransactionResponse>($"{ApiConstant.GetTransactionById}?id={id}");
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<ActionResult> ConfirmEdit(TransactionResponse transactionResponse)
        {
            try
            {
                await _client.PutAsync<TransactionResponse>(ApiConstant.UpdateTransaction, transactionResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            TransactionResponse transaction = new TransactionResponse();
            transaction = await _client.GetAsync<TransactionResponse>($"{ApiConstant.GetTransactionById}?id={id}");
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            try
            {
                TransactionResponse transaction = new TransactionResponse();
                transaction = await _client.DeleteAsync<TransactionResponse>($"{ApiConstant.DeleteTransaction}?id={id}");
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
