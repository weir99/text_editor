abstract class Model{
    private List<View> Views = new List<View>();
    private Controller? Controller;

    protected virtual void addView(View newView){
        Views.Add(newView);
    }

    protected virtual void addController(Controller newController){
        Controller = newController;
    }

    // Gets    
    protected Action getAction(){
        if (Controller is null){
            return new NullAction();
        }
        return Controller.getAction();
    }

    
    // Update all the views
    protected virtual void updateViews(Update update){
        foreach (View view in Views){
            view.update(update);
        }
    }
}