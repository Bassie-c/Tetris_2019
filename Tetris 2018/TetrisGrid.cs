﻿using Microsoft.Xna.Framework;
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

    
    /// De kleurarray om blokken in de grid te tekenen.
    Color[,] colorGrid = new Color[10, 20];

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        position = Vector2.Zero;

        for (int i = 0; i < colorGrid.GetLength(0); i++)
        {
            for (int j = 0; j < colorGrid.GetLength(1); j++)
            {
                colorGrid[i, j] = Color.White;
            }              
        }
 
        Clear();
    }

   


    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Draw empty grid
        for (int i = 0; i < Width * emptyCell.Width; i += emptyCell.Width)
        {
            position.X = i;
            for (int j = 0; j < Height * emptyCell.Height; j += emptyCell.Height)
            {
                position.Y = j;
                spriteBatch.Draw(emptyCell, position, colorGrid[i / emptyCell.Width, j / emptyCell.Height]);

            }
        }

        // Draw blocks
        
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}