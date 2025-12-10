using System;
using System.Linq;
using BibliothequeNumerique.Exceptions;
using BibliothequeNumerique.Models;
using BibliothequeNumerique.Services;

namespace BibliothequeNumerique
{
    class Program
    {
        static void Main(string[] args)
        {
            var bibliotheque = new Bibliotheque();
            bool continuer = true;
            
            Console.WriteLine("=== BIBLIOTHÈQUE NUMÉRIQUE ===");
            
            while (continuer)
            {
                AfficherMenu();
                Console.Write("\nVotre choix: ");
                
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out int choix))
                    {
                        Console.WriteLine("Veuillez entrer un nombre valide.");
                        continue;
                    }
                    
                    switch (choix)
                    {
                        case 1:
                            AjouterDocument(bibliotheque);
                            break;
                            
                        case 2:
                            bibliotheque.AfficherTous();
                            break;
                            
                        case 3:
                            RechercherDocument(bibliotheque);
                            break;
                            
                        case 4:
                            SupprimerDocument(bibliotheque);
                            break;
                            
                        case 5:
                            SauvegarderBibliotheque(bibliotheque);
                            break;
                            
                        case 6:
                            ChargerBibliotheque(ref bibliotheque);
                            break;
                            
                        case 7:
                            continuer = false;
                            Console.WriteLine("Au revoir!");
                            break;
                            
                        default:
                            Console.WriteLine("Choix invalide. Veuillez choisir entre 1 et 7.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur: {ex.Message}");
                }
                
                if (continuer)
                {
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        
        static void AfficherMenu()
        {
            Console.WriteLine("\n=== MENU PRINCIPAL ===");
            Console.WriteLine("1. Ajouter un document");
            Console.WriteLine("2. Afficher tous les documents");
            Console.WriteLine("3. Rechercher par mot-clé");
            Console.WriteLine("4. Supprimer un document");
            Console.WriteLine("5. Sauvegarder dans un fichier");
            Console.WriteLine("6. Charger depuis un fichier");
            Console.WriteLine("7. Quitter");
        }
        
        static void AjouterDocument(Bibliotheque bibliotheque)
        {
            Console.WriteLine("\n=== AJOUTER UN DOCUMENT ===");
            Console.WriteLine("1. Livre");
            Console.WriteLine("2. Magazine");
            Console.WriteLine("3. Document PDF");
            Console.Write("Type de document: ");
            
            if (!int.TryParse(Console.ReadLine(), out int type) || type < 1 || type > 3)
            {
                Console.WriteLine("Type invalide.");
                return;
            }
            
            Console.Write("Titre: ");
            var titre = Console.ReadLine();
            
            Console.Write("Auteur: ");
            var auteur = Console.ReadLine();
            
            Console.Write("Année: ");
            if (!int.TryParse(Console.ReadLine(), out int annee))
            {
                Console.WriteLine("Année invalide.");
                return;
            }
            
            Document doc = null;
            
            switch (type)
            {
                case 1:
                    Console.Write("Nombre de pages: ");
                    if (!int.TryParse(Console.ReadLine(), out int pages))
                    {
                        Console.WriteLine("Nombre de pages invalide.");
                        return;
                    }
                    doc = new Livre(titre, auteur, annee, pages);
                    break;
                    
                case 2:
                    Console.Write("Numéro: ");
                    if (!int.TryParse(Console.ReadLine(), out int numero))
                    {
                        Console.WriteLine("Numéro invalide.");
                        return;
                    }
                    doc = new Magazine(titre, auteur, annee, numero);
                    break;
                    
                case 3:
                    Console.Write("Taille en Mo: ");
                    if (!double.TryParse(Console.ReadLine(), out double taille))
                    {
                        Console.WriteLine("Taille invalide.");
                        return;
                    }
                    doc = new DocumentPDF(titre, auteur, annee, taille);
                    break;
            }
            
            bibliotheque.AjouterDocument(doc);
        }
        
        static void RechercherDocument(Bibliotheque bibliotheque)
        {
            Console.Write("\nMot-clé de recherche: ");
            var motCle = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(motCle))
            {
                Console.WriteLine("Mot-clé invalide.");
                return;
            }
            
            try
            {
                var resultats = bibliotheque.Rechercher(motCle);
                Console.WriteLine($"\n=== RÉSULTATS DE RECHERCHE ({resultats.Count} éléments) ===");
                foreach (var doc in resultats)
                {
                    doc.AfficherDetails();
                }
            }
            catch (DocumentNonTrouveException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        static void SupprimerDocument(Bibliotheque bibliotheque)
        {
            Console.Write("\nID du document à supprimer: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("ID invalide.");
                return;
            }
            
            try
            {
                bibliotheque.SupprimerDocument(id);
            }
            catch (DocumentNonTrouveException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        static void SauvegarderBibliotheque(Bibliotheque bibliotheque)
        {
            Console.Write("\nChemin du fichier de sauvegarde: ");
            var chemin = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(chemin))
            {
                Console.WriteLine("Chemin invalide.");
                return;
            }
            
            try
            {
                FileService.Sauvegarder(bibliotheque, chemin);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde: {ex.Message}");
            }
        }
        
        static void ChargerBibliotheque(ref Bibliotheque bibliotheque)
        {
            Console.Write("\nChemin du fichier à charger: ");
            var chemin = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(chemin))
            {
                Console.WriteLine("Chemin invalide.");
                return;
            }
            
            try
            {
                bibliotheque = FileService.Charger(chemin);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement: {ex.Message}");
            }
        }
    }
}