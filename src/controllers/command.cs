namespace Commands;
using Models;

// Abstract type for handline actions
// Will probably just be keypresses for now, but may want more complex (e.g. mouse movement) later
public abstract class Command{
}

// If for whatever reason we don't have an action to pass along, pass this
public class NullCommand : Command{}

// 
// Below are the commands that we'll actually execute
//


//Basic command, does a thing, can't be undone
public abstract class DoCommand : Command{
    // The document to execute the command in
    protected Document doc;
    public DoCommand(Document doc){
        this.doc = doc;
    }
    public abstract void @do();
}

// More complex command, can be undone
public abstract class UndoCommand : DoCommand{
    public UndoCommand(Document doc): base(doc){}
    public abstract void @undo();
}



