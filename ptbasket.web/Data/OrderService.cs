using Microsoft.Extensions.Configuration;
using ptbasket.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ptbasket.web.Data
{
    public class OrderService
    {
        IConfiguration configuration;
        string apiBaseUrl;
        public OrderService(IConfiguration _configuration)
        {
            configuration = _configuration;
            apiBaseUrl = configuration.GetValue<string>("ApiEndpoint");
        }
        
        public async Task<string> SubmitOrder(Order newOrder)
        {
            string orderId = string.Empty;
            var serializedOrder = System.Text.Json.JsonSerializer.Serialize<Order>(newOrder);
            var payload = new StringContent(serializedOrder, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-user-id", newOrder.FlatNumber); //FIXME : need to fetch this from logged in user
            var response = httpClient.PostAsync(new Uri(apiBaseUrl), payload).Result;
            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                orderId = response.Content.ReadAsStringAsync().Result;
            }
            return orderId;
        }

        public async Task<List<Order>> GetMyOrders()
        {
            List<Order> orders = new List<Order>();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-user-id", "admin"); //FIXME : need to fetch this from logged in user
            var response = await httpClient.GetAsync(apiBaseUrl);
            if(response.IsSuccessStatusCode)
            {
                orders = JsonConvert.DeserializeObject<List<Order>>(response.Content.ReadAsStringAsync().Result);
            }
            return orders;
        }

        public async Task<bool> UpdateOrderStatus(Order order)
        {
            var serializedOrder = System.Text.Json.JsonSerializer.Serialize<Order>(order);
            var payload = new StringContent(serializedOrder, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-user-id", "admin"); //FIXME : need to fetch this from logged in user
            var response = httpClient.PutAsync($"{apiBaseUrl}/{order.Id}", payload).Result;
            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
