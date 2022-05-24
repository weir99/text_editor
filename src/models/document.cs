namespace Models;
using Commands;

// Basic document, inherits from model, but shared with our views to make updating easier.
// Not really ideal implementation of MVC, but better than having a bunch of copies of text
// taking up memory

class Document : Model{
    // Holds the text being edited, may want to change to regular list
    LinkedList<string> Text {get;}

    // Contains information about where editing is taking place
    Cursor Position {get;}
    // Stores current line being worked on, be careful it stays in sync with cursor
    LinkedListNode<string> CurrentLine {get;}

    // Contains the current editing status
    Status status;

    // Check if document is currently being viewed/edited
    bool live;

    public Document(){
        Text = new LinkedList<string>();
        CurrentLine = new LinkedListNode<string>("");
        Text.AddFirst(CurrentLine);
        Position = new Cursor{xPosition = 0, yPosition = 0};
        status = new Status();
        status.setInsert(); //We'll just use insert mode for now
        live = false;
    }

    public void Operate(){
        live = true;
        Command c;
        while(live){
            c = getCommand();
        }
    }

    private void Insert(char c){
        // Updates current line
        CurrentLine.Value = CurrentLine.Value.Insert(Position.xPosition, c.ToString());
        // Move cursor one to the right
        ++ (Position.xPosition);
        updateViews();
    }

    private void HandleCommand(Command c){
        if (c is NullCommand) return;
        // Needs to be more complex, but we'll keep it simple for now
        if (c is KeyCommand) Insert(((KeyCommand) c).cki.KeyChar);
    }


}