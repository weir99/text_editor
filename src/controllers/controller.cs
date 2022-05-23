namespace Controllers;
using Commands;
abstract class Controller{
    public abstract Command getCommand();
}