using Microsoft.Xna.Framework;
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
    TetrisGrid grid;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Start;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        switch (gameState)
        {
            case GameState.Start:

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.K))
                {
                    GameStart();
                }
                break;

            case GameState.Playing:

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                {
                    OpenGameMenu();
                }

                if (inputHelper.KeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad0))
                {
                    GameOver();
                }
                break;

            case GameState.GameMenu:

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                {
                    CloseGameMenu();
                }

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    GameOver();
                    GameHomescreen();
                }
                break;

            case GameState.GameOver:

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    GameStart();
                }

                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                {
                    GameHomescreen();
                }
                break;
        }
    }

    public void Update(GameTime gameTime)
    {

    }

    private void ResetSettings() // Stelt standaard settings in.
    {

    }

    // Methodes that handle the gamestate
    private void GameHomescreen()
    {
        gameState = GameState.Start;
    }

    private void GameStart()
    {
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
        spriteBatch.DrawString(font, "Hello!", Vector2.Zero, Color.Blue);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}