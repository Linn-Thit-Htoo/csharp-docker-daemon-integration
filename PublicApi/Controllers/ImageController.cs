using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PublicApi.Models;

namespace PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("GetImages")]
        public async Task<IActionResult> GetImages(CancellationToken cs)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("DockerClient");
            HttpResponseMessage response = await httpClient.GetAsync("/images/json", cs);
            response.EnsureSuccessStatusCode();

            string jsonStr = await response.Content.ReadAsStringAsync(cs);
            var lst = JsonConvert.DeserializeObject<List<ImageModel>>(jsonStr);

            return Ok(lst);
        }
    }
}
