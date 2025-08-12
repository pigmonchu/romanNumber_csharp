using Calculator.ConsoleView;

ConsoleView vista = new ConsoleView();
MenuOption opt = vista.MostrarMenu();
Console.WriteLine($"Has elegido {(int)opt} -> {opt}");