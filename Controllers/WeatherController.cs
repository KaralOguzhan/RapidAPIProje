using Microsoft.AspNetCore.Mvc;
using MyAkademiRapidApi.Models;
using Newtonsoft.Json;

namespace MyAkademiRapidApi.Controllers
{
	public class WeatherController : Controller
	{
		public async Task<IActionResult> Index(IFormCollection data)
		{
			var city = data["city"].ToString();
			
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://yahoo-weather5.p.rapidapi.com/weather?location={city}&format=json&u=c"),
				Headers =
	{
		{ "X-RapidAPI-Key", "941cfb51ebmsh9c789303add2982p1062c5jsn08a9b508ce41" },
		{ "X-RapidAPI-Host", "yahoo-weather5.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<WeatherViewModel>(body);
				return View(values);
			}
		}
	}
}
