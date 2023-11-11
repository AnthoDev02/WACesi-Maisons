namespace WACesi_Maisons.Models
{
    public class AppSettings
    {
        public ConnectionStringsOptions ConnectionStrings { get; set; }
    }

    public class ConnectionStringsOptions
    {
        public string SQLServerConnexion { get; set; }
    }

}
