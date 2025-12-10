namespace BibliothequeNumerique.Models
{
    public class DocumentPDF : Document
    {
        public double TailleEnMo { get; set; }

        public DocumentPDF(string titre, string auteur, int annee, double tailleEnMo)
            : base(titre, auteur, annee)
        {
            TailleEnMo = tailleEnMo;
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"=== DOCUMENT PDF ===");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Titre: {Titre}");
            Console.WriteLine($"Auteur: {Auteur}");
            Console.WriteLine($"Année: {Annee}");
            Console.WriteLine($"Taille: {TailleEnMo} Mo");
            Console.WriteLine("=====================");
        }
    }
}