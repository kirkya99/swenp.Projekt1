// <copyright file="PauseMenuScreen.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.GameStates.GameScreens;

using ComputerGame.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static ComputerGame.GameStates.GameStateHandler;

/// <summary>
/// Menu of the game.
/// </summary>
public class PauseMenuScreen : BaseScreen
{
    private Rectangle resumeGameRectangle;
    private Rectangle helpPageRectangle;
    private Rectangle quitGameRectangle;

    private Color resumeGameColor = Color.White;
    private Color helpGameColor = Color.White;
    private Color quitGameColor = Color.White;

    /// <summary>
    /// Initializes a new instance of the <see cref="PauseMenuScreen"/> class.
    /// </summary>
    /// <param name="viewport">Viewport.</param>
    /// <param name="gameStateHandler">GameStateHandler.</param>
    public PauseMenuScreen(Viewport viewport, GameStateHandler gameStateHandler)
        : base(viewport, gameStateHandler)
    {
    }

    /// <inheritdoc/>
    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);

        int index = 0;
        this.resumeGameRectangle = this.GetButtonArea(index++);
        this.helpPageRectangle = this.GetButtonArea(index++);
        this.quitGameRectangle = this.GetButtonArea(index++);
    }

    /// <inheritdoc/>
    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        this.resumeGameColor = this.resumeGameRectangle.Contains(Mouse.GetState().Position) ? Color.LightGray : Color.White;
        this.helpGameColor = this.helpPageRectangle.Contains(Mouse.GetState().Position) ? Color.LightGray : Color.White;
        this.quitGameColor = this.quitGameRectangle.Contains(Mouse.GetState().Position) ? Color.LightGray : Color.White;

        Mouse.SetCursor(this.resumeGameRectangle.Contains(Mouse.GetState().Position)
            || this.helpPageRectangle.Contains(Mouse.GetState().Position)
            || this.quitGameRectangle.Contains(Mouse.GetState().Position)
            ? MouseCursor.Hand : MouseCursor.Arrow);

        if (this.GameStateHandler.LeftMouseIsClicked(mouseState))
        {
            if (this.resumeGameRectangle.Contains(mouseState.Position))
            {
                this.GameStateHandler.SetGameState(GameState.Game);
            }
            else if (this.helpPageRectangle.Contains(mouseState.Position))
            {
                this.GameStateHandler.OpenHelpPageScreen();
            }
            else if (this.quitGameRectangle.Contains(mouseState.Position))
            {
                this.GameStateHandler.SetGameState(GameState.MainMenu);
                this.GameStateHandler.SetGameTransitionState(GameTransitionState.ReturnToMainMenu);
            }
        }
    }

    /// <inheritdoc/>
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(this.Font, Strings.CurrentScore + this.GameStateHandler.CurrentScore.ToString(), new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(this.Font, Strings.Highscore + this.GameStateHandler.Highscore.ToString(), new Vector2(10, 32), Color.White);
        int index = 0;
        this.DrawButton(spriteBatch, index++, Strings.ResumeGame, this.resumeGameColor);
        this.DrawButton(spriteBatch, index++, Strings.HelpPage, this.helpGameColor);
        this.DrawButton(spriteBatch, index++, Strings.ReturnToStart, this.quitGameColor);
        spriteBatch.End();
    }
}
