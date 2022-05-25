namespace TestViews;
using Views; using Updates; using Models; using Controllers;

public class TestView : View{
    Document doc;
    Status state;

    public TestView(Document doc, Status state){
        this.doc = doc;
        this.state = state;
    }
    public override void update(Update toUpdate)
    {
        return; // Not to worried right now, just handling insert
    }

    public char readChar(int x, int y){
        return doc.Text.ElementAt(y)[x];
    }

    public string readLine(int y){
        return doc.Text.ElementAt(y);
    }

    public bool lineEmpty(int y){
        return doc.Text.ElementAtOrDefault(y) is null;
    }

}