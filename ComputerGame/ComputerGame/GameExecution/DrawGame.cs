// <copyright file="DrawGame.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

#nullable enable
namespace ComputerGame.GameExecution;

using ComputerGame.GameStates;
using ComputerGame.GameStates.GameScreens;
using ComputerGame.Platforms;
using ComputerGame.Players;
using ComputerGame.Views;
using ComputerGame.Views.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ComputerGame.GameStates.GameStateHandler;

/// <summary>
/// Draw the game.
/// </summary>
public class DrawGame : Game
{
    /// <summary>
    /// Draws the game.
    /// </summary>
    /// <param name="gameTime">GameTime.</param>
    /// <param name="gameStateHandler">GameStateHandler.</param>
    /// <param name="spriteBatch">SpriteBatch.</param>
    /// <param name="camera">Camera.</param>
    /// <param name="background">Background.</param>
    /// <param name="player">Player.</param>
    /// <param name="platformCollection">PlatformCollection.</param>
    /// <param name="startScreen">StartScreen.</param>
    /// <param name="pauseMenuScreen">PauseMenuScreen.</param>
    /// <param name="gameOverScreen">GameOverScreen.</param>
    /// <param name="helpPageScreen">HelpPageScreen.</param>
    /// <param name="gameTimer">GameTimer.</param>
    /// <param name="scoreManager">ScoreManager.</param>
    /// <param name="pixel">Pixel.</param>
    public void Draw(
        GameTime gameTime,
        GameStateHandler gameStateHandler,
        SpriteBatch spriteBatch,
        Camera camera,
        Background background,
        Player player,
        PlatformCollection platformCollection,
        MainMenuScreen startScreen,
        PauseMenuScreen pauseMenuScreen,
        GameOverScreen gameOverScreen,
        HelpPageScreen helpPageScreen,
        GameTimeManager gameTimer,
        ScoreManager scoreManager,
        Texture2D? pixel = null)
    {
        switch (gameStateHandler.CurrentGameState)
        {
            case GameState.MainMenu:
                startScreen.Draw(spriteBatch);
                break;
            case GameState.Game:
                spriteBatch.Begin(transformMatrix: camera.ViewMatrix);
                background.Draw(spriteBatch);
#if DEBUG
                // Draw bounding boxes for debugging
                if (pixel != null)
                {
                    this.DrawBoundingBox(spriteBatch, player.BoundingBox, Color.Red, pixel);
                    foreach (var platform in platformCollection.GetPlatforms())
                    {
                        this.DrawBoundingBox(spriteBatch, platform.GetBoundingBox(), Color.Green, pixel);
                    }
                }
#endif
                player.Draw(spriteBatch);
                platformCollection.Draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin();
                gameTimer.Draw(spriteBatch);
                scoreManager.Draw(spriteBatch);
                spriteBatch.End();
                break;
            case GameState.PauseMenu:
                pauseMenuScreen.Draw(spriteBatch);
                break;
            case GameState.GameOver:
                gameOverScreen.Draw(spriteBatch);
                break;
            case GameState.HelpPage:
                helpPageScreen.Draw(spriteBatch);
                break;
        }
    }

    /// <summary>
    /// Draws the bounding box of an object.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object used for drawing.</param>
    /// <param name="boundingBox">The bounding box to be drawn.</param>
    /// <param name="color">The color of the bounding box.</param>
    /// <param name="pixel">Pixel.</param>
    private void DrawBoundingBox(SpriteBatch spriteBatch, Rectangle boundingBox, Color color, Texture2D pixel)
    {
        spriteBatch.Draw(pixel, boundingBox, color * 0.5f);
    }
}
