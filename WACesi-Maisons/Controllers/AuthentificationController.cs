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

        [HttpPost("SignIn")]
        public ActionResult<bool> SignIn(Credentials credentials)
        {
            var isAuthenticated = _authenticationRepository.CheckAuthentication(credentials);

            if (isAuthenticated)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPost("SignUp")]
        public ActionResult<bool> SignUp(User userInfos)
        {
            bool isRegistered = false;
            try
            {
                isRegistered = _authenticationRepository.TryRegistered(userInfos);
                if (isRegistered)
                {
                    return Ok(true);
                }
            }
            catch (AuthenticationException exception)
            {
                AuthenticationException authenticateException = new AuthenticationException()
                {
                    Message = exception.Message,
                    IsAuthenticate = exception.IsAuthenticate
                };
                return NotFound(authenticateException);
            }
            return Ok(false);
        }
    }
}
