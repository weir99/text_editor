namespace Commands;
using Models;
public class CharCommand : UndoCommand{
    public char c;
    public Cursor pos; //keep track of where insertion was made
    bool done = false; // Don't undo if we haven't executed the command yet
    public CharCommand(Document doc, char c): base(doc){
        this.c = c;
        this.pos = doc.Position;
    }
    public override void @do()
    {
        doc.Insert(c);
        pos = new Cursor(doc.Position); //Want to get the position after the insert happened
        done = true;
    }
    public override void undo()
    {
        // If we haven't yet executed the command, don't undo it.
        // Similarly, if we can't move to where the command was done, don't undo it
        if(!done || !doc.MoveTo(pos)) return;
        doc.Backspace();
    }
}

public class NewLineCommand : Command{}


public class BackspaceCommand : Command{}

public class QuitCommand : Command{}

public class MoveCommand : Command{
    public int xMove = 0; public int yMove = 0;
}


// Just tells document to pass update along to the views
// Want document to mostly handle stuff like this,
// but for stuff like updates to status, or potentially scrolling, these two can handle it
public class ViewCommand : DoCommand{
    public Updates.Update update;
    public ViewCommand(Document doc): base(doc){
        update = new Updates.WholeUpdate();
    }
    public ViewCommand(Document doc, Updates.Update update): base(doc){
        this.update = update;
    }

    public override void @do()
    {
        doc.updateViews(update);
    }
}

// Stores multiple commands in a Queue
public class CombinedCommand : Command{
    public Queue<Command> commands;
    public CombinedCommand(){
        commands = new Queue<Command>();   
    }
    public CombinedCommand(params Command[] commands){
        this.commands = new Queue<Command>();
        foreach (Command command in commands){
            this.commands.Enqueue(command);
        }
    }
}