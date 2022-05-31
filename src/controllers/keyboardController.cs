namespace Controllers;

using System; using Commands; using Models;

public class KeyboardController : Controller{

    public Status status {get; private set;} //Used to modify command handling based off of status
    public KeyboardController(){
        status = new Status();
        status.setNormal();
    }
    public KeyboardController(Status status) => this.status = status;

    public override Command getCommand()
    {
        ConsoleKeyInfo cki;
        if (Console.KeyAvailable) cki = Console.ReadKey(true);
        else return new NullCommand();
        return ProcessKey(cki);
    }

    private Command ProcessKey(ConsoleKeyInfo cki){
        if(cki.Key == ConsoleKey.Escape){
            status.setNormal();
            return new ViewCommand();
        }
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
        if(cki.KeyChar == 'k') return new MoveCommand{yMove = -1};
        if(cki.KeyChar == 'j') return new MoveCommand{yMove = 1};
        if(cki.KeyChar == 'h') return new MoveCommand{xMove = -1};
        if(cki.KeyChar == 'l') return new MoveCommand{xMove = 1};
        if(cki.KeyChar == 'i'){
            status.setInsert();
            return new ViewCommand();
        }
        if(cki.KeyChar == 'a'){
            status.setInsert();
            return new CombinedCommand(new MoveCommand{xMove = 1}, new ViewCommand());
        }
        if(cki.KeyChar == ':'){
            status.setCommand(":");
            return new ViewCommand();
        }
        else return new NullCommand();
    }

    private Command ProcessCommand(ConsoleKeyInfo cki){
        if (cki.Key == ConsoleKey.Enter){
            string input = status.statDisplay.Remove(0,1);
            status.setInsert();
            //Want to send ViewCommand as we updated status
            return new CombinedCommand(new ViewCommand(), ParseCommand(input));
        }
        if (cki.Key == ConsoleKey.Backspace || cki.Key == ConsoleKey.Delete){
            if(status.statDisplay.Length == 1) status.setNormal();
            else status.setCommand(status.statDisplay.Remove(status.statDisplay.Length-1));
        }
        else{
            status.setCommand(status.statDisplay + cki.KeyChar);
        }
        return new ViewCommand();
    }

    private Command ParseCommand(String input){
        if(input == "q") return new QuitCommand();
        else return new NullCommand();
    }
}