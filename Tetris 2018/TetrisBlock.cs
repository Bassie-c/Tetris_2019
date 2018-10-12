using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TetrisBlock
{
    protected bool[,] block;
    protected Color color;
    protected Vector2 position;

    public TetrisBlock(Color color)
    {
        for (int x = 0; x <= 3; x++)
            for (int y = 0; y <= 3; y++)
                block[x, y] = false;
        this.color = color;
        position = Vector2.Zero;
    }

    /// <summary>
    /// Rotates the block clockwise.
    /// </summary>
    void RotateBlockClockwise()
    {
        bool[,] rotatedBlock = new bool[block.GetLength(1), block.GetLength(0)];
        for (int x = 0; x < block.GetLength(0); x++)
        {
            for (int y = 0; y < block.GetLength(1); y++)
            {
                if (block[x, y])
                    rotatedBlock[rotatedBlock.GetLength(0) - y, x] = true;
            }
        }
        block = rotatedBlock;
    }

    /// <summary>
    /// Rotates the block counterclockwise
    /// </summary>
    void RotateBlockCounterClockwise()
    {
        for (int i = 0; i < 3; i++)
            RotateBlockClockwise();
    }

}

class IBlock : TetrisBlock
{
    public IBlock() : base(Color.LightBlue)
    {
        block[0, 0] = true;
        block[0, 1] = true;
        block[0, 2] = true;
        block[0, 3] = true;
    }
}

class JBlock : TetrisBlock
{
    public JBlock() : base(Color.DarkBlue)
    {
        block[1,0] = true;
        block[1,1] = true;
        block[1,2] = true;
        block[0,2] = true;
    }
}

class LBlock : TetrisBlock
{
    public LBlock() : base(Color.Orange)
    {
        block[0,0] = true;
        block[0,1] = true;
        block[0,2] = true;
        block[1,2] = true;
    }
}

class OBlock : TetrisBlock
{
    public OBlock() : base(Color.Yellow)
    {
        block[0,0] = true;
        block[0,1] = true;
        block[1,0] = true;
        block[1,1] = true;
    }
}

class SBlock : TetrisBlock
{
    public SBlock() : base(Color.Green)
    {
        block[1,0] = true;
        block[2,0] = true;
        block[0,1] = true;
        block[1,1] = true;
    }
}

class TBlock : TetrisBlock
{
    public TBlock() : base(Color.Purple)
    {
        block[0,0] = true;
        block[0,1] = true;
        block[0,2] = true;
        block[1,1] = true;
    }
}

class ZBlock : TetrisBlock
{
    public ZBlock() : base(Color.Red)
    {
        block[0,0] = true;
        block[1,0] = true;
        block[1,1] = true;
        block[1,2] = true;
    }
}