using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BibliothequeNumerique.Models;

namespace BibliothequeNumerique.Services
{
    public class FileService
    {
        public static void Sauvegarder(Bibliotheque bibliotheque, string cheminFichier)
        {
            try
            {
                using (var fileStream = new FileStream(cheminFichier, FileMode.Create, FileAccess.Write))
                using (var writer = new StreamWriter(fileStream))
                {
                    foreach (var doc in bibliotheque.GetDocuments())
                    {
                        string ligne = "";
                        
                        if (doc is Livre livre)
                        {
                            ligne = $"LIVRE|{livre.Id}|{livre.Titre}|{livre.Auteur}|{livre.Annee}|{livre.NombrePages}";
                        }
                        else if (doc is Magazine magazine)
                        {
                            ligne = $"MAGAZINE|{magazine.Id}|{magazine.Titre}|{magazine.Auteur}|{magazine.Annee}|{magazine.Numero}";
                        }
                        else if (doc is DocumentPDF pdf)
                        {
                            ligne = $"PDF|{pdf.Id}|{pdf.Titre}|{pdf.Auteur}|{pdf.Annee}|{pdf.TailleEnMo}";
                        }
                        
                        writer.WriteLine(ligne);
                    }
                }
                
                Console.WriteLine($"Bibliothèque sauvegardée avec succès dans {cheminFichier}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erreur d'accès au fichier: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde: {ex.Message}");
                throw;
            }
        }

        public static Bibliotheque Charger(string cheminFichier)
        {
            var bibliotheque = new Bibliotheque();
            var documents = new List<Document>();
            
            try
            {
                if (!File.Exists(cheminFichier))
                    throw new FileNotFoundException($"Fichier {cheminFichier} introuvable.");
                
                using (var fileStream = new FileStream(cheminFichier, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(fileStream))
                {
                    string ligne;
                    int ligneNum = 1;
                    
                    while ((ligne = reader.ReadLine()) != null)
                    {
                        try
                        {
                            var parts = ligne.Split('|');
                            
                            if (parts.Length < 5)
                                throw new FormatException($"Format incorrect à la ligne {ligneNum}");
                            
                            var type = parts[0];
                            var id = Guid.Parse(parts[1]);
                            var titre = parts[2];
                            var auteur = parts[3];
                            var annee = int.Parse(parts[4]);
                            
                            Document doc = null;
                            
                            switch (type)
                            {
                                case "LIVRE":
                                    if (parts.Length < 6)
                                        throw new FormatException($"Format incorrect pour un livre à la ligne {ligneNum}");
                                    var pages = int.Parse(parts[5]);
                                    doc = new Livre(titre, auteur, annee, pages) { Id = id };
                                    break;
                                    
                                case "MAGAZINE":
                                    if (parts.Length < 6)
                                        throw new FormatException($"Format incorrect pour un magazine à la ligne {ligneNum}");
                                    var numero = int.Parse(parts[5]);
                                    doc = new Magazine(titre, auteur, annee, numero) { Id = id };
                                    break;
                                    
                                case "PDF":
                                    if (parts.Length < 6)
                                        throw new FormatException($"Format incorrect pour un PDF à la ligne {ligneNum}");
                                    var taille = double.Parse(parts[5]);
                                    doc = new DocumentPDF(titre, auteur, annee, taille) { Id = id };
                                    break;
                                    
                                default:
                                    throw new FormatException($"Type de document inconnu: {type} à la ligne {ligneNum}");
                            }
                            
                            documents.Add(doc);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erreur ligne {ligneNum}: {ex.Message}");
                        }
                        
                        ligneNum++;
                    }
                }
                
                bibliotheque.SetDocuments(documents);
                Console.WriteLine($"Bibliothèque chargée avec succès depuis {cheminFichier} ({documents.Count} documents)");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
                throw;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erreur d'accès au fichier: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Erreur de format: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement: {ex.Message}");
                throw;
            }
            
            return bibliotheque;
        }
    }
}