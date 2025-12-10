namespace BibliothequeNumerique.Models
{
    public class Livre : Document
    {
        public int NombrePages { get; set; }

        public Livre(string titre, string auteur, int annee, int nombrePages)
            : base(titre, auteur, annee)
        {
            NombrePages = nombrePages;
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"=== LIVRE ===");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Titre: {Titre}");
            Console.WriteLine($"Auteur: {Auteur}");
            Console.WriteLine($"Année: {Annee}");
            Console.WriteLine($"Nombre de pages: {NombrePages}");
            Console.WriteLine("=====================");
        }
    }
}