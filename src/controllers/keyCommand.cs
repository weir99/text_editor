namespace Commands;
public class KeyCommand : Command{
    ConsoleKeyInfo cki;
    public KeyCommand(ConsoleKeyInfo cki) => this.cki = cki;
}