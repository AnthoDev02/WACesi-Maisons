namespace WACesi_Maisons.Models
{
    public class Promo
    {
        public string Nom { get; set; }
        public string Score { get; set; }
        public string Logo { get; set; }
        
        public Promo(string nom, string score, string logo)
        {
            this.Nom = nom;
            this.Score = score;
            this.Logo = logo;
        }
    }
}
