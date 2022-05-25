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