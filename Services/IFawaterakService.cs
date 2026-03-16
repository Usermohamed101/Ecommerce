using System.CodeDom;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Ecommerce.Dtos.Fawaterak;
using Ecommerce.Dtos.Fawaterak.CallBacks;
using Google.Apis.Json;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Services
{
    public interface IFawaterakService
    {
        Task<GetPaymentMethodsRequest> GetPaymentMethods();
        Task<PaymentResponse> ExecutePaymentAsync(EInvoiceRequest req);
        bool verifyWebHookCancelTransaction(CancelWebHook dto);
        bool verifyWebHook(PaidWebHook dto);


    }

    public class FawaterakService(IHttpClientFactory httpClientFactory, IConfiguration config) : IFawaterakService
    {
        public async Task<GetPaymentMethodsRequest> GetPaymentMethods()
        {

            var client = httpClientFactory.CreateClient();



            var request = new HttpRequestMessage(HttpMethod.Get, $"{config["Fawaterak:BaseUrl"]}getPaymentmethods");
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", config["Fawaterak:ApiKey"]);
            request.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");



            var response = await client.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<GetPaymentMethodsRequest>();

        }
        public async Task<PaymentResponse> ExecutePaymentAsync(EInvoiceRequest req)
        {
            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{config["Fawaterak:BaseUrl"]}invoiceInitPay");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", config["Fawaterak:ApiKey"]);
            request.Content = new StringContent(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            if(req.PaymentMethodId==2)
                 return await response.Content.ReadFromJsonAsync<MasterCardResponse>();
            if (req.PaymentMethodId == 3)
                return await response.Content.ReadFromJsonAsync<FawreyResponse>();
            if (req.PaymentMethodId == 4)
                return await response.Content.ReadFromJsonAsync<MobileWalletResponse>();

            throw new Exception("something went wrong");
        }












        public bool verifyWebHook(PaidWebHook dto)
        {


            var haskKey = GenerateHashKeyForWebhook(dto.invoiceId, dto.invoiceKey, dto.paymentMethod);
            return dto.hashKey == haskKey;


        }


        public bool verifyWebHookCancelTransaction (CancelWebHook dto)
        {


            var haskKey = GenerateHashKeyForCancelWebhook(dto.refrenceId, dto.paymentMethod);
            return dto.hashKey == haskKey;


        }











        public string GenerateHashKeyForWebhook(long invoiceId,string invoiceKey,string paymentMethod)
        {
            string secretKey = config["Fawaterak:ApiKey"];
            string queryParams = $"InvoiceId={invoiceId}&InvoiceKey={invoiceKey}&PaymentMethod={paymentMethod}";


            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var queryBytes = Encoding.UTF8.GetBytes(queryParams);

            using (var hmc=new HMACSHA256(keyBytes))
            {

                var hashKey = hmc.ComputeHash(queryBytes);
                return BitConverter.ToString(hashKey).Replace("-", "").ToLower();
            }

        }




        public string GenerateHashKeyForCancelWebhook(string refrenceId, string paymentMethod)
        {
            string secretKey = config["Fawaterak:vendorKey"];
            string queryParams = $"referenceId={refrenceId}&PaymentMethod={paymentMethod}";


            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var queryBytes = Encoding.UTF8.GetBytes(queryParams);

            using (var hmc = new HMACSHA256(keyBytes))
            {

                var hashKey = hmc.ComputeHash(queryBytes);
                return BitConverter.ToString(hashKey).Replace("_", "").ToLower();
            }

        }

    }
}