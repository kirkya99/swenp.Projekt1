// <copyright file="GameStateHandler.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.GameStates;

using System.Diagnostics;
using ComputerGame.Players;
using ComputerGame.Views.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Handle the switch between menu and game.
/// </summary>
public class GameStateHandler
{
    private bool escapeKeyIsPressed = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameStateHandler"/> class.
    /// </summary>
    /// <param name="gameTimer">GameTimer.</param>
    public GameStateHandler(GameTimeManager gameTimer)
    {
        this.GameTimer = gameTimer;
    }

    /// <summary>
    /// Game states.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// MainMenu screen of the game.
        /// </summary>
        MainMenu,

        /// <summary>
        /// Game is running
        /// </summary>
        Game,

        /// <summary>
        /// Game is paused.
        /// </summary>
        PauseMenu,

        /// <summary>
        /// Game is over
        /// </summary>
        GameOver,

        /// <summary>
        /// Help page is open
        /// </summary>
        HelpPage,
    }

    /// <summary>
    /// Game transitions.
    /// </summary>
    public enum GameTransitionState
    {
        /// <summary>
        /// No transition to execute.
        /// </summary>
        NoTransition,

        /// <summary>
        /// Quit the game.
        /// </summary>
        QuitGame,

        /// <summary>
        /// Return to the main manu.
        /// </summary>
        ReturnToMainMenu,
    }

    /// <summary>
    /// Gets or sets a value indicating whether the game should be closed.
    /// </summary>
    public GameTransitionState CurrentGameTransitionState { get; set; } = GameTransitionState.NoTransition;

    /// <summary>
    /// Gets or sets a value indicating whether the game should be closed.
    /// </summary>
    public bool ExitGame { get; set; } = false;

    /// <summary>
    /// Gets or sets current game state.
    /// </summary>
    public GameState CurrentGameState { get; set; } = GameState.MainMenu;

    /// <summary>
    /// Gets or sets the previous game state.
    /// </summary>
    public GameState PreviousGameState { get; set; }

    /// <summary>
    /// Gets or sets the second previous game state.
    /// </summary>
    public GameState SecondPreviousGameState { get; set; }

    /// <summary>
    /// Gets or sets the highscore.
    /// </summary>
    public int Highscore { get; set; } = 0;

    /// <summary>
    /// Gets or sets the current score.
    /// </summary>
    public int CurrentScore { get; set; } = 1;

    /// <summary>
    /// Gets or sets the previous keyboard state.
    /// </summary>
    private KeyboardState PreviousKeyboardState { get; set; }

    /// <summary>
    /// Gets or sets the current keyboard state.
    /// </summary>
    private KeyboardState CurrentKeyboardState { get; set; }

    /// <summary>
    /// Gets or sets the previous mouse state.
    /// </summary>
    private MouseState PreviousMouseState { get; set; }

    /// <summary>
    /// Gets or sets the second previous mouse state.
    /// </summary>
    private MouseState SecondPreviousMouseState { get; set; }

    /// <summary>
    /// Gets or sets the remaining game time.
    /// </summary>
    private GameTimeManager GameTimer { get; set; }

    /// <summary>
    /// Handle game states.
    /// </summary>
    /// <param name="player">Player.</param>
    public void HandleGameState(Player player)
    {
        Debug.WriteLine($"Remaining time {this.GameTimer.GetRemainingTime()}, Player position: ({player.PlayerPosition.X}; {player.PlayerPosition.Y})");
        if (player.PlayerPosition.Y > 250 || this.GameTimer.GetRemainingTime() == 0)
        {
            this.CurrentGameState = GameState.GameOver;
        }
        else
        {
            if (this.escapeKeyIsPressed == true)
            {
                if (this.CurrentGameState == GameState.Game)
                {
                    this.CurrentGameState = GameState.PauseMenu;
                }

                // TODO: Uncomment when transition from menu to game is fluent.Unpausing the game is currently only possible via the
                else if (this.CurrentGameState == GameState.PauseMenu || (this.CurrentGameState == GameState.HelpPage && this.PreviousGameState == GameState.PauseMenu))
                {
                    this.CurrentGameState = GameState.Game;
                }
            }
        }
    }

    /// <summary>
    /// Update the menu states.
    /// </summary>
    /// <param name="gameTime">gameTime Parameter.</param>
    /// <param name="player">Player.</param>
    public void Update(GameTime gameTime, Player player)
    {
        this.HandleEscapeKeyStates();
        this.HandleGameState(player);
    }

    /// <summary>
    /// Handle mouse click.
    /// </summary>
    /// <param name="currentMouseState">Mouse state.</param>
    /// <returns>Bool is clicked.</returns>
    public bool LeftMouseIsClicked(MouseState currentMouseState)
    {
        bool buttonIsClicked = currentMouseState.LeftButton == ButtonState.Pressed
            && this.PreviousMouseState.LeftButton == ButtonState.Released
            && this.SecondPreviousMouseState.LeftButton == ButtonState.Released ? true : false;
        this.PreviousMouseState = currentMouseState;
        this.SecondPreviousMouseState = this.PreviousMouseState;
        return buttonIsClicked;
    }

    /// <summary>
    /// Sets the new game state.
    /// </summary>
    /// <param name="gameState">new game state.</param>
    public void SetGameState(GameState gameState)
    {
        this.CurrentGameState = gameState;
    }

    /// <summary>
    /// Open help page.
    /// </summary>
    public void OpenHelpPageScreen()
    {
        this.PreviousGameState = this.CurrentGameState;
        this.CurrentGameState = GameState.HelpPage;
    }

    /// <summary>
    /// Return to previous screen.
    /// </summary>
    public void ReturnToPreviousGameStateFromHelpPageScreen()
    {
        this.CurrentGameState = this.PreviousGameState;
    }

    /// <summary>
    /// Set current transition state.
    /// </summary>
    /// <param name="transitionState">new transition state.</param>
    public void SetGameTransitionState(GameTransitionState transitionState)
    {
        this.CurrentGameTransitionState = transitionState;
    }

    /// <summary>
    /// Resets the game.
    /// </summary>
    public void ResetGame()
    {
        this.CurrentGameState = GameState.MainMenu;
        this.CurrentGameTransitionState = GameTransitionState.ReturnToMainMenu;
    }

    /// <summary>
    /// Sets the new reached highscore.
    /// </summary>
    /// <param name="highscore">new highscore.</param>
    public void SetHighscore(int highscore)
    {
        this.Highscore = highscore > this.Highscore ? highscore : this.Highscore;
    }

    /// <summary>
    /// Sets the current score.
    /// </summary>
    /// <param name="currentScore">current score.</param>
    public void SetCurrentScore(int currentScore)
    {
        this.CurrentScore = currentScore;
    }

    /// <summary>
    /// Handle the state of the escape key state field.
    /// </summary>
    private void HandleEscapeKeyStates()
    {
        this.PreviousKeyboardState = this.CurrentKeyboardState;
        this.CurrentKeyboardState = Keyboard.GetState();

        // Check if the Escape key is pressed
        if (this.CurrentKeyboardState.IsKeyDown(Keys.Escape) && !this.PreviousKeyboardState.IsKeyDown(Keys.Escape))
        {
            // Toggle the state only when the Escape key is pressed for the first time
            this.escapeKeyIsPressed = true;
        }
        else
        {
            this.escapeKeyIsPressed = false;
        }
    }
}
