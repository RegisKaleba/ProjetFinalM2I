using System;

namespace BlogConsole.UI;

public static class ConsoleHelper
{
    public static void Clear()
    {
        Console.Clear();
    }

    public static void Pause()
    {
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public static int ReadInt(string label)
    {
        int value;
        while (true)
        {
            Console.Write(label);
            if (int.TryParse(Console.ReadLine(), out value))
                return value;

            WriteError("Veuillez entrer un nombre valide.");
        }
    }

    public static string ReadRequiredString(string label)
    {
        while (true)
        {
            Console.Write(label);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            WriteError("Ce champ est obligatoire.");
        }
    }

    public static void WriteTitle(string title)
    {
        Console.WriteLine($"=== {title.ToUpper()} ===\n");
    }

    public static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
