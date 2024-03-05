// <copyright file="Game1.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame;

using ComputerGame.GameExecution;
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
/// Class Game1.
/// </summary>
public class Game1 : Game
{
    private ScoreManager score;
    private GameTimeManager gameTimer;
    private Player player;  // Neue Instanz der Player-Klasse
    private PlatformCollection platformCollection;  // Neue Instanz der Platform-Klasse
    private Background background; // Neue Instanz der Background-Klasse
    private Camera camera; // Neue Instanz der Camera-Klasse
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Texture2D pixel; // Used for drawing bounding boxes for debugging
    private GameStateHandler gameStateHandler;
    private MainMenuScreen mainMenuScreen;
    private PauseMenuScreen pauseMenuScreen;
    private GameOverScreen gameOverScreen;
    private HelpPageScreen helpPageScreen;
    private LoadGameContent loadGameContent = new LoadGameContent();
    private UpdateGame updateGame = new UpdateGame();
    private DrawGame drawGame = new DrawGame();

    /// <summary>
    /// Initializes a new instance of the <see cref="Game1"/> class.
    /// Game1 Constructor.
    /// </summary>
    public Game1()
    {
        this.graphics = new GraphicsDeviceManager(this);
        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
        this.NumberOfPlatforms = 250;
    }

    /// <summary>
    /// Gets or sets the number of platforms.
    /// </summary>
    private int NumberOfPlatforms { get; set; }

    /// <summary>
    /// Resets the game.
    /// </summary>
    public void Reset()
    {
        this.gameStateHandler.SetHighscore(this.score.ResetScore());
        this.player.ResetPlayerPosition();
        this.gameStateHandler.CurrentGameTransitionState = GameTransitionState.NoTransition;
        this.gameTimer.ResetRemainingTime();
        this.platformCollection.ResetTouchedPlatforms();
    }

    /// <summary>
    /// Initialize Content.
    /// </summary>
    protected override void Initialize()
    {
        this.platformCollection = new PlatformCollection(this.NumberOfPlatforms);
        this.NumberOfPlatforms += 25;
        this.player = new Player();
        this.background = new Background();
        this.camera = new Camera(this.GraphicsDevice.Viewport);
        this.gameTimer = new GameTimeManager(new System.Numerics.Vector2(620, 50), 0.6f); // Position des Timers setzen, sowie dessen Größe
        this.gameStateHandler = new GameStateHandler(this.gameTimer);
        this.mainMenuScreen = new MainMenuScreen(this.graphics.GraphicsDevice.Viewport, this.gameStateHandler);
        this.pauseMenuScreen = new PauseMenuScreen(this.graphics.GraphicsDevice.Viewport, this.gameStateHandler);
        this.gameOverScreen = new GameOverScreen(this.graphics.GraphicsDevice.Viewport, this.gameStateHandler);
        this.helpPageScreen = new HelpPageScreen(this.graphics.GraphicsDevice.Viewport, this.gameStateHandler);
        this.score = new ScoreManager(new System.Numerics.Vector2(120, 50), 0.6f);

        base.Initialize();
    }

    /// <summary>
    /// Load Content.
    /// </summary>
    protected override void LoadContent()
    {
        this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
        this.loadGameContent.LoadContent(this.Content, this.player, this.mainMenuScreen, this.pauseMenuScreen, this.gameOverScreen, this.helpPageScreen, this.platformCollection, this.background, this.GraphicsDevice, this.gameTimer, this.score);
        this.pixel = new Texture2D(this.GraphicsDevice, 1, 1);
        this.pixel.SetData(new Color[] { Color.White });
    }

    /// <summary>
    /// Update Content.
    /// </summary>
    /// <param name="gameTime">GameTime is para.</param>
    protected override void Update(GameTime gameTime)
    {
        // Calling the virtual method from Game.
        if (this.gameStateHandler.CurrentGameState == GameState.Game)
        {
            base.Update(gameTime);
        }

        if (this.gameStateHandler.CurrentGameTransitionState == GameTransitionState.QuitGame)
        {
            this.Exit();
            this.gameStateHandler.CurrentGameTransitionState = GameTransitionState.NoTransition;
        }
        else if (this.gameStateHandler.CurrentGameTransitionState == GameTransitionState.ReturnToMainMenu)
        {
            this.Reset();
        }

        this.updateGame.Update(gameTime, this.gameStateHandler, this.player, this.platformCollection, this.camera, this.mainMenuScreen, this.pauseMenuScreen, this.gameOverScreen, this.helpPageScreen, this.gameTimer, this.score);
    }

    /// <summary>
    /// Draw Content.
    /// </summary>
    /// <param name="gameTime">GameTime is para.</param>
    protected override void Draw(GameTime gameTime)
    {
        // Calling the virtual method from Game.
        if (this.gameStateHandler.CurrentGameState == GameState.Game)
        {
            base.Draw(gameTime);
        }

        this.GraphicsDevice.Clear(Color.CornflowerBlue);
        this.drawGame.Draw(gameTime, this.gameStateHandler, this.spriteBatch, this.camera, this.background, this.player, this.platformCollection, this.mainMenuScreen, this.pauseMenuScreen, this.gameOverScreen, this.helpPageScreen, this.gameTimer, this.score, this.pixel);
    }
}