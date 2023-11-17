namespace WACesi_Maisons.Models
{
    public class AuthenticationException: Exception
    {
        public string Message { get; set; }
        public bool IsAuthenticate { get; set; }
    }
}
