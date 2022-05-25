namespace test;
using TestControllers; using TestViews; using Models;



public class ToTest //Does basic setup for test
{
    protected Document doc;
    protected TestView view;
    protected TestController controller;

    public ToTest(){
        doc = new Document();
        controller = new TestController();
        view = new TestView(doc, controller.state);
        doc.addView(view);
        doc.addController(controller);
    }
}