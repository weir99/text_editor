namespace TestControllers;
using Controllers; using Commands; using System;

public class TestController : Controller{

    public Status state {get; private set;} = new Status();
    Queue<Command> inputs = new Queue<Command>();

    public override Command getCommand()
    {
        if(inputs.Any()){
            return inputs.Dequeue();
        }
        return new NullCommand();
    }
    
    public void move(int moveX, int moveY) => inputs.Enqueue(new MoveCommand(){xMove = moveX, yMove = moveY});

    public void backspace() => inputs.Enqueue(new BackspaceCommand());

    public void writeChar(char c) => inputs.Enqueue(new CharCommand(c));

    public void writeNewline(){
        inputs.Enqueue(new NewLineCommand());
    }

    public void Normal() => state.setNormal();
    public void Insert() => state.setInsert();

    public void Quit(){
        inputs.Enqueue(new QuitCommand());
    }

    public void writeString(string s){
        foreach(char c in s){
            writeChar(c);
        }
    }
}