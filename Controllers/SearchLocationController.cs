using Microsoft.AspNetCore.Mvc;
using MyAkademiRapidApi.Models;
using Newtonsoft.Json;

namespace MyAkademiRapidApi.Controllers
{
	public class SearchLocationController : Controller
	{
		public async Task<IActionResult> Index(string city)
		{
			if (!string.IsNullOrEmpty(city))
			{ 
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?name={city}&locale=en-gb"),
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
				var values = JsonConvert.DeserializeObject<List<SearchLocationViewModel>> (body);
				return View(values.ToList());
				
			}

			}
			else
			{
				var client = new HttpClient();
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Get,
					RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?name=istanbul&locale=en-gb"),
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
					var values = JsonConvert.DeserializeObject<List<SearchLocationViewModel>>(body);
					return View(values.ToList());

				}
			}

		}
	}
}
