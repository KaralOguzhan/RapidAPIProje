using Microsoft.AspNetCore.Mvc;
using MyAkademiRapidApi.Models;
using Newtonsoft.Json;

namespace MyAkademiRapidApi.Controllers
{
	public class MoviesSeriesController : Controller
	{

	
		public async Task<IActionResult> Index()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"),
				Headers =
		{
			{ "X-RapidAPI-Key", "941cfb51ebmsh9c789303add2982p1062c5jsn08a9b508ce41" },
			{ "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
		},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<SeriesViewModel>>(body);
				return View(values);
			}
		}
	}
}
