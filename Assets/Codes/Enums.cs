public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class DirecWay
{
    public DirecWay(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public  int x;
    public  int y;
}

public static class DirectionValue
{
    public static DirecWay Up = new DirecWay(0, 1);

    public static DirecWay Down = new DirecWay(0, -1);

    public static DirecWay Left = new DirecWay(-1, 0);

    public static DirecWay Right = new DirecWay(1, 0);

    public static DirecWay theDrect(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                    return Up;
            case Direction.Down:
                    return Down;
            case Direction.Left:
                    return Left;
            case Direction.Right:
                    return Right;
            default: break;
        }

        return null;
    }
}