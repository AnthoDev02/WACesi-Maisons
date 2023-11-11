using Microsoft.AspNetCore.Mvc;
using WACesi_Maisons.Models;
using WACesi_Maisons.repository;

namespace WACesi_Maisons.Controllers
{
    [ApiController]
    public class PromosController : ControllerBase
    {
        private readonly IPromoRepository _promoRepository;
        public PromosController(IPromoRepository promoRepository) {
            _promoRepository = promoRepository;
        }

        [HttpGet("allPromos")]
        public ActionResult AllPromos()
        {
            List<Promo> allPromos = _promoRepository.GetAllPromos();

            if (allPromos.Count > 0)
            {
                return Ok(allPromos);
            }
            return NotFound();
        }
    }
}