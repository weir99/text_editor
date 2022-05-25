namespace test;
using TestControllers; using TestViews; using Models;



public class InsertTest1 : ToTest
{
    [Fact]
    public void SingleChar()
    {   
        string expected = "c";
        controller.writeString(expected);
        controller.Quit();
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
        controller.Quit();
        doc.Operate();
        string actual = view.readLine(0);
        Assert.Equal(expected, actual);
        Assert.True(view.lineEmpty(1));
    }
}