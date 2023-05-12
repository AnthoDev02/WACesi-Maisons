namespace WACesi_Maisons.Models
{
    public class Promo
    {
        public string Nom { get; set; }
        public string Score { get; set; }
        public string logo { get; set; }
        
        public Promo(string Nom, string Score, string logo)
        {
            this.Nom = Nom;
            this.Score = Score;
            this.logo = logo;
        }
    }
}
