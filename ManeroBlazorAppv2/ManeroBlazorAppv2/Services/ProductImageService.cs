using Newtonsoft.Json;
using System.Text;

namespace ManeroBlazorAppv2.Services;

public class ProductImageService(HttpClient client)
{
    private readonly HttpClient _httpClient = client;

    public async Task<bool> DeleteProductImages(string productId, Dictionary<string, List<string>> imgUrls)
    {
        var deleteProducImagestUrl = "https://productimageprovider.azurewebsites.net/api/product/images/%7Bid%7D?code=SeeYouLaterImages";

        var formattedUrl = deleteProducImagestUrl.Replace("%7Bid%7D", productId);
        var productImagesDeleteUri = new Uri(formattedUrl);
        var jsonContent = JsonConvert.SerializeObject(imgUrls);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Delete, productImagesDeleteUri)
        {
            Content = httpContent
        };
        
        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
            return true;

        return false;

    }
}
