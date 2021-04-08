using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DocSolid.Test.WindowsService.BitcoinPriceService
{
    public class DataObject
    {
        public string Name { get; set; }
    }

    public class BitCoin
    {
        public DateTime CreateDate { get; set; }
        public decimal LastPrice { get; set; }
        public decimal LastChange { get; set; }
        public decimal Volume { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }

        public BitCoin(string endpoint)
        {
            string bitCoinData = GetDetail(endpoint);

            JObject jObject = JObject.Parse(bitCoinData);
            JToken jCoin = jObject["data"];
            CreateDate = DateTime.UtcNow;
            LastPrice = (decimal)jCoin["last"];
            LastChange = (decimal)jCoin["last_change"];
            Volume = (decimal)jCoin["volume"];
            HighPrice = (decimal)jCoin["high"];
            LowPrice = (decimal)jCoin["low"];
        }
        public string GetDetail(string endpoint)
        {
            string result = string.Empty;
            string urlParameters = "";
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri(endpoint);

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            } 

            return result;
        }
    }
}
