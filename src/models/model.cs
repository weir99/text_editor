namespace Models;

using Views; using Controllers; using Commands; using Updates;

public abstract class Model{
    private List<View> Views = new List<View>();
    private Controller? Controller;

    public virtual void addView(View newView){
        Views.Add(newView);
    }

    public virtual void addController(Controller newController){
        Controller = newController;
    }

    // Gets    
    protected Command getCommand(){
        if (Controller is null){
            return new NullCommand();
        }
        return Controller.getCommand();
    }

    
    // Update all the views
    protected virtual void updateViews(Update update){
        foreach (View view in Views){
            view.update(update);
        }
    }
}