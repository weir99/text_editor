namespace TestViews;
using Views; using Updates; using Models;

class TestView : View{
    Document doc;
    public override void update(Update toUpdate)
    {
        return; // Not to worried right now, just handling insert
    }

    public char readChar(int x, int y){
        return doc.Text.ElementAt(y)[x];
    }
}