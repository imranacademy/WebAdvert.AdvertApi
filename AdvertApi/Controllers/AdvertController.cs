using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
    [Route("adverts/v1")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;

        public AdvertController(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(CreateAdvertResponse),200)]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string recordId;
            try
            {
                 recordId = await _advertStorageService.Add(model);

            }
            catch (KeyNotFoundException )
            {
                return new NotFoundResult();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
            return StatusCode(201, new CreateAdvertResponse { Id = recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {

            try
            {
              await _advertStorageService.Confirm(model);

            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
            return new OkResult();
        }
    }
}
