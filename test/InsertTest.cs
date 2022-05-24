namespace test;
using TestControllers; using TestViews; using Models;



public class InsertTest1
{
    Document doc;
    TestView view;
    TestKeyboard controller;

    public InsertTest1(){
        doc = new Document();
        view = new TestView(doc);
        controller = new TestKeyboard();
        doc.addView(view);
        doc.addController(controller);
    }

    [Fact]
    public void SingleChar()
    {   
        string expected = "c";
        controller.writeString(expected);
        controller.writeChar('q');
        doc.Operate();
        string actual = view.readLine(0);
        Assert.Equal(expected, actual);
        Assert.True(view.lineEmpty(1));
    }

    [Fact]
    public void MultiChar()
    {
        string expected = "Hello World! 123";
        controller.writeString(expected);
        controller.writeChar('q');
        doc.Operate();
        string actual = view.readLine(0);
        Assert.Equal(expected, actual);
        Assert.True(view.lineEmpty(1));
    }
}