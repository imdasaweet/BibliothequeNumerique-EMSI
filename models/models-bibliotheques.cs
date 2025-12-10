using System;
using System.Collections.Generic;
using System.Linq;
using BibliothequeNumerique.Exceptions;

namespace BibliothequeNumerique.Models
{
    public class Bibliotheque
    {
        private List<Document> documents;

        public Bibliotheque()
        {
            documents = new List<Document>();
        }

        public void AjouterDocument(Document d)
        {
            documents.Add(d);
            Console.WriteLine($"Document ajouté avec succès! ID: {d.Id}");
        }

        public void SupprimerDocument(Guid id)
        {
            var document = documents.FirstOrDefault(d => d.Id == id);
            
            if (document == null)
                throw new DocumentNonTrouveException($"Aucun document avec l'ID {id} n'a été trouvé.");
            
            documents.Remove(document);
            Console.WriteLine($"Document supprimé avec succès!");
        }

        public List<Document> Rechercher(string motCle)
        {
            motCle = motCle.ToLower();
            
            var resultats = documents.Where(d =>
                d.Titre.ToLower().Contains(motCle) ||
                d.Auteur.ToLower().Contains(motCle) ||
                d.Annee.ToString().Contains(motCle)
            ).ToList();

            if (!resultats.Any())
                throw new DocumentNonTrouveException($"Aucun document contenant '{motCle}' n'a été trouvé.");
            
            return resultats;
        }

        public void AfficherTous()
        {
            if (!documents.Any())
            {
                Console.WriteLine("La bibliothèque est vide.");
                return;
            }

            Console.WriteLine($"\n=== LISTE DES DOCUMENTS ({documents.Count} éléments) ===");
            foreach (var doc in documents)
            {
                doc.AfficherDetails();
            }
        }

        public List<Document> GetDocuments() => documents;
        
        public void SetDocuments(List<Document> docs) => documents = docs;
    }
}