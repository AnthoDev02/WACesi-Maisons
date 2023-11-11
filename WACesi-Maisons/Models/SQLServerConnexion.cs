namespace WACesi_Maisons.Models
{
    public class SQLServerConnexion
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public string Database { get; set; }

        public SQLServerConnexion(string server, string port, string uid, string pwd, string database)
        {
            Server = server;
            Port = port;
            Uid = uid;
            Pwd = pwd;
            Database = database;
        }
    }
}
