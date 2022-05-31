namespace Views;
using Models; using Updates; using System; using Controllers;

public class ConsoleView : View{
    Document? doc;
    Status? state;

    public ConsoleView(Document doc, Status state){
        this.doc = doc;
        this.state = state;
        writeDoc();
    }

    public override void update(Update toUpdate)
    {
        if(toUpdate is WholeUpdate) writeDoc();
    }

    // Writes the entire document, we'll just use this for now
    private void writeDoc(){
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        if(doc is not null){
            foreach(var Line in doc.Text){
                Console.WriteLine(Line);
            }
        }
        writeStatus();
        updateCursor();
    }

    private void writeStatus(){
        if(state is not null){
            Console.SetCursorPosition(0, Console.WindowHeight);
            Console.Write(state.statDisplay);
            if(doc is not null){ //Print cursor position for debugging
                Console.Write("\t");
                Console.Write($"{doc.Position.xPosition}, {doc.Position.yPosition}");
            }
        }
        updateCursor(); 
    }

    private void updateCursor(){
        if(doc is not null) Console.SetCursorPosition(doc.Position.xPosition, doc.Position.yPosition);
    }
}