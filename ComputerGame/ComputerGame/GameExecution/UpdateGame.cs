// <copyright file="UpdateGame.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.GameExecution;

using ComputerGame.GameStates;
using ComputerGame.GameStates.GameScreens;
using ComputerGame.Platforms;
using ComputerGame.Players;
using ComputerGame.Views;
using ComputerGame.Views.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static ComputerGame.GameStates.GameStateHandler;

/// <summary>
/// Update the game.
/// </summary>
public class UpdateGame : Game
{
    /// <summary>
    /// Update game.
    /// </summary>
    /// <param name="gameTime">GameTime.</param>
    /// <param name="gameStateHandler">GameStateHandler.</param>
    /// <param name="player">Player.</param>
    /// <param name="platformCollection">PlatformCollection.</param>
    /// <param name="camera">Camera.</param>
    /// <param name="mainMenuScreen">MainMenuScreen.</param>
    /// <param name="pauseMenuScreen">PauseMenuScreen.</param>
    /// <param name="gameOverScreen">GameOverScreen.</param>
    /// <param name="helpPageScreen">HelpPageScreen.</param>
    /// <param name="gameTimer">GameTimer.</param>
    /// <param name="scoreManager">ScoreManger.</param>
    public void Update(
        GameTime gameTime,
        GameStateHandler gameStateHandler,
        Player player,
        PlatformCollection platformCollection,
        Camera camera,
        MainMenuScreen mainMenuScreen,
        PauseMenuScreen pauseMenuScreen,
        GameOverScreen gameOverScreen,
        HelpPageScreen helpPageScreen,
        GameTimeManager gameTimer,
        ScoreManager scoreManager)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
        {
            gameStateHandler.SetGameTransitionState(GameTransitionState.QuitGame);
        }

        // Menu states
        gameStateHandler.Update(gameTime, player);

        switch (gameStateHandler.CurrentGameState)
        {
            case GameState.MainMenu:
                gameStateHandler.SetGameState(GameState.MainMenu);
                player.ResetPlayerPosition();
                mainMenuScreen.Update(gameTime);
                break;
            case GameState.Game:
                player.Update(gameTime, platformCollection, scoreManager);
                camera.Update(player.PlayerPosition);
                gameTimer.Update(gameTime);
                gameStateHandler.SetCurrentScore(scoreManager.GetScore());
                break;
            case GameState.PauseMenu:
                pauseMenuScreen.Update(gameTime);
                break;
            case GameState.GameOver:
                gameOverScreen.Update(gameTime);
                break;
            case GameState.HelpPage:
                helpPageScreen.Update(gameTime);
                break;
        }
    }
}
