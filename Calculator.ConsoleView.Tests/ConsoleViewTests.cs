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

    [Fact]
    public void MenuComposerMuestraCabeceraYOpciones()
    {
        var input = new StringReader("1\n");
        var output = new StringWriter();

        var composer = new MenuComposer();
        var option = composer.MostrarMenu(input, output);

        Assert.Equal(MenuOption.Iniciar, option);
        Assert.Contains("1 - Iniciar", output.ToString());
        Assert.Contains("2 - Seguir", output.ToString());
        Assert.Contains("3 - Salir", output.ToString());
    }

    [Fact]
    public void MenuComposerSeleccion_Valida_Directa_Devuelve_Iniciar()
    {
        var input = new StringReader("0\n1\n");
        var output = new StringWriter();

        var composer = new MenuComposer();
        var option = composer.MostrarMenu(input, output);

        Assert.Equal(MenuOption.Iniciar, option);
        Assert.Contains("Opcion incorrecta", output.ToString());
    }

    [Fact]
    public void MenuComposerSeleccion_Invalida_Luego_Valida_Devuelve_Seguir()
    {
        var input = new StringReader("0\n2\n");
        var output = new StringWriter();

        var composer = new MenuComposer();
        var option = composer.MostrarMenu(input, output);

        Assert.Equal(MenuOption.Seguir, option);
        Assert.Contains("Opcion incorrecta", output.ToString());
    }

    [Fact]
    public void MenuComposerRepite_Hasta_Valida_Conteo_De_Errores()
    {
        var input = new StringReader("0\nkk\n3\n");
        var output = new StringWriter();

        var composer = new MenuComposer();
        var option = composer.MostrarMenu(input, output);

        Assert.Equal(MenuOption.Salir, option);
        Assert.Contains("Opcion incorrecta", output.ToString());
        var count = output.ToString().Split("Opcion incorrecta").Length - 1;

        Assert.Equal(2, count);
    }

    [Fact]
    public void Delegacion_Devuelve_Lo_Que_Composer_Devuelve()
    {
        
    }
}
