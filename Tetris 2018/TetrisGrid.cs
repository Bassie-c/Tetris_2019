using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    // The soundefects of placing a block
    SoundEffect placeBlock_1;
    SoundEffect placeBlock_2;
    SoundEffect placeBlock_3;
    SoundEffect placeBlock_4;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    /// De kleurarray om blokken in de grid te tekenen.
    Color[,] colorGrid;

    /// Int for keeping track of the removed rows for leveling up
    int removedRows;

    /// Sound effect for leveling up.
    SoundEffect levelUp;

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        placeBlock_1 = TetrisGame.ContentManager.Load<SoundEffect>("PlaceBlock_1");
        placeBlock_2 = TetrisGame.ContentManager.Load<SoundEffect>("PlaceBlock_2");
        placeBlock_3 = TetrisGame.ContentManager.Load<SoundEffect>("PlaceBlock_3");
        placeBlock_4 = TetrisGame.ContentManager.Load<SoundEffect>("PlaceBlock_4");
        levelUp = TetrisGame.ContentManager.Load<SoundEffect>("LevelUp");
        position = Vector2.Zero;
        colorGrid = new Color[Width, Height];
        Clear();
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
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

    public bool CheckBlock(int xChange = 0, int yChange = 0)
    {
        TetrisBlock block = TetrisGame.gameWorld.activeBlock;
        for (int x = 0; x < block.block.GetLength(0); x++)        
            for (int y = 0; y < block.block.GetLength(1); y++)            
                if (block.block[x, y] && colorGrid[block.x + xChange + x, block.y + yChange + y] != Color.White)
                    return true;            
        return false;
    }
    /// <summary>
    /// Places a block on the grid.
    /// </summary>
    public void PlaceBlock()
    {
        int random = GameWorld.Random.Next(1, 4);
        switch (random)
        {
            case 1:
                placeBlock_1.Play();
                break;

            case 2:
                placeBlock_2.Play();
                break;

            case 3:
                placeBlock_3.Play();
                break;

            case 4:
                placeBlock_4.Play();
                break;
        }
        TetrisBlock block = TetrisGame.gameWorld.activeBlock;
        for (int x = 0; x < block.block.GetLength(0); x++)
            for (int y = 0; y < block.block.GetLength(1); y++)
                if (block.block[x, y])
                    colorGrid[block.x + x, block.y + y] = block.color;       
        TetrisGame.gameWorld.activeBlock = null;
        CheckRow(block);
    }

    /// <summary>
    /// Checkt of rijen leeg zijn en haalt ze zo nodig weg.
    /// </summary>
    /// <param name="block"></param>
    public void CheckRow(TetrisBlock block)
    {
        int rowsCleared = 0;
        for (int i = 0; i < Height; i++)
            if (IsRowFull(i))
            {
                RemoveRow(i);
                rowsCleared++;
            }
        GivePoints(rowsCleared);
    }

    /*     
    //Processorefficiënter, maar minder leesbaar. Checkt alleen de rijen van het geplaatste block.
    public void CheckRow(TetrisBlock block)
    {
        int rowsCleared = 0;
        for (int i = block.y; i < block.y + block.block.GetLength(1); i++)
            if (IsRowFull(i))
            {
                RemoveRow(i);
                rowsCleared++;
            }
        GivePoints(rowsCleared);
    }
    */

    /// <summary>
    /// Checkt of een rij vol is. 
    /// </summary>
    /// <param name="j"></param>
    /// <returns></returns>
    public bool IsRowFull(int j)
    {
        for (int i = 0; i < Width; i++)            
                if (colorGrid[i, j] == Color.White)
                    return false;            
        return true;              
    }

    /// <summary>
    /// Haalt een rij weg.
    /// </summary>
    /// <param name="y"></param>
    public void RemoveRow(int y)
    {
        for (int i = 0; i < Width; i++)
            for (int j = y; j > 0; j--)
                colorGrid[i, j] = colorGrid[i, j - 1];
        for (int i = 0; i < 0; i++)
            colorGrid[i, 0] = Color.White;
    }

    /// <summary>
    /// Geeft punten op basis van aantal weggehaalde rijen
    /// </summary>
    /// <param name="x"></param>
    public void GivePoints(int x)
    {
        switch (x)
        {
            case 1:
                TetrisGame.gameWorld.Score += 50;
                removedRows += 1;
                if (removedRows >= 4)
                {
                    TetrisGame.gameWorld.level += 1;
                    levelUp.Play();
                    removedRows = 0;
                }
                break;

            case 2:
                TetrisGame.gameWorld.Score += 200;
                removedRows += 2;
                if (removedRows >= 4)
                {
                    TetrisGame.gameWorld.level += 1;
                    levelUp.Play();
                    removedRows = 0;
                }
                break;

            case 3:
                TetrisGame.gameWorld.Score += 500;
                removedRows += 3;
                if (removedRows >= 4)
                {
                    TetrisGame.gameWorld.level += 1;
                    levelUp.Play();
                    removedRows = 0;
                }
                break;

            case 4:
                TetrisGame.gameWorld.Score += 2000;
                removedRows += 4;
                if (removedRows >= 4)
                {
                    TetrisGame.gameWorld.level += 1;
                    levelUp.Play();
                    removedRows = 0;
                }
                break;
        }
    }
    /// <summary>
    /// Checkt of er al een block bovenaan zit.
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    public bool CheckSpawn(TetrisBlock block)
    {
        for (int i = block.x; i < block.x + block.block.GetLength(0); i++)
            for (int j = 0; j < block.block.GetLength(1); j++)
                if (block.block[i - block.x, j] && (colorGrid[i, j] != Color.White))
                    return true;
        return false;
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < colorGrid.GetLength(0); i++)
            for (int j = 0; j < colorGrid.GetLength(1); j++)
                colorGrid[i, j] = Color.White;
        removedRows = 0;
    }
}