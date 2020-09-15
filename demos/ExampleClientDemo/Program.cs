using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace ExampleClientDemo
{
    class Program
    {
        private static string _baseUrl = "https://localhost:63534";
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri(_baseUrl);

            // 1. without access_token will not access the service  
            //    and return 401 .  
            //var resWithoutToken = client.GetAsync("/api/catalog").Result;

            //print something here   

            //2. with access_token will access the service  
            //   and return result.  
            client.DefaultRequestHeaders.Clear();
            var jwt = GetJwt();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            var resWithToken = client.GetAsync("/api/catalog").Result;

            //print something here   

            //3. visit no auth service   
            //client.DefaultRequestHeaders.Clear();
            //var res = client.GetAsync("/api/catalog").Result;

            //print something here   

            Console.Read();
        }

        private static string GetJwt()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Clear();
            var res2 = client.GetAsync("/api/auth?name=catcher&pwd=123").Result;
            dynamic jwt = JsonConvert.DeserializeObject(res2.Content.ReadAsStringAsync().Result);
            return jwt.access_token;
        }
    }
}
