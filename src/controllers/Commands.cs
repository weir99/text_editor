namespace Commands;
public class CharCommand : Command{
    public char c;
    public CharCommand(char c) => this.c = c;
}

public class NewLineCommand : Command{}

public class NormalCommand : Command{}