using Microsoft.VisualStudio.TestTools.UnitTesting;
using ptbasket.models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ptbasket.tests
{
    [TestClass]
    public class OrderTests
    {
        static string apiBaseUrl = string.Empty;

        [TestInitialize]
        public void OrderTestInitialize()
        {
            apiBaseUrl = "http://localhost:44378/api/order";
        }

        [TestMethod]
        public void shouldBeAbleToCreateNewOrderWithOrderUser()
        {
            var httpClient = new HttpClient();
            var order = new Order
            {
                FlatNumber = "17118",
                MobileNumber = "9632598325",
                Items = new List<Item>()
                {
                    new Item { Description="Sugar 1 kg", Quantity = 1},
                    new Item { Description = "Milk 1/2 liter", Quantity = 2}
                },
                CreatedDateTime = DateTime.Now
            };

            var serializedOrder = JsonSerializer.Serialize<Order>(order);
            var payload = new StringContent(serializedOrder, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("x-user-id", "17118");
            var response = httpClient.PostAsync(new Uri(apiBaseUrl), payload).Result;
            Assert.Equals(response.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void shouldBeAbleToGetAllActiveOrders()
        {
            var httpClient = new HttpClient();

        }
    }
}
