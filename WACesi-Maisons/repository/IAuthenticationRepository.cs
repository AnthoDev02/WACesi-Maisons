using WACesi_Maisons.Models;

namespace WACesi_Maisons.repository
{
    public interface IAuthenticationRepository
    {
        public bool CheckAuthentication(Credentials credentials);
        public bool TryRegistered(User userInfos);
    }
}
