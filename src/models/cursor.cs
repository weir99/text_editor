namespace Models;
public class Cursor{

    public int xPosition {get; set;}
    public int yPosition {get; set;}
    public Cursor(int xPosition, int yPosition){
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    }
    public Cursor(Cursor otherCursor){
        this.xPosition = otherCursor.xPosition;
        this.yPosition = otherCursor.yPosition;
    }
}

