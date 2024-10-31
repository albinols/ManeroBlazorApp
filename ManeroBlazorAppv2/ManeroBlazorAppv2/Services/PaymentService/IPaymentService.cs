using ManeroBlazorAppv2.Models;

namespace ManeroBlazorAppv2.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<string> CreateNewPayment(PaymentModel newPayment);
        Task<PaymentModel> GetCardById(string id);
        Task<string> UpdateCard(string id, PaymentModel paymentModel);
    }
}
