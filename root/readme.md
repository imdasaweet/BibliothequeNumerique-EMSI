
**Projet pour l'examen DOTNET EMSI - 4IIR**

Développer un mini-système de gestion pour une bibliothèque numérique en C# (.NET 6+) suivant les principes de POO avancée et de gestion des ressources.


### Partie 1 - Modélisation Objet (POO C#)
-  Classe abstraite `Document` avec propriétés et méthodes
-  3 classes dérivées : `Livre`, `Magazine`, `DocumentPDF`
-  Classe `Bibliotheque` pour la gestion des collections
-  Polymorphisme avec `AfficherDetails()`

### Partie 2 - Gestion des erreurs
-  Exception personnalisée `DocumentNonTrouveException`
-  Gestion robuste des erreurs (fichier, format, etc.)
-  Utilisation appropriée de `try/catch/finally`

### Partie 3 - Flux de données
-  Sauvegarde des documents dans un fichier CSV
-  Chargement depuis un fichier
-  Libération propre des ressources avec `using`

### Partie 4 - Programme principal
-  Menu console interactif
-  7 options de gestion complète
-  Gestion des erreurs sans plantage


### Prérequis
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) ou supérieur
- Visual Studio 2022, VS Code ou Rider

### Instructions
```bash
# 1. Clonez le dépôt
git clone https://github.com/imdasaweet/BibliothequeNumerique-EMSI.git

# 2. Accédez au dossier
cd BibliothequeNumerique-EMSI

# 3. Compilez et exécutez
dotnet run