using Microsoft.AspNetCore.Mvc;
using MyAkademiRapidApi.Models;
using Newtonsoft.Json;

namespace MyAkademiRapidApi.Controllers
{
    public class ExchangeController : Controller
    {
        public async Task<IActionResult> Index(IFormCollection data)
        {
            var moneyType = data["txtValue1"].ToString();
            if (moneyType != null)
            {



                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency={moneyType}&locale=en-gb"),
                    Headers =
    {
        { "X-RapidAPI-Key", "941cfb51ebmsh9c789303add2982p1062c5jsn08a9b508ce41" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ExchangeViewModel>(body);
                    return View(values.exchange_rates.ToList());
                }
            }
            else
            {

            
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                 Method = HttpMethod.Get,
                 RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=USD&locale=en-gb"),
                  Headers =
        {
            { "X-RapidAPI-Key", "941cfb51ebmsh9c789303add2982p1062c5jsn08a9b508ce41" },
            { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
        },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ExchangeViewModel>(body);
                    return View(values.exchange_rates.ToList());
                }
            }
        }
    }
}
