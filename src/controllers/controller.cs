namespace Controllers;

using Commands;

public abstract class Controller{
    public abstract Command getCommand();
}