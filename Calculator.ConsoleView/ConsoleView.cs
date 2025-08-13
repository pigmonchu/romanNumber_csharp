namespace Calculator.ConsoleView;

public class ConsoleView
{
    private readonly MenuComposer _composer;

    public ConsoleView(MenuComposer? composer = null)
    {
        _composer = composer ?? new MenuComposer();
    }

    public MenuOption MostrarMenu()
    {
        return _composer.MostrarMenu(Console.In, Console.Out);
    }
}