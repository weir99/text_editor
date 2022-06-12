namespace Models;
public class Cursor{

    public int xPosition {get; set;}
    public int yPosition {get; set;}

    public Cursor(Cursor otherCursor){
        this.xPosition = otherCursor.xPosition;
        this.yPosition = otherCursor.yPosition;
    }
}

