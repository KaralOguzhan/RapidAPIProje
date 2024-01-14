using Microsoft.AspNetCore.Mvc;
using MyAkademiRapidApi.Models;
using Newtonsoft.Json;

namespace MyAkademiRapidApi.Controllers
{
    public class BookingController : Controller
    {
        public async Task<IActionResult> Index(IFormCollection data)
        {
            var city = data["CityCode"].ToString();
            var checkInDate = data["CheckIn"].ToString();
            var checkOutDate = data["CheckOut"].ToString();
            var children = data["ChildrenCount"].ToString();
            if(city != null && checkInDate != null && checkOutDate != null && children != null)
            {
                var clientSearhID = new HttpClient();
                var requestSearhID = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?name={city}"+ "&locale=en-gb"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "941cfb51ebmsh9c789303add2982p1062c5jsn08a9b508ce41" },
                        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
                    },
                };
                    var responseID = await clientSearhID.SendAsync(requestSearhID);
                
                    responseID.EnsureSuccessStatusCode();
                    var bodyID = await responseID.Content.ReadAsStringAsync();
                    var valuesID = JsonConvert.DeserializeObject<List<SearchLocationViewModel>>(bodyID);
                    var CityID= valuesID.Select(x=>x.dest_id).FirstOrDefault();
                    
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/search?order_by=popularity&checkout_date={checkOutDate}&filter_by_currency=AED&locale=en-gb&units=metric&dest_id={CityID}&dest_type=city&adults_number=2&room_number=1&checkin_date={checkInDate}&include_adjacency=true&page_number=0&children_number={children}&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&children_ages=5%2C0"),
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
                    var values = JsonConvert.DeserializeObject<BookingViewModel>(body);
                    return View(values.result.ToList());
                }
                
            }
            else
            {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/hotels/search?order_by=popularity&checkout_date=2024-05-20&filter_by_currency=AED&locale=en-gb&units=metric&dest_id=-1456928&dest_type=city&adults_number=2&room_number=1&checkin_date=2024-05-19&include_adjacency=true&page_number=0&children_number=2&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&children_ages=5%2C0"),
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
                var values = JsonConvert.DeserializeObject<BookingViewModel>(body);
                return View(values.result.ToList());
            }
            return View();
            }
            
        }
    }
}
