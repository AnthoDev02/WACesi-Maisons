using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using WACesi_Maisons.Models;

namespace WACesi_Maisons.repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlConnection _connection;

        public AuthenticationRepository(IConfiguration configuration, MySqlConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
        }
        public bool CheckAuthentication(Credentials credentials)
        {
            try
            {
                string salt = _configuration.GetSection("Authentication").Get<AuthenticationOptions>().Salt;
                var user = new User();
                string sqlRequest = string.Format("SELECT * FROM UTILISATEUR WHERE mail = '{0}'", credentials.Email);

                _connection.Open();
                using (MySqlCommand command = new MySqlCommand(sqlRequest, _connection))
                {
                    //Recup le MDP le hasher
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        return false;
                    }
                    while (reader.Read())
                    {
                        user.Mail = (string)reader["mail"];
                        user.Password = (string)reader["password"];
                        user.Salt = (string)reader["salt"];
                    }
                    _connection.Close();
                    //verify
                    return VerifyPassword(credentials.Password, user.Password, user.Salt);
                    
                }
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public static HashSalt GenerateSaltedHash(int size, string password)
        {
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
            return hashSalt;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}
