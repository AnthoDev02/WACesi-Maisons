namespace WACesi_Maisons.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public string Promotion { get; set; }
    }
}
