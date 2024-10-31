using ManeroBlazorAppv2.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ManeroBlazorAppv2.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public PaymentService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string> CreateNewPayment(PaymentModel newPayment)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                newPayment.UserId = userId;

                if (!string.IsNullOrEmpty(newPayment.PaymentCompany) && !string.IsNullOrEmpty(newPayment.CardHolderName) && !string.IsNullOrEmpty(newPayment.CardNumber) && !string.IsNullOrEmpty(newPayment.ExpiryDate) && !string.IsNullOrEmpty(newPayment.CVV))
                {
                    var response = await _httpClient.PostAsJsonAsync("https://paymentprovider-manero.azurewebsites.net/api/CreateCard?code=QVDo6NR6aZbltQj9M8RyUgIlIn_fd20FY-zt6EIQ4sjSAzFud_Vmxw%3D%3D", newPayment);
                    if (response.IsSuccessStatusCode)
                    {
                        return null; // Success
                    }
                    else
                    {
                        return "An error occurred when updating your card, please try again!";
                    }
                }
            }
            return "Invalid user ID or empty fields.";
        }

        public async Task<PaymentModel> GetCardById(string id)
        {
            return await _httpClient.GetFromJsonAsync<PaymentModel>($"https://paymentprovider-manero.azurewebsites.net/api/GetCardById/{id}?code=Fc4HFsmJcCWrorTlPHul22gRmq5olT6p5uHkU1ngiNeaAzFux6k1Xw%3D%3D");
        }

        public async Task<string> UpdateCard(string id, PaymentModel paymentModel)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://paymentprovider-manero.azurewebsites.net/api/UpdateCard/{id}?code=J-GEoKCpAKbd1aEIlscpfzL149UH-U9AunfovO22G82jAzFuwtCXjA%3D%3D", paymentModel);
            if (response.IsSuccessStatusCode)
            {
                return null; // Success
            }
            else
            {
                return "An error occurred when updating your card, please try again!";
            }
        }
    }
}
