namespace Calculator.ConsoleView;

public class MenuComposer
{
    public MenuOption MostrarMenu(TextReader input, TextWriter output)
    {
        output.WriteLine("CALCULATUM");
        output.WriteLine("==========");

        foreach (MenuOption opt in Enum.GetValues(typeof(MenuOption))) {
            output.WriteLine($"{(int)opt} - {opt}");
        }

        output.WriteLine("");
        do
        {
            output.Write("Seleccione opción: ");
            var option = input.ReadLine();

            if (option is null)
                return MenuOption.Salir;

            if (int.TryParse(option, out int numero) && Enum.IsDefined(typeof(MenuOption), numero))
                {
                    return (MenuOption)numero;
                }
            output.WriteLine("Opcion incorrecta");
        } while (true);
    }
}