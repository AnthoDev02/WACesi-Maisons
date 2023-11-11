using Microsoft.AspNetCore.Mvc;
using WACesi_Maisons.Models;

namespace WACesi_Maisons.Controllers
{
    [ApiController]
    public class AuthentificationController : Controller
    {
        [HttpPost("signIn")]
        public ActionResult<bool> SignIn(Credentials credentials)
        {
            return Ok(true);
        }
    }
}
