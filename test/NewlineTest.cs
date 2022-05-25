namespace test;
using TestControllers; using TestViews; using Models;



public class NewlineTest1 : ToTest
{
    [Fact]
    public void SingleLine()
    {   
        controller.writeString("Hello");
        controller.writeNewline();
        controller.writeString("World!");
        controller.Quit();
        doc.Operate();
        Assert.Equal(view.readLine(0), "Hello");
        Assert.Equal(view.readLine(1), "World!");
        Assert.True(view.lineEmpty(2));
    }
}