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

        [HttpPut("ApproveCustomer")]
        public async Task<IActionResult> ApproveCustomer(int id)
        {
            try
            {
                await _client.PutAsync<object>($"{ApiConstant.ApproveCustomer}?id={id}",null);
                _nToastNotify.AddSuccessToastMessage("Customer approved successfully");
            }
            catch
            {
                _nToastNotify.AddErrorToastMessage("Error approving customer");
            }

            return RedirectToAction("Index", "Customer");
        }

        [HttpPost("CreateCustomerAccount")]
        public async Task<IActionResult> CreateCustomerAccount(int customerId)
        {
            try
            {
                await _client.PostAsync<object>($"{ApiConstant.CreateCustomerAccount}?customerId={customerId}",null);
                _nToastNotify.AddSuccessToastMessage("Customer account created successfully");
            }
            catch
            {
                _nToastNotify.AddErrorToastMessage("Error creating customer account");
            }

            return RedirectToAction("Index", "Customer");
        }
    }
}
