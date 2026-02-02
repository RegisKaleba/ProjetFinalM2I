using System;  // Permet d'utiliser les classes de base de .NET, comme Console

namespace BlogConsole.UI;  // Définit l'espace de noms pour organiser le code

// Classe statique utilitaire pour gérer les interactions console
public static class ConsoleHelper
{
    // Vide l'écran de la console
    public static void Clear()
    {
        Console.Clear();  // Méthode intégrée pour effacer tout le contenu de la console
    }

    // Met en pause l'exécution en attendant qu'une touche soit pressée
    public static void Pause()
    {
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");  // Message à l'utilisateur
        Console.ReadKey();  // Attend qu'une touche soit pressée
    }

    // Lit un entier depuis la console avec validation
    public static int ReadInt(string label)
    {
        int value;
        while (true)  // Boucle infinie jusqu'à ce que l'utilisateur fournisse un entier valide
        {
            Console.Write(label);  // Affiche le message de saisie
            if (int.TryParse(Console.ReadLine(), out value))  // Tente de convertir l'entrée en entier
                return value;  // Retourne l'entier si la conversion réussit

            WriteError("Veuillez entrer un nombre valide.");  // Message d'erreur en rouge
        }
    }

    // Lit une chaîne obligatoire depuis la console avec validation
    public static string ReadRequiredString(string label)
    {
        while (true)  // Boucle jusqu'à ce que l'utilisateur saisisse quelque chose de valide
        {
            Console.Write(label);  // Affiche le message de saisie
            string? input = Console.ReadLine();  // Lit la ligne saisie par l'utilisateur

            if (!string.IsNullOrWhiteSpace(input))  // Vérifie que l'entrée n'est pas vide ou uniquement des espaces
                return input.Trim();  // Supprime les espaces superflus et retourne la chaîne

            WriteError("Ce champ est obligatoire.");  // Message d'erreur en rouge
        }
    }

    // Affiche un titre formaté pour la console
    public static void WriteTitle(string title)
    {
        Console.WriteLine($"=== {title.ToUpper()} ===\n");  // Transforme le texte en majuscules et ajoute des signes décoratifs
    }

    // Affiche un message d'erreur en rouge
    public static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;  // Change la couleur du texte en rouge
        Console.WriteLine(message);  // Affiche le message
        Console.ResetColor();  // Réinitialise la couleur à celle par défaut
    }

    // Affiche un message de succès en vert
    public static void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;  // Change la couleur du texte en vert
        Console.WriteLine(message);  // Affiche le message
        Console.ResetColor();  // Réinitialise la couleur
    }
}

