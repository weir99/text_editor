namespace Controllers;

using System; using Commands; using Models;

public class KeyboardController : Controller{

    Status status; //Used to modify command handling based off of status

    public KeyboardController(Status status) => this.status = status;

    public override Command getCommand()
    {
        ConsoleKeyInfo cki;
        if (Console.KeyAvailable) cki = Console.ReadKey(true);
        else return new NullCommand();
        return ProcessKey(cki);

    }

    private Command ProcessKey(ConsoleKeyInfo cki){
        if(cki.Key == ConsoleKey.Escape) return new NormalCommand();
        else if (status.stat == State.Insert) return ProcessInsert(cki);
        else if (status.stat == State.Command)return ProcessCommand(cki);// Handle command mode
        else return ProcessNormal(cki);
    }

    private Command ProcessInsert(ConsoleKeyInfo cki){
        if(cki.Key == ConsoleKey.Enter) return new NewLineCommand();

        if(cki.Key == ConsoleKey.Backspace || cki.Key == ConsoleKey.Delete) return new BackspaceCommand();

        if(cki.Key == ConsoleKey.UpArrow) return new MoveCommand{yMove = -1};
        if(cki.Key == ConsoleKey.DownArrow) return new MoveCommand{yMove = 1};
        if(cki.Key == ConsoleKey.LeftArrow) return new MoveCommand{xMove = -1};
        if(cki.Key == ConsoleKey.RightArrow) return new MoveCommand{xMove = 1};

        else return new CharCommand(cki.KeyChar);
    }
    
    private Command ProcessNormal(ConsoleKeyInfo cki){
        if(cki.KeyChar == 'q') return new QuitCommand();
        else return new NullCommand();
    }

    private Command ProcessCommand(ConsoleKeyInfo cki){
        return new NullCommand();
    }
}