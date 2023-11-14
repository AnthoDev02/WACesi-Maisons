using Microsoft.AspNetCore.Authentication;

namespace WACesi_Maisons.Models
{
    public class AppSettings
    {
        public ConnectionStringsOptions ConnectionStrings { get; set; }
        public AuthenticationOptions Authentication { get; set; }
    }

    public class ConnectionStringsOptions
    {
        public string SQLServerConnexion { get; set; }
    }
    public class AuthenticationOptions
    {
        public string Salt { get; set; }
    }

}
