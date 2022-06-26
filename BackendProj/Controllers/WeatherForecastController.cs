using Microsoft.AspNetCore.Mvc;

namespace BackendProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private AudioService _audioService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger , 
            AudioService audioService)
        {
            _logger = logger;
            _audioService = audioService;

        }

        //[HttpGet]
        //public async Task<bool> Get()
        //{
        //    await _audioService.StartSending();
        //    return true;
        //}

        [HttpGet("get-file-stream/{id}")]
        public FileStreamResult DownloadAsync(string id)
        {
            var fileName = $"{id}Label.wav";
            var mimeType = "application/....";
            Stream stream = GetFileStreamById($"sound/{id}Label.wav");

            return new FileStreamResult(stream, mimeType)
            {
                FileDownloadName = fileName
            };
        }


        private Stream GetFileStreamById(string path)
        {
            var audio = System.IO.File.ReadAllBytes(path);
            return new MemoryStream(audio);
        }
    }
}