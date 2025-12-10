namespace BibliothequeNumerique.Models
{
    public class Magazine : Document
    {
        public int Numero { get; set; }

        public Magazine(string titre, string auteur, int annee, int numero)
            : base(titre, auteur, annee)
        {
            Numero = numero;
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"=== MAGAZINE ===");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Titre: {Titre}");
            Console.WriteLine($"Auteur: {Auteur}");
            Console.WriteLine($"Année: {Annee}");
            Console.WriteLine($"Numéro: {Numero}");
            Console.WriteLine("=====================");
        }
    }
}