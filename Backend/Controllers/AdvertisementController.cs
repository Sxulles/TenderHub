using Backend.Service.Factory;
using Backend.Service.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUserRepository _userRepository;

        public AdvertisementController(IAdvertisementRepository advertisementRepository, IUserRepository userRepository)
        {
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetAdvertisements")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAdvertisements()
        {
            try
            {
                return Ok(_advertisementRepository.GetAll());
            }
            catch (Exception e)
            {
                return NotFound("No advertisements in database.");
            }
        }

        [HttpPost]
        [Route("AddAdvertisement")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdvertisement()
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
    }
}
