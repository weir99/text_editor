namespace Views;
using Models; using Updates; using System;

public class ConsoleView : View{
    Document? doc;

    public ConsoleView(Document doc){
        this.doc = doc;
        writeDoc();
    }

    public override void update(Update toUpdate)
    {
        if(toUpdate is InsertUpdate) writeDoc();
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
        updateCursor();
    }

    private void updateCursor(){
        Console.SetCursorPosition(doc.Position.xPosition, doc.Position.yPosition);
    }
}