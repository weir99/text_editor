namespace Models;

// Contains info on current editing status, probably going to keep pretty basic for now
public class Status{
    public const string INSERT_STATUS = "INSERT";
    public const string NORMAL_STATUS = "NORMAL"; 

    private string statDisplay = NORMAL_STATUS;
    private State stat  = State.Normal;

    public void setInsert(){
        statDisplay = INSERT_STATUS; 
        stat = State.Insert;
    }
    public void setNormal(){
        statDisplay = NORMAL_STATUS;
         stat = State.Normal; 
    }

    public void setCommand(string commandText){
        statDisplay = commandText;
        stat = State.Command;
    }

}

enum State{
    Insert,
    Normal,
    Command
}

