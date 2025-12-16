using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BankManagementSystem.Client.Controllers
{
    public class AdministrativeController : Controller
    {
        private readonly IGenericHttpClient _client;
        private readonly IToastNotification _nToastNotify;

        public AdministrativeController(IGenericHttpClient client,IToastNotification nToastNotify)
        {
            _client = client;
            _nToastNotify = nToastNotify;
        }
        [HttpPost]
        public async Task<IActionResult> ApproveCustomer(int id)
        {
            try
            {
                await _client.PutAsync<object>($"{ApiConstant.ApproveCustomer}?id={id}", new {});
                var accountResponse= await _client.PostAsync<UserResponse>($"{ApiConstant.CreateCustomerAccount}?customerId={id}", new { });

                return Json (accountResponse);
            }
            catch (HttpRequestException ex)
            {
                _nToastNotify.AddErrorToastMessage($"Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _nToastNotify.AddErrorToastMessage($"Unexpected error: {ex.Message}");
            }

            return RedirectToAction("Index", "Customer");
        }
        [HttpPost]
        public async Task<IActionResult> RejectCustomer(int id)
        {
            try
            {
                var response=await _client.PutAsync<object>($"{ApiConstant.RejectCustomer}?id={id}", new { });
                _nToastNotify.AddAlertToastMessage("Rejected uccessfully");
                return Json(response);
            }
            catch (HttpRequestException ex)
            {
                _nToastNotify.AddErrorToastMessage($"Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _nToastNotify.AddErrorToastMessage($"Unexpected error: {ex.Message}");
            }

            return RedirectToAction("Index", "Customer");
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAccount(int accountId)
        {
            try
            {
                var response=await _client.PutAsync<Object>($"{ApiConstant.ApproveAccount}?accountId={accountId}", new { });
                _nToastNotify.AddSuccessToastMessage("Account request approved successfully");
            }
            catch (HttpRequestException ex)
            {
                _nToastNotify.AddErrorToastMessage($"Request error: {ex.Message}");

            }
            catch (Exception ex)
            {
                _nToastNotify.AddErrorToastMessage($"Unexpected error: {ex.Message}");

            }
            return RedirectToAction("Index", "BankAccount");
        }
        [HttpPost]
        public async Task<IActionResult> RejectAccount(int accountId)
        {
            try
            {
                var response=await _client.PutAsync<Object>($"{ApiConstant.RejectAccount}?accountId={accountId}", new { });
                _nToastNotify.AddSuccessToastMessage("Account request rejected successfully");
            }
            catch (HttpRequestException ex)
            {
                _nToastNotify.AddErrorToastMessage($"Request error: {ex.Message}");

            }
            catch (Exception ex)
            {
                _nToastNotify.AddErrorToastMessage($"Unexpected error: {ex.Message}");

            }
            return RedirectToAction("Index", "BankAccount");
        }
        [HttpPost]
        public async Task<IActionResult> BlockCustomer(int customerId)
        {
            try
            {
                await _client.PostAsync<Object>($"{ApiConstant.BlockCustomer}?id={customerId}", new { });
                _nToastNotify.AddSuccessToastMessage("Customer blocked successfully");
            }
            catch (HttpRequestException ex)
            {
                _nToastNotify.AddErrorToastMessage($"Request error: {ex.Message}");

            }
            catch (Exception ex)
            {
                _nToastNotify.AddErrorToastMessage($"Unexpected error: {ex.Message}");

            }
            return RedirectToAction("Index", "Customer");
        }
        [HttpPost]
        public async Task<IActionResult> UnBlockCustomer(int customerId)
        {
            try
            {
                await _client.PostAsync<Object>($"{ApiConstant.UnBlockCustomer}?id={customerId}", new { });
                _nToastNotify.AddSuccessToastMessage("Customer unblocked successfully");
            }
            catch (HttpRequestException ex)
            {
                _nToastNotify.AddErrorToastMessage($"Request error: {ex.Message}");

            }
            catch (Exception ex)
            {
                _nToastNotify.AddErrorToastMessage($"Unexpected error: {ex.Message}");

            }
            return RedirectToAction("Index", "Customer");
        }

        [HttpPost]
        public async Task<IActionResult> ApproveDeposit(int transactionId)
        {
            try
            {
                await _client.PutAsync<object>($"{ApiConstant.ApproveDeposit}?transactionId={transactionId}", new { });
                _nToastNotify.AddSuccessToastMessage("Deposit approved successfully.");
            }
            catch (HttpRequestException ex)
            {
                _nToastNotify.AddErrorToastMessage($"API error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _nToastNotify.AddErrorToastMessage($"Unexpected error: {ex.Message}");
            }

            return RedirectToAction("Index","Transaction");
        }
        [HttpPost]
        public async Task<IActionResult> ApproveWithdraw(int transactionId)
        {
            try
            {
                await _client.PutAsync<object>($"{ApiConstant.ApproveWithdraw}?transactionId={transactionId}", new { });
                _nToastNotify.AddSuccessToastMessage("Withdraw approved successfully.");
            }
            catch (HttpRequestException ex)
            {
                _nToastNotify.AddErrorToastMessage($"API error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _nToastNotify.AddErrorToastMessage($"Unexpected error: {ex.Message}");
            }

            return RedirectToAction("Index","Transaction");
        }

        #region Create Employee

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var response = await _client.PostAsync<dynamic>(ApiConstant.AddEmployee, model);

                _nToastNotify.AddSuccessToastMessage("Employee account created successfully!");

            }
            catch (HttpRequestException)
            {
                _nToastNotify.AddErrorToastMessage("Error adding employee. Please try again.");
            }
            catch (Exception)
            {
                _nToastNotify.AddErrorToastMessage("Unexpected error occurred.");
            }
            return RedirectToAction("Index", "Employee");
        }
        #endregion

    }
}
