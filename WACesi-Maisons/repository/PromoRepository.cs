using MySql.Data.MySqlClient;
using WACesi_Maisons.Models;

namespace WACesi_Maisons.repository
{
    public class PromoRepository : IPromoRepository
    {
        private readonly MySqlConnection _connection;

        public PromoRepository(MySqlConnection connection)
        {
            _connection = connection;
        }
        public List<Promo> GetAllPromos()
        {
            try
            {
                //test
                _connection.Open();
                Console.WriteLine("Connexion réussie.");
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM PROMOTION", _connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    List<Promo> promoList = new List<Promo>();
                    while (reader.Read())
                    {
                        var promo = new Promo()
                        {
                            Nom = (string)reader["nom"],
                            Score = (int)reader["score"],
                            Logo = (string)reader["logo"],
                            Pseudo = (string)reader["pseudo"],
                            Code = (string)reader["pseudo"]

                        };
                        promoList.Add(promo);
                    }
                    _connection.Close();
                    var result = promoList;
                    return result;
                }
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
