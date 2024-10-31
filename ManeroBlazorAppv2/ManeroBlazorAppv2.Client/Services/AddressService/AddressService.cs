using ManeroBlazorAppv2.Client.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ManeroBlazorAppv2.Client.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AddressService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string> CreateNewAddress(AddressModel newAddress)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                newAddress.UserId = userId;

                if (!string.IsNullOrEmpty(newAddress.Title) && !string.IsNullOrEmpty(newAddress.Address))
                {
                    var response = await _httpClient.PostAsJsonAsync("https://addressprovider-manero.azurewebsites.net/api/CreateAddress?code=ZfZyebKiSItNtyNy1EcdIrXfp8YyVZH1Kca8_30uOvs8AzFuGgH2ZA%3D%3D", newAddress);
                    if (response.IsSuccessStatusCode)
                    {
                        return null; // Success
                    }
                    else
                    {
                        return "An error occurred when updating your profile, please try again!";
                    }
                }
            }
            return "Invalid user ID or empty address/title.";
        }

        public async Task<UpdateAddressModel> GetAddressById(string id)
        {
            return await _httpClient.GetFromJsonAsync<UpdateAddressModel>($"https://addressprovider-manero.azurewebsites.net/api/GetAddressById/{id}?code=7ybAKMmsHBFd81JrIXDBlxLPk9l316ghydDcM_nH7_LpAzFuImrRCQ%3D%3D");
        }

        public async Task<string> UpdateAddress(string id, UpdateAddressModel updateAddress)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://addressprovider-manero.azurewebsites.net/api/UpdateAddress/{id}?code=wYbvd2P-_9UfeZX3upF97WMZjMa5UYT1eE0vHyFN8fqQAzFuCY2shQ%3D%3D", updateAddress);
            if (response.IsSuccessStatusCode)
            {
                return null; // Success
            }
            else
            {
                return "An error occurred when updating your address, please try again!";
            }
        }
    }
}
