// <copyright file="MainMenuScreen.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.GameStates.GameScreens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static ComputerGame.GameStates.GameStateHandler;

/// <summary>
/// MainMenu screen.
/// </summary>
public class MainMenuScreen : BaseScreen
{
    private Rectangle startGameRectangle;
    private Rectangle helpPageRectangle;
    private Rectangle quitGameRectangle;

    private Color startGameColor = Color.White;
    private Color helpPageColor = Color.White;
    private Color quitGameColor = Color.White;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainMenuScreen"/> class.
    /// </summary>
    /// <param name="viewport">Viewport.</param>
    /// <param name="gameStateHandler">GameStateHandler.</param>
    public MainMenuScreen(Viewport viewport, GameStateHandler gameStateHandler)
        : base(viewport, gameStateHandler)
    {
    }

    /// <inheritdoc/>
    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);

        int index = 0;
        this.startGameRectangle = this.GetButtonArea(index++);
        this.helpPageRectangle = this.GetButtonArea(index++);
        this.quitGameRectangle = this.GetButtonArea(index++);
    }

    /// <inheritdoc/>
    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        this.startGameColor = this.startGameRectangle.Contains(Mouse.GetState().Position) ? Color.LightGray : Color.White;
        this.helpPageColor = this.helpPageRectangle.Contains(Mouse.GetState().Position) ? Color.LightGray : Color.White;
        this.quitGameColor = this.quitGameRectangle.Contains(Mouse.GetState().Position) ? Color.LightGray : Color.White;

        Mouse.SetCursor(this.startGameRectangle.Contains(Mouse.GetState().Position)
            || this.helpPageRectangle.Contains(Mouse.GetState().Position)
            || this.quitGameRectangle.Contains(Mouse.GetState().Position)
            ? MouseCursor.Hand : MouseCursor.Arrow);

        if (this.GameStateHandler.LeftMouseIsClicked(mouseState))
        {
            if (this.startGameRectangle.Contains(mouseState.Position))
            {
                this.GameStateHandler.SetGameState(GameState.Game);
            }
            else if (this.helpPageRectangle.Contains(mouseState.Position))
            {
                this.GameStateHandler.OpenHelpPageScreen();
            }
            else if (this.quitGameRectangle.Contains(mouseState.Position))
            {
                this.GameStateHandler.SetGameTransitionState(GameTransitionState.QuitGame);
            }
        }
    }

    /// <inheritdoc/>
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(this.Font, Strings.Highscore + this.GameStateHandler.Highscore.ToString(), new Vector2(10, 10), Color.White);
        int index = 0;
        this.DrawButton(spriteBatch, index++, Strings.StartGame, this.startGameColor);
        this.DrawButton(spriteBatch, index++, Strings.HelpPage, this.helpPageColor);
        this.DrawButton(spriteBatch, index++, Strings.QuitGame, this.quitGameColor);
        spriteBatch.End();
    }
}
