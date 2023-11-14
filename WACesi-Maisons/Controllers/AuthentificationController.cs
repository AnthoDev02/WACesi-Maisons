using Microsoft.AspNetCore.Mvc;
using WACesi_Maisons.Models;
using WACesi_Maisons.repository;

namespace WACesi_Maisons.Controllers
{
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthentificationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("signIn")]
        public ActionResult<bool> SignIn(Credentials credentials)
        {
            var isAuthenticated = _authenticationRepository.CheckAuthentication(credentials);

            if (isAuthenticated)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);            }
        }
    }
}
