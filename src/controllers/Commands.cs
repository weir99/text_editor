namespace Commands;
public class CharCommand : Command{
    public char c;
    public CharCommand(char c) => this.c = c;
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
public class ViewCommand : Command{
    public Updates.Update update;
    public ViewCommand(){
        update = new Updates.WholeUpdate();
    }
    public ViewCommand(Updates.Update update){
        this.update = update;
    }
}