// <copyright file="LoadGameContent.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.GameExecution;

using ComputerGame.GameStates.GameScreens;
using ComputerGame.Platforms;
using ComputerGame.Players;
using ComputerGame.Views;
using ComputerGame.Views.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Load game content.
/// </summary>
public class LoadGameContent : Game
{
    /// <summary>
    /// Load game content.
    /// </summary>
    /// <param name="content">Content.</param>
    /// <param name="player">Player.</param>
    /// <param name="mainMenuScreen">StartScreen.</param>
    /// <param name="pauseMenuScreen">PauseMenuScreen.</param>
    /// <param name="gameOverScreen">GameOverScreen.</param>
    /// <param name="helpPageScreen">HelpPageScreen.</param>
    /// <param name="platformCollection">PlatformCollection.</param>
    /// <param name="background">Background.</param>
    /// <param name="graphicsDevice">GraphicsDevice.</param>
    /// <param name="gameTimer">GameTimer.</param>
    /// <param name="scoreManager">ScoreManger.</param>
    public void LoadContent(
        ContentManager content,
        Player player,
        MainMenuScreen mainMenuScreen,
        PauseMenuScreen pauseMenuScreen,
        GameOverScreen gameOverScreen,
        HelpPageScreen helpPageScreen,
        PlatformCollection platformCollection,
        Background background,
        GraphicsDevice graphicsDevice,
        GameTimeManager gameTimer,
        ScoreManager scoreManager)
    {
        player.LoadContent(content);
        mainMenuScreen.LoadContent(content);
        pauseMenuScreen.LoadContent(content);
        gameOverScreen.LoadContent(content);
        helpPageScreen.LoadContent(content);
        platformCollection.LoadContent(content);
        background.LoadContent(content, graphicsDevice);
        gameTimer.LoadContent(content);
        scoreManager.LoadContent(content);
    }
}