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

    public void writeChar(char c){
        inputs.Enqueue(new CharCommand(c));
    }

    public void writeNewline(){
        inputs.Enqueue(new NewLineCommand());
    }

    public void Normal(){
        state.setNormal();
    }

    public void Quit(){
        inputs.Enqueue(new QuitCommand());
    }

    public void writeString(string s){
        foreach(char c in s){
            writeChar(c);
        }
    }
}