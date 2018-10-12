using Microsoft.Xna.Framework;

class TetrisBlockM
{
    Vector2 position;

    public TetrisBlockM()
    {
        Block = new bool[4, 4];
    }

    public bool[,] Block
    {
        get { return Block; }
        set { Block = value; }
    }

    public Color Color
    {
        get { return Color; }
        set { Color = value; }
    }

}