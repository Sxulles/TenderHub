using IdentityTest.Model;
using IdentityTest.Model.DbEntities;
using IdentityTest.Services.Factory;
using IdentityTest.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisementsController :Controller
    {
        private IAdvertisementRepository _advertisementRepository;
        private IUserRepository _userRepository; 
        private readonly ILogger<AdvertisementsController> _logger;

        public AdvertisementsController(ILogger<AdvertisementsController> logger, IAdvertisementRepository advertisementRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetAdvertisements"), Authorize(Roles = "User")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_advertisementRepository.GetAll(User.Identity.Name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("AddAdvertisement"), Authorize(Roles = "User")]
        public async Task<IActionResult> Add()
        {
            try
            {
                Guid userId = _userRepository.GetUserByName(User.Identity.Name).Id;

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
        [Route("SaveAdvertisement/{advertisementId:required}"), Authorize(Roles = "User")]
        public async Task<IActionResult> Save(Guid advertisementId)
        {
            try
            {
                var success = await _advertisementRepository.SaveAdvertisementForUser(userName: User.Identity.Name, advertisementId);

                return Ok(new { Success = success });
            }
            catch
            {
                return BadRequest("Some error happened");
            }
        }

        [HttpGet]
        [Route("GetSavedAdvertisements"), Authorize(Roles = "User")]
        public IActionResult GetSaved()
        {
            try
            {
                return Ok(_advertisementRepository.GetSavedAdvertisementForUser(userName: User.Identity.Name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteSavedAdvertisement/{advertisementId:required}"), Authorize(Roles="User")]
        public async Task<IActionResult> DeleteSaved(Guid advertisementId)
        {
            try
            {
                return Ok(await _advertisementRepository.DeleteAdvertisement(advertisementId));
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return BadRequest(false);
            }
        }
    }
}
