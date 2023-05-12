using WACesi_Maisons.Models;

namespace WACesi_Maisons.repository
{
    public class PromoRepository : IPromoRepository
    {
        public List<Promo> GetAllPromos()
        {
            List<Promo> allPromos = new List<Promo>();

            try
            {
                allPromos = new List<Promo>
                {
                    new Promo("coucou", "80", "Poisson"),
                    new Promo("Kenny", "12", "AOE2"),
                    new Promo("test", "50", "Hamster")
                };
            }
            catch (Exception ex)
            {
                new Exception(ex.ToString());
            }

            return allPromos;
        }
    }
}
