using Microsoft.AspNetCore.Mvc;
using WACesi_Maisons.Models;
using WACesi_Maisons.repository;

namespace WACesi_Maisons.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromosController : ControllerBase
    {
        private readonly IPromoRepository _promoRepository;
        public PromosController(IPromoRepository promoRepository) {
            _promoRepository = promoRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<Promo> allPromos = _promoRepository.GetAllPromos();

            if (allPromos != null)
            {
                return Ok(allPromos);
            }
            return NoContent();
        }
    }
}