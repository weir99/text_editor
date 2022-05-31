namespace test;
using TestControllers; using TestViews; using Models;



public class CursorTest1 : ToTest
{
    [Fact]
    public void HorizontalMove()
    {   
        string expected = "Hello World!";
        controller.move(3,1);
        controller.writeString("Hello Worl!");
        controller.move(-1,0);
        controller.writeString("d");
        controller.Quit();
        doc.Operate();
        Assert.Equal(expected, view.readLine(0));
    }

    [Fact]
    public void VerticalMove()
    {
        controller.writeString("Hiya!");
        controller.writeNewline();
        controller.writeString("Earth!");
        controller.move(0,-1);
        controller.writeChar('?');
        controller.writeNewline();
        controller.move(0,2);
        controller.writeChar('*');
        controller.move(0,-6);
        controller.writeChar('$');
        controller.move(0,1);
        controller.writeChar('f');
        controller.move(0,5);
        controller.writeChar('B');
        controller.Quit();
        doc.Operate();
        Assert.Equal("H$iya!?", view.readLine(0));
        Assert.Equal("f", view.readLine(1));
        Assert.Equal("*BEarth!", view.readLine(2));
        Assert.True(view.lineEmpty(3));
    }
}