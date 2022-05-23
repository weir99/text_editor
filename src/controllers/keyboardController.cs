namespace Controllers;

using System; using Commands;

class KeyboardController : Controller{
    public override Command getCommand()
    {
        ConsoleKeyInfo cki;
        if (Console.KeyAvailable) cki = Console.ReadKey();
        else return new NullCommand();
        return new CharCommand(((char)cki.Key));
    }
}