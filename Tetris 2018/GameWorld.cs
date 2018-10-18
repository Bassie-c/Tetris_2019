﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    enum GameState
    { Start, Playing, GameMenu, GameOver }

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    public TetrisGrid grid;

    /// <summary>
    /// The score of the player.
    /// </summary>
    int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    /// <summary>
    /// The block that can be controlled by the player.
    /// </summary>
    public TetrisBlock activeBlock;

    /// <summary>
    /// The next block to be active.
    /// </summary>
    public TetrisBlock queuedBlock;

    /// <summary>
    /// The timer for moving the block down.
    /// </summary>
    float blockTimer;

    /// <summary>
    /// The current level the player is playing at.
    /// </summary>
    int level;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Start;
        grid = new TetrisGrid();           
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        score = 0;
        level = 1;
    }

    /// <summary>
    /// Handles all the player input.
    /// <param name="gameTime"></param>
    /// <param name="inputHelper"></param>
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        switch (gameState)
        {
            case GameState.Start:

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    GameStart();
                }
                break;

            case GameState.Playing:

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))                
                    OpenGameMenu();                

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))                
                    activeBlock.MoveLeft();                

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))                
                    activeBlock.MoveRight();                

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))                
                    activeBlock.MoveDown();                

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))                
                    while (activeBlock != null)
                        activeBlock.MoveDown();                

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.A))                
                    activeBlock.RotateCounterclockwise();

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.D))                
                    activeBlock.RotateClockwise();                

                // Quick GameOver debug cheat
                if (inputHelper.KeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad0))                
                    GameOver();                
                break;

            case GameState.GameMenu:

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))                
                    CloseGameMenu();                

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    GameOver();
                    GameHomescreen();
                }
                break;

            case GameState.GameOver:

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))                
                    GameStart();                

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))                
                    GameHomescreen();                
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        switch (gameState)
        {
            case GameState.Start:
                break;

            case GameState.Playing:
                blockTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (blockTimer <= 0)
                {
                    activeBlock.MoveDown();
                    ResetBlockTimer();
                }

                if (activeBlock == null) //M: TODO: Check of er al een block is. 
                {
                    activeBlock = queuedBlock;
                    NewBlock();
                    if (grid.CheckSpawn(activeBlock))
                        GameOver();
                }
                break;

            case GameState.GameMenu:
                break;

            case GameState.GameOver:
                break;
        }
    }

    private void NewBlock()
    {
        int r = random.Next(7);
        switch (r)
        {
            case 0:
                queuedBlock = new IBlock();
                break;

            case 1:
                queuedBlock = new JBlock();
                break;

            case 2:
                queuedBlock = new LBlock();
                break;

            case 3:
                queuedBlock = new OBlock();
                break;

            case 4:
                queuedBlock = new SBlock();
                break;

            case 5:
                queuedBlock = new TBlock();
                break;

            case 6:
                queuedBlock = new ZBlock();
                break;
        }
    }

    public void ResetBlockTimer()
    {
        blockTimer = (float)(1 - 0.2 * Math.Pow(level, 0.5));
    }

    /*
    private void ResetSettings() // Stelt standaard settings in. //M: Onderaan staat al Reset()
    {
        level = 1;
        score = 0;
    }*/

    // Methods that handle the gamestate
    private void GameHomescreen()
    {
        gameState = GameState.Start;
    }

    private void GameStart()
    {
        NewBlock();
        activeBlock = queuedBlock;
        NewBlock();
        level = 1;
        ResetBlockTimer();
        gameState = GameState.Playing;
    }

    private void OpenGameMenu()
    {
        gameState = GameState.GameMenu;
    }

    private void CloseGameMenu()
    {
        gameState = GameState.Playing;
    }

    private void GameOver()
    {
        gameState = GameState.GameOver;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);

        switch (gameState)
        {
            case GameState.Start:
                spriteBatch.DrawString(font, "Hoofdmenu", new Vector2(97, 0), Color.Blue);
                break;

            case GameState.Playing:
                drawGame();
                break;

            case GameState.GameMenu:
                drawGame();
                break;

            case GameState.GameOver:
                spriteBatch.DrawString(font, "Game Over", new Vector2(97, 4), Color.Blue);
                spriteBatch.DrawString(font, "Level = " + level, new Vector2(97, 34), Color.Blue);
                spriteBatch.DrawString(font, "Score = " + score, new Vector2(97, 64), Color.Blue);
                break;
        }

        spriteBatch.End();

        void drawGame()
        {
            grid.Draw(gameTime, spriteBatch);
            activeBlock.Draw(gameTime, spriteBatch, Vector2.Zero);
            queuedBlock.Draw(gameTime, spriteBatch, new Vector2(400, 100));
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(500, 0), Color.Blue);
            spriteBatch.DrawString(font, "Level: " + level, new Vector2(500, 40), Color.Blue);
        }
    }

    public void Reset()
    {
        level = 1;
        score = 0;
        TetrisGame.gameWorld.activeBlock = null;
        grid.Clear();
        GameHomescreen();
    }
}