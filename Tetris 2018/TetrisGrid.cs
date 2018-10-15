﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
public class TetrisGrid
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
    Color[,] colorGrid = new Color[10 ,20];

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        position = Vector2.Zero;

        /// Zet alle elementen op blank.
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

    public bool CheckBlock(int xChange, int yChange)
    {
        TetrisBlock block = TetrisGame.gameWorld.activeBlock;
        for (int x = 0; x < block.block.GetLength(0); x++)
        {
            for (int y = 0; y < block.block.GetLength(1); y++)
            {
                if (block.block[x, y] && colorGrid[block.x + xChange + x, block.y + yChange + y] != Color.White)
                    return true;
            }
        }
        return false;
    }

    public void PlaceBlock()
    {
        TetrisBlock block = TetrisGame.gameWorld.activeBlock;
        for (int x = 0; x < block.block.GetLength(0); x++)
        {
            for (int y = 0; y < block.block.GetLength(1); y++)
            {
                if (block.block[x, y])
                    colorGrid[block.x + x, block.y + y] = block.color;
            }
        }
        TetrisGame.gameWorld.activeBlock = null;
    }


    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}