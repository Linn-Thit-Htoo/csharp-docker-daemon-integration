using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PublicApi.Models;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContainerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("CreateContianer")]
        public async Task<IActionResult> CreateContainer(CreateContainerRequestModel requestModel, string containerName, CancellationToken cs)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("DockerClient");
                HttpContent content = new StringContent(JsonConvert.SerializeObject(requestModel), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"/containers/create?name={containerName}", content, cs);

                response.EnsureSuccessStatusCode();
                string jsonStr = await response.Content.ReadAsStringAsync(cs);

                var model = JsonConvert.DeserializeObject<CreateContainerResponseModel>(jsonStr);
                
                if (model is not null)
                {
                    await StartContainerAsync(model.Id);
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        private async Task StartContainerAsync(string containerId)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("DockerClient");
                HttpResponseMessage response = await httpClient.PostAsync($"/containers/{containerId}/start", null!);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
