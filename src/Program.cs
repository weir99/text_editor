// See https://aka.ms/new-console-template for more information
using Models; using Views; using Controllers;
Document doc = new Document();
ConsoleView view = new ConsoleView(doc);
doc.addView(view);
KeyboardController controller = new KeyboardController(doc.status);
doc.addController(controller);
doc.Operate();
