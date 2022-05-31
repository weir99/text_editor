namespace Models;
using Commands; using Updates;

// Basic document, inherits from model, but shared with our views to make updating easier.
// Not really ideal implementation of MVC, but better than having a bunch of copies of text
// taking up memory

public class Document : Model{
    // Holds the text being edited, may want to change to regular list
    public LinkedList<string> Text {get;private set;}

    // Contains information about where editing is taking place
    // Might want to move over to controller, but this way easy to ensure cursor is always in a valid location
    public Cursor Position {get; private set;}
    // Stores current line being worked on, be careful it stays in sync with cursor
    public LinkedListNode<string> CurrentLine {get; private set;}

    // Check if document is currently being viewed/edited
    bool live;

    public Document(){
        Text = new LinkedList<string>();
        CurrentLine = new LinkedListNode<string>("");
        Text.AddFirst(CurrentLine);
        Position = new Cursor{xPosition = 0, yPosition = 0};
        live = false;
    }

    public void Operate(){
        live = true;
        Command c;
        while(live){
            c = getCommand();
            HandleCommand(c);
        }
    }

    private void Insert(char c){
        // Updates current line
        CurrentLine.Value = CurrentLine.Value.Insert(Position.xPosition, c.ToString());
        // Move cursor one to the right
        ++ (Position.xPosition);
    }

    private void NewLine(){
        CurrentLine = Text.AddAfter(CurrentLine, "");
        ++(Position.yPosition);
        Position.xPosition = 0;
    }

    private void Quit(){
        live = false;
    }

    private void MoveCursor(MoveCommand movement){
        MoveVertical(movement.yMove);
        MoveHorizontal(movement.xMove);
    }

    private void MoveVertical(int movement){
        // Gets direction to move
        if(movement  == 0) return;
        int direction;
        bool movingUp;
        Func<bool> moveCommand;
        if(movement > 0){
            direction = 1;
            movingUp = false;
            moveCommand = () =>{
                if(CurrentLine.Next is null) return false; //Shouldn't happen but just in case
                CurrentLine = CurrentLine.Next;
                return true;
            };
        } 
        else if(movement < 0){
            direction = -1;
            movingUp = true;
            moveCommand = () =>{
                if(CurrentLine.Previous is null) throw new InvalidOperationException();
                CurrentLine = CurrentLine.Previous;
                return true;
            };
        }
        else return;


        LinkedListNode<String>? end; //Don't go past this point
        if(movingUp) end = Text.First;
        else end = Text.Last;
        if(end is null) return;

        for(int i = 0; i < Math.Abs(movement); ++i){
            if(CurrentLine == end) break; //If we have excess move commands, stop moving
            if(!moveCommand()) break; //If for some reason moveCommand failed, stop moving
            Position.yPosition += direction;
        }
        Position.xPosition = Math.Min(CurrentLine.Value.Length, Position.xPosition);
    }

    private void MoveHorizontal(int movement){
        if(movement == 0 ) return;
        bool movingRight = movement > 0 ? true : false;
        if(movingRight) Position.xPosition = Math.Min(Position.xPosition + movement, CurrentLine.Value.Length);
        else Position.xPosition = Math.Max(Position.xPosition + movement, 0);
    }


    private void Backspace(){
        //If we're deleting a newline 
        if(Position.xPosition == 0){
            //Don't do anything if deleting start of document
            if(CurrentLine == Text.First) return;

            LinkedListNode<String>? previous = CurrentLine.Previous;
            if(previous is null) throw new InvalidOperationException();
            Position.yPosition --;
            Position.xPosition = previous.Value.Length;
            (previous.ValueRef) += CurrentLine.Value;
            Text.Remove(CurrentLine);
            CurrentLine = previous;
            return;
        }


        CurrentLine.ValueRef = CurrentLine.Value.Remove(Position.xPosition-1, 1);

        //Adjust cursor if needed
        Position.xPosition = Math.Min(Position.xPosition, CurrentLine.Value.Length);
        Position.xPosition = Math.Max(Position.xPosition, 0);

    }

    private void HandleCommand(Command c){
        if (c is NullCommand) return;
        // Needs to be more complex, but we'll keep it simple for now
        if (c is CharCommand) Insert(((CharCommand)c).c);
        else if (c is NewLineCommand) NewLine();
        else if (c is QuitCommand) Quit();
        else if (c is ViewCommand) updateViews(((ViewCommand)c).update);
        else if (c is MoveCommand) MoveCursor((MoveCommand) c);
        else if (c is BackspaceCommand) Backspace();
        else if (c is CombinedCommand){
            foreach(Command command in ((CombinedCommand) c).commands) HandleCommand(command);
        }
        updateViews(new WholeUpdate()); 
    }
}