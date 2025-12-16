using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BankManagementSystem.Client.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IGenericHttpClient _client;
        private readonly IToastNotification _nToastNotify;

        public TransactionController(IGenericHttpClient client,IToastNotification nToastNotify)
        {
            _client = client;
            _nToastNotify = nToastNotify;
        }

        public async Task<ActionResult> Index()
        {
            List<TransactionResponse> transactions = new List<TransactionResponse>();
            transactions = await _client.GetAsync<List<TransactionResponse>>(ApiConstant.GetAllTransactions);
            return View(transactions);
        }
        #region CRUD
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
        #endregion

        #region Deposit

        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            var customerId = int.Parse(User.FindFirst("CustomerId")?.Value ?? "0");
            var customer=await _client.GetAsync<CustomerResponse>($"{ApiConstant.GetCustomerById}?id={customerId}");
            if (customer.Status == "Rejected")
            {
                _nToastNotify.AddErrorToastMessage("Your status is set to rejected. Contact Manager");
                return RedirectToAction("Index","Dashboard");
            }
            var accounts = await _client.GetAsync<List<AccountResponse>>(
                $"{ApiConstant.GetAccountsByCustomer}?customerId={customerId}"
            );

            return View(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(TransactionRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);          
            try
            {
                if (model.Amount <= 0)
                {
                    _nToastNotify.AddErrorToastMessage("Amoount must be greater than zero");
                }
                await _client.PostAsync<object>(ApiConstant.Deposit, model);
                _nToastNotify.AddSuccessToastMessage("Deposit request submitted");
                return RedirectToAction("Deposit");
            }
            catch (HttpRequestException)
            {
                _nToastNotify.AddErrorToastMessage("Deposit request submission failed");
                return RedirectToAction("Deposit");
            }
            catch (Exception)
            {
                _nToastNotify.AddErrorToastMessage("Deposit request submission failed");
                return RedirectToAction("Deposit");
            }
        }

        #endregion

        #region Withdraw
        [HttpGet]
        public async Task<IActionResult> Withdraw()
        {
            var customerId = int.Parse(User.FindFirst("CustomerId")?.Value ?? "0");
            var customer = await _client.GetAsync<CustomerResponse>($"{ApiConstant.GetCustomerById}?id={customerId}");
            if (customer.Status == "Rejected")
            {
                _nToastNotify.AddErrorToastMessage("Your status is set to rejected. Contact Manager!");
                return RedirectToAction("Index","Dashboard");
            }
            var accounts = await _client.GetAsync<List<AccountResponse>>(
                $"{ApiConstant.GetAccountsByCustomer}?customerId={customerId}"
            );

            return View(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(TransactionRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                if(model.Amount <= 0)
                {
                    _nToastNotify.AddErrorToastMessage("Amount must be greater than zero");
                }
                await _client.PostAsync<object>(ApiConstant.Withdraw, model);
                _nToastNotify.AddSuccessToastMessage("Withdraw request submitted");
                return RedirectToAction("Withdraw");
            }
            catch (HttpRequestException)
            {
                _nToastNotify.AddErrorToastMessage("Withdraw request submission failed");
                return RedirectToAction("Withdraw");
            }
            catch (Exception)
            {
                _nToastNotify.AddErrorToastMessage("Withdraw request submission failed");
                return RedirectToAction("Withdraw");
            }
        }
        #endregion

        #region Transfer
        [HttpGet]
        public async Task<IActionResult> Transfer()
        {
            var customerId = int.Parse(User.FindFirst("CustomerId")?.Value ?? "0");
            var customer = await _client.GetAsync<CustomerResponse>($"{ApiConstant.GetCustomerById}?id={customerId}");
            if (customer.Status == "Rejected")
            {
                _nToastNotify.AddErrorToastMessage("Your status is set to rejected. Contact Manager!");
                return RedirectToAction("Index","Dashboard");
            }
            var accounts = await _client.GetAsync<List<AccountResponse>>(
                $"{ApiConstant.GetAccountsByCustomer}?customerId={customerId}"
            );

            return View(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _client.PostAsync<object>(ApiConstant.Transfer, model);
                _nToastNotify.AddSuccessToastMessage("Transfer successfull");
                return RedirectToAction("Transfer");
            }
            catch (HttpRequestException e)
            {
                _nToastNotify.AddErrorToastMessage("Money transfer failed "+e.Message);
                return RedirectToAction("Transfer");
            }
            catch (Exception e)
            {
                _nToastNotify.AddErrorToastMessage("Money transfer failed "+e.Message);
                return RedirectToAction("Transfer");
            }
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetTransactionByCustomer()
        {
            var customerId = int.Parse(User.FindFirst("CustomerId")?.Value ?? "0");
            var customer = await _client.GetAsync<CustomerResponse>($"{ApiConstant.GetCustomerById}?id={customerId}");
            if (customer.Status == "Rejected")
            {
                _nToastNotify.AddErrorToastMessage("Your status is set to rejected. Contact Manager!");
                return RedirectToAction("Index","Dashboard");
            }
            var transactions = await _client.GetAsync<List<TransactionResponse>>(
                $"{ApiConstant.GetTransactionsByCustomerId}?customerId={customerId}"
            );

            return View(transactions);
        }

        [HttpGet]
        public async Task<IActionResult> List(string? status, DateTime? date)
        {
            List<TransactionResponse> transactions = new();

            try
            {
                string queryParams = $"?status={status}";
                if (date.HasValue)
                {
                    string formattedDate = date.Value.ToString("yyyy-MM-dd");
                    queryParams += $"&date={formattedDate}";
                }

                transactions = await _client.GetAsync<List<TransactionResponse>>(
                    $"{ApiConstant.GetTransactionsBySearch}{queryParams}"
                );
            }
            catch (Exception)
            {

                _nToastNotify.AddErrorToastMessage("Fetching failed");
            }

            return View("Index", transactions);
        }

    }
}
