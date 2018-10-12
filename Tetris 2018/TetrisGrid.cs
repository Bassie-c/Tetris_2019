using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    /// De kleurarray om blokken in de grid te tekenen.
    Color[,] colorGrid;

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        position = Vector2.Zero;
        colorGrid = new Color[Width, Height];

        /// Zet alle elementen op blank.
        for (int i = 0; i < colorGrid.GetLength(0); i++)
            for (int j = 0; j < colorGrid.GetLength(1); j++)
                colorGrid[i, j] = Color.White; 
 
        Clear();
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Teken grid
        for (int i = 0; i < Width; i++)
        {
            position.X = i * emptyCell.Width;
            for (int j = 0; j < Height; j++)
            {
                position.Y = j * emptyCell.Height;
                spriteBatch.Draw(emptyCell, position, colorGrid[i,j]);
            }
        }
    }

    public void PlaceBlock(TetrisBlockM block)
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                if (block.Block[i, j])
                    colorGrid[((int)position.X) + i, ((int)position.Y) + j] = block.Color;
            }
    }

    /*
    public void PlaceBlock(TetrisBlockM block)
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                if (block.Block[i, j])
                    colorGrid[((int)position.X) + i, ((int)position.Y) + j] = block.Color;
                    
            }
                
    }
    */

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}