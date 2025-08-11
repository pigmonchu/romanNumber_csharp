using System;
using System.Runtime.InteropServices;

namespace Calculator.ConsoleView;

public class ConsoleView
{
    public MenuOption MostrarMenu() 
    {
        Console.WriteLine("CALCULATUM");
        Console.WriteLine("==========");

        foreach (MenuOption opt in Enum.GetValues(typeof(MenuOption))) {
            Console.WriteLine($"{(int)opt} - {opt}");
        }

        Console.WriteLine("");
        do
        {
            Console.Write("Seleccione opción: ");
            var option = Console.ReadLine();

            if (int.TryParse(option, out int numero) && Enum.IsDefined(typeof(MenuOption), numero))
            {
                return (MenuOption)numero;
            }
            Console.WriteLine("Opcion incorrecta");
        } while (true);

    }
}