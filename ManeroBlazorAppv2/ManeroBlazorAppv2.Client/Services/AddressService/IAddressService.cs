using ManeroBlazorAppv2.Client.Models;

namespace ManeroBlazorAppv2.Client.Services.AddressService
{
    public interface IAddressService
    {
        Task<string> CreateNewAddress(AddressModel newAddress);
        Task<UpdateAddressModel> GetAddressById(string id);
        Task<string> UpdateAddress(string id, UpdateAddressModel updateAddress);
    }
}
