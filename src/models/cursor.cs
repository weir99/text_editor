class Cursor{
    private Coordinate location;
    private Bound2D bounds;

    // Moves cursor to adjusted valid position
    public void move(Coordinate moveTo){
        location = fitBounds2D(moveTo, bounds);
    }

    public void moveAmount(int dX, int dY){
        Coordinate moveTo = location;
        moveTo.x += dX;
        moveTo.y += dY;
        move(moveTo);
    }

    public void moveUp() => moveAmount(0,-1);
    public void moveDown() => moveAmount(0,1);
    public void moveLeft() => moveAmount(-1,0);
    public void moveRight() => moveAmount(1,0);


    // Adjusts target.x and target.y to closest values such that
    // range.minY <= target.Y <= range.maxY
    // range.minX <= target.X <= range.maxX
    private Coordinate fitBounds2D(Coordinate position, Bound2D bounds){
        Coordinate adjusted;
        adjusted.x = fitBound(position.x, bounds.xBound);
        adjusted.y = fitBound(position.y, bounds.yBound);
        return adjusted;
    }

    private int fitBound(int num, Bound range){
        if(num < range.min) return range.min;
        else if (range.max < num) return range.max;
        else return num;
    }
}

// Refers to a position in x,y coordinates
struct Coordinate{
    public int x;
    public int y;
}

// Defines (inclusive) upper and lower bounds on an integer
struct Bound{
    public int min;
    public int max;
}

// Defines (inclusive) bounds on position, using coordinate system from above
struct Bound2D{
    public Bound xBound;
    public Bound yBound;
}