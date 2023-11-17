using MySql.Data.MySqlClient;
using System.Net;
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
                User user = new User();
                string sqlRequest = string.Format("SELECT * FROM UTILISATEUR WHERE mail = '{0}'", credentials.Email);

                _connection.Open();
                using (MySqlCommand command = new MySqlCommand(sqlRequest, _connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        return false;
                    }
                    while (reader.Read())
                    {
                        user.Email = (string)reader["mail"];
                        user.Password = (string)reader["password"];
                        user.Salt = (string)reader["salt"];
                    }
                    _connection.Close();

                    return VerifyPassword(credentials.Password, user.Password, user.Salt);

                }
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool TryRegistered(User userInfos)
        {
            AuthenticationException authenticateException = null;
            try
            {
                string salt = _configuration.GetSection("Authentication").Get<AuthenticationOptions>().Salt;
                HashSalt hashSalt = GenerateSaltedHash(16, salt);
                string userSqlRequest = string.Format("SELECT * FROM UTILISATEUR WHERE mail = '{0}'", userInfos.Email);

                _connection.Open();
                using (MySqlCommand command = new MySqlCommand(userSqlRequest, _connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        authenticateException = new AuthenticationException()
                        {
                            Message = "Le mail existe déjà",
                            IsAuthenticate = false
                        };
                        throw authenticateException;
                    }
                }
                _connection.Close();

                string insertSqlRequest = string.Format("INSERT INTO UTILISATEUR (mail, password, salt, FK_idRole, FK_idPromotion) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", userInfos.Email, hashSalt.Hash, hashSalt.Salt, userInfos.Role, userInfos.Promotion);

                _connection.Open();
                using (MySqlCommand command = new MySqlCommand(insertSqlRequest, _connection))
                {
                    command.ExecuteNonQuery();
                    _connection.Close();
                    return true;
                }
            }
            catch (AuthenticationException ae)
            {
                authenticateException = new AuthenticationException()
                {
                    Message = ae.Message,
                    IsAuthenticate = false
                };
                throw authenticateException;
            }
            catch (Exception)
            {
                authenticateException = new AuthenticationException()
                {
                    Message = "Une erreur est survenue lors la tentative de création de compte",
                    IsAuthenticate = false
                };
                throw authenticateException;
            }
        }

        private static HashSalt GenerateSaltedHash(int size, string password)
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

        private static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}
