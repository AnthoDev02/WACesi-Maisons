namespace WACesi_Maisons.Models
{
    public class User
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public string Promotion { get; set; }
    }
}
