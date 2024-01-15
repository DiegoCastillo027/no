using Microsoft.AspNetCore.Mvc;

namespace PhotoProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecast : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherForecast(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WeatherForecast>> GetPhotosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<WeatherForecast>>("https://jsonplaceholder.typicode.com/photos");
        }
    };

        private readonly WeatherForecast _photoService;

        public WeatherForecastController(PhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var photos = await _photoService.GetPhotosAsync();
            var paginatedPhotos = photos.Skip((page - 1) * pageSize).Take(pageSize);

            var viewModel = new PhotoViewModel
            {
                Photos = paginatedPhotos,
                CurrentPage = page,
                PageSize = pageSize
            };

            return View(viewModel);
        }
    }
}