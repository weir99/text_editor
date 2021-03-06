namespace Controllers;

using System; using Commands; using Models;

public class KeyboardController : Controller{
    private string CommandBuffer = ""; //Handles multi-char commands in command and normal mode
    Document doc;
    public Status status {get; private set;} //Used to modify command handling based off of status
    public KeyboardController(Document doc){
        this.doc = doc;
        status = new Status();
        status.setNormal();
    }
    public KeyboardController(Document doc, Status status){
        this.doc = doc;
        this.status = status;
    }

    public override Command getCommand()
    {
        if (Console.KeyAvailable) return ProcessKey(Console.ReadKey(true));
        return new NullCommand();
    }

    private Command ProcessKey(ConsoleKeyInfo cki){
        if(cki.Key == ConsoleKey.Escape){
            CommandBuffer = "";
            status.setNormal();
            return new ViewCommand(doc);
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

        else return new CharCommand(doc, cki.KeyChar);
    }
    
    private Command ProcessNormal(ConsoleKeyInfo cki){
        CommandBuffer += cki.KeyChar;
        if(CommandBuffer == "k")return ClearBufferAndCommand(new MoveCommand{yMove = -1});
        if(CommandBuffer == "j") return ClearBufferAndCommand(new MoveCommand{yMove = 1});
        if(CommandBuffer == "h") return ClearBufferAndCommand(new MoveCommand{xMove = -1});
        if(CommandBuffer == "l") return ClearBufferAndCommand(new MoveCommand{xMove = 1});
        if(CommandBuffer == "u") return ClearBufferAndCommand(new ExecuteUndoCommand(doc));
        if(CommandBuffer == "i"){
            status.setInsert();
            return ClearBufferAndCommand(new ViewCommand(doc));
        }
        if(CommandBuffer == "a"){
            status.setInsert();
            return ClearBufferAndCommand(new CombinedCommand(new MoveCommand{xMove = 1}, new ViewCommand(doc)));
        }
        if(CommandBuffer == ":"){
            status.setCommand(":");
            return ClearBufferAndCommand(new ViewCommand(doc));
        }
        else return ClearBufferAndCommand(new NullCommand());
    }

    private Command ClearBufferAndCommand(Command command){
        CommandBuffer = "";
        return command;
    }

    private Command ProcessCommand(ConsoleKeyInfo cki){
        if (cki.Key == ConsoleKey.Enter){
            string input = status.statDisplay.Remove(0,1);
            status.setInsert();
            //Want to send ViewCommand as we updated status
            CommandBuffer = "";
            return new CombinedCommand(new ViewCommand(doc), ParseCommand(input));
        }
        if (cki.Key == ConsoleKey.Backspace || cki.Key == ConsoleKey.Delete){
            if(status.statDisplay.Length == 1)
            {   status.setNormal();
                CommandBuffer = ""; //Clear out the COmmandBuffer
            }
            else{
                CommandBuffer = CommandBuffer.Remove(CommandBuffer.Length-1);
                status.setCommand(":" + CommandBuffer);
            }
        }
        else{
            CommandBuffer += cki.KeyChar;
            status.setCommand(":" + CommandBuffer);
        }
        return new ViewCommand(doc);
    }

    private Command ParseCommand(String input){
        if(input == "q") return new QuitCommand();
        else return new NullCommand();
    }
}