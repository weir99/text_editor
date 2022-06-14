// See https://aka.ms/new-console-template for more information
using Models; using Views; using Controllers;
Document doc = new Document();
KeyboardController controller = new KeyboardController(doc);
doc.addController(controller);
ConsoleView view = new ConsoleView(doc, controller.status);
doc.addView(view);
doc.Operate();
