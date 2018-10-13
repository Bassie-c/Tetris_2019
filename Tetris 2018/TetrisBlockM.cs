using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TetrisBlockM
{
    public bool[,] block;
    public Color color;
    Texture2D emptyCell;
    public Vector2 position;
    public int velocity;
    bool isFalling;

    public TetrisBlockM()
    {
        block = new bool[4, 4];
        block[0, 0] = true;
        block[0, 1] = true;
        block[1, 1] = true;
        block[1, 2] = true;

        color = Color.Red;
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        isFalling = true;
        position = Vector2.Zero;
        velocity = 1;
    }

    public void Update()
    {
        position.Y += velocity;

        if (!isFalling)
        {
            TetrisBlockM Block = new TetrisBlockM();
            isFalling = true;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (position.Y % emptyCell.Height == 0)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (block[i, j])
                    {
                        Vector2 position2 = new Vector2(position.X + i * emptyCell.Width, position.Y + j * emptyCell.Height);
                        spriteBatch.Draw(emptyCell, position2, color);
                    }
                }
        }
        else
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (block[i, j])
                    {
                        float previousY = position.Y - position.Y % emptyCell.Height;
                        Vector2 previousPosition = new Vector2(i * emptyCell.Width, previousY + j * emptyCell.Height);
                        spriteBatch.Draw(emptyCell, previousPosition, color);
                    }
                }

        }
    }    
}