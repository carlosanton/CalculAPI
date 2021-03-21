using CalculatorService.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CalculatorService.Client.Services
{
    public class Service : IService
    {
        private string url = "http://localhost:65130/";
        public void Add(Sums sums, string clientID)
        {
            GetHttpResponse(clientID, sums, "api/calculator/add");
        }

        public void Divide(Division division, string clientID)
        {
            GetHttpResponse(clientID, division, "api/calculator/div");
        }

        public void Factor(Factor factor, string clientID)
        {
            GetHttpResponse(clientID, factor, "api/calculator/mult");
        }

        public void Query(User user)
        {
            GetHttpResponse(String.Empty, user, "api/calculator/query");
        }

        public void Sqrt(Sqrt sqrt, string clientID)
        {
            GetHttpResponse(clientID, sqrt, "api/calculator/sqrt");
        }

        public void Subtraction(Subtraction subtraction, string clientID)
        {
            GetHttpResponse(clientID, subtraction, "api/calculator/sub");
        }

        private void GetHttpResponse(string clientID, object sendObject, string endpoint)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:65130/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!String.IsNullOrEmpty(clientID))
            {
                client.DefaultRequestHeaders.Add("X-Evi-Tracking-Id", clientID);
            }

            string serailizeddto = JsonConvert.SerializeObject(sendObject);

            var inputMessage = new HttpRequestMessage
            {
                Content = new StringContent(serailizeddto, Encoding.UTF8, "application/json")
            };

            inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response =
                client.PostAsync(endpoint, inputMessage.Content).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");

                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);

                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
        }
    }
}
