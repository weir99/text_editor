namespace Commands;
public class KeyCommand : Command{
    public ConsoleKeyInfo cki;
    public KeyCommand(ConsoleKeyInfo cki) => this.cki = cki;
}