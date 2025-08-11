using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace Calculator.ConsoleView.Tests;

public class ConsoleViewTests
{
    [Fact]
    public void MostrarMenu_1_Devuelve_Iniciar()
    {
        Console.SetIn(new StringReader("1\n"));
        
        Console.SetOut(new StringWriter());

        var vista = new ConsoleView();

        var opt = vista.MostrarMenu();

        Assert.Equal(MenuOption.Iniciar, opt);
    }

    [Theory]
    [InlineData("4\n2\n", MenuOption.Seguir)] // primero inválido (4), luego 2 → Seguir
    [InlineData("k\n3\n", MenuOption.Salir)]

    public void MostrarMenu_opciones_incorrectas_vuelve_a_pedir(String input, MenuOption resultado)
    {
        Console.SetIn(new StringReader(input));
        StringWriter output = new StringWriter();
        Console.SetOut(output);

        var vista = new ConsoleView();
        var opt = vista.MostrarMenu();

        Assert.Equal(resultado, opt);
        Assert.Contains("Opcion incorrecta", output.ToString());
    }
}
