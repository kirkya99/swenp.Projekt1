// <copyright file="GameOverScreen.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.GameStates.GameScreens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static ComputerGame.GameStates.GameStateHandler;

/// <summary>
/// Game over screen.
/// </summary>
public class GameOverScreen : BaseScreen
{
    private Color returnToStartColor = Color.White;
    private Color quitGameColor = Color.White;

    private Rectangle returnToStartRectangle;
    private Rectangle quitGameRectangle;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameOverScreen"/> class.
    /// </summary>
    /// <param name="viewport">Viewport.</param>
    /// <param name="gameStateHandler">GameStateHandler.</param>
    public GameOverScreen(Viewport viewport, GameStateHandler gameStateHandler)
        : base(viewport, gameStateHandler)
    {
    }

    /// <inheritdoc/>
    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);

        this.returnToStartRectangle = this.GetButtonArea(0);
        this.quitGameRectangle = this.GetButtonArea(1);
    }

    /// <inheritdoc/>
    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        this.returnToStartColor = this.returnToStartRectangle.Contains(Mouse.GetState().Position) ? Color.LightGray : Color.White;
        this.quitGameColor = this.quitGameRectangle.Contains(Mouse.GetState().Position) ? Color.LightGray : Color.White;

        if (this.GameStateHandler.LeftMouseIsClicked(mouseState))
        {
            if (this.returnToStartRectangle.Contains(mouseState.Position))
            {
                this.GameStateHandler.SetGameState(GameState.MainMenu);
                this.GameStateHandler.SetGameTransitionState(GameTransitionState.ReturnToMainMenu);
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
        spriteBatch.DrawString(this.Font, Strings.CurrentScore + this.GameStateHandler.CurrentScore.ToString(), new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(this.Font, Strings.Highscore + this.GameStateHandler.Highscore.ToString(), new Vector2(10, 32), Color.White);
        int index = 0;
        this.DrawButton(spriteBatch, index++, Strings.ReturnToStart, this.returnToStartColor);
        this.DrawButton(spriteBatch, index++, Strings.QuitGame, this.quitGameColor);
        spriteBatch.End();
    }
}
