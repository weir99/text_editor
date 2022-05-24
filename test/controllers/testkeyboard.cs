namespace TestControllers;
using Controllers; using Commands; using System;

class TestKeyboard : Controller{
    Queue<ConsoleKeyInfo> inputs = new Queue<ConsoleKeyInfo>();

    public override Command getCommand()
    {
        if(inputs.Any()){
            return new KeyCommand(inputs.Dequeue());
        }
        return new NullCommand();
    }

    public void writeChar(char c, bool shift, bool alt, bool control){
        // Hopefully this works
        ConsoleKey key = (ConsoleKey) c;
        ConsoleKeyInfo cki = new ConsoleKeyInfo(c, key, shift, alt, control);
        inputs.Enqueue(cki);
    }

    public void writeChar(char c){
        writeChar(c, false, false, false);
    }
}