using Backend.Services.Factory;
using Backend.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisementController : Controller
    {
        private IAdvertisementRepository _advertisementRepository;
        private IUserRepository _userRepository;
        private readonly ILogger<AdvertisementController> _logger;

        public AdvertisementController(ILogger<AdvertisementController> logger, IAdvertisementRepository advertisementRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
        }

        [HttpGet(Name = "GetAdvertisements"), Authorize(Roles = "User")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_advertisementRepository.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(Name = "AddAdvertisement"), Authorize(Roles = "User")]
        public async Task<IActionResult> Add()
        {
            try
            {
                Guid userId = _userRepository.GetUserByName("asd").Id;

                var advertisement = AdvertisementGenerator.CreateAdvertisement(userId);
                var result = await _advertisementRepository.AddAdvertisement(advertisement);

                return result ? Ok(new { success = result }) : BadRequest(new { success = result });
            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}, {e.InnerException}");
            }
        }

        [HttpPost]
        [Route("SaveAdvertisement/user={userName:required}&advertisement={advertisementId:required}"), Authorize(Roles = "User")]
        public async Task<IActionResult> SaveAdvertisementForUser(string userName, Guid advertisementId)
        {
            try
            {
                var success = await _advertisementRepository.SaveAdvertisementForUser(userName, advertisementId);

                return success ? Ok(new { Success = success }) : BadRequest(new { Success = success });
            }
            catch
            {
                return BadRequest("Some error happened");
            }
        }

        [HttpGet]
        [Route("GetSavedAdvertisements/user={userName:required}"), Authorize(Roles = "User")]
        public IActionResult GetSavedAdvertisementsForUser(string userName)
        {
            try
            {
                return Ok(_advertisementRepository.GetSavedAdvertisementForUser(userName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteSavedAdvertisement/user={userName:required}"), Authorize(Roles = "User")]
        public IActionResult DeleteSavedAdvertisement(string userName)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("error");
            }
        }
    }
}
