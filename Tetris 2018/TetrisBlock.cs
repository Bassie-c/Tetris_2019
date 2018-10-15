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
    Texture2D emptyCell;
    public int x;
    public int y;


    /// <summary>
    /// The constructor of the TetrisBlock. Creates an array of false bools, notes the color of the block and loads the textures.
    /// </summary>
    /// <param name="color">The color of the block</param>
    /// <param name="x">The size of the </param>
    /// <param name="y"></param>
    public TetrisBlock(int arrayX, int arrayY, Color color)
    {
        block = new bool[arrayX, arrayY];
        for (int x = 0; x <= arrayX - 1; x++)
        {
            for (int y = 0; y <= arrayY - 1; y++)
            {
                block[x, y] = false;
            }
        }
        this.color = color;
        x = y = 0;
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
    }

    public void MoveLeft()
    {
        if (x > 0)
            x--;
    }

    public void MoveRight()
    {
        if (x < 10 - block.GetLength(0))
            x++;
    }

    public void MoveDown()
    {
        if (y < 20)
            y++;
    }

    /// <summary>
    /// Rotates the block clockwise.
    /// </summary>
    public void RotateClockwise()
    {
        bool[,] rotatedBlock = new bool[block.GetLength(1), block.GetLength(0)];
        for (int x = 0; x < block.GetLength(0); x++)
        {
            for (int y = 0; y < block.GetLength(1); y++)
            {
                if (block[x, y])
                    rotatedBlock[rotatedBlock.GetLength(0) - 1 - y, x] = true;
            }
        }

        block = rotatedBlock;

        if (x > 10 - block.GetLength(0))
            x--;
    }

    /// <summary>
    /// Rotates the block counterclockwise
    /// </summary>
    public void RotateCounterclockwise()
    {
        for (int i = 0; i < 3; i++)
            RotateClockwise();
    }

    /// <summary>
    /// Draws the block
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Vector2 position = new Vector2(x * emptyCell.Width, y * emptyCell.Height);
        for (int x = 0; x < block.GetLength(0); x++)
        {
            for (int y = 0; y < block.GetLength(1); y++)
            {
                if (block[x, y])
                    spriteBatch.Draw(emptyCell, position + new Vector2(x * emptyCell.Width, y * emptyCell.Height), color);
            }
        }
    }

}

class IBlock : TetrisBlock
{
    public IBlock() : base(1, 4, Color.LightBlue)
    {
        block[0, 0] = true;
        block[0, 1] = true;
        block[0, 2] = true;
        block[0, 3] = true;
    }
}

class JBlock : TetrisBlock
{
    public JBlock() : base(2, 3, Color.DarkBlue)
    {
        block[1,0] = true;
        block[1,1] = true;
        block[1,2] = true;
        block[0,2] = true;
    }
}

class LBlock : TetrisBlock
{
    public LBlock() : base(2, 3, Color.Orange)
    {
        block[0, 0] = true;
        block[0, 1] = true;
        block[0, 2] = true;
        block[1, 2] = true;
    }
}

class OBlock : TetrisBlock
{
    public OBlock() : base(2, 2, Color.Yellow)
    {
        block[0,0] = true;
        block[0,1] = true;
        block[1,0] = true;
        block[1,1] = true;
    }
}

class SBlock : TetrisBlock
{
    public SBlock() : base(3, 2, Color.Green)
    {
        block[1,0] = true;
        block[2,0] = true;
        block[0,1] = true;
        block[1,1] = true;
    }
}

class TBlock : TetrisBlock
{
    public TBlock() : base(3, 2, Color.Purple)
    {
        block[0,0] = true;
        block[1,0] = true;
        block[2,0] = true;
        block[1,1] = true;
    }
}

class ZBlock : TetrisBlock
{
    public ZBlock() : base(3, 2, Color.Red)
    {
        block[0,0] = true;
        block[1,0] = true;
        block[1,1] = true;
        block[2,1] = true;
    }
}