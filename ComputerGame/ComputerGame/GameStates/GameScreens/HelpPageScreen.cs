// <copyright file="HelpPageScreen.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.GameStates.GameScreens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Help page screen.
/// </summary>
public class HelpPageScreen : BaseScreen
{
    private Color returnToPrevScreenColor = Color.White;

    private Rectangle returnToPrevScreenRectangle = Rectangle.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="HelpPageScreen"/> class.
    /// </summary>
    /// <param name="viewport">Viewport.</param>
    /// <param name="gameStateHandler">GameStateHandler.</param>
    public HelpPageScreen(Viewport viewport, GameStateHandler gameStateHandler)
        : base(viewport, gameStateHandler)
    {
    }

    /// <inheritdoc/>
    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);

        this.returnToPrevScreenRectangle = this.GetButtonArea(2);
    }

    /// <summary>
    /// Update the help page.
    /// </summary>
    /// <param name="gameTime">GameTime.</param>
    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        this.returnToPrevScreenColor = this.returnToPrevScreenRectangle.Contains(mouseState.Position) ? Color.LightGray : Color.White;

        Mouse.SetCursor(this.returnToPrevScreenRectangle.Contains(mouseState.Position)
            ? MouseCursor.Hand : MouseCursor.Arrow);

        if (this.GameStateHandler.LeftMouseIsClicked(mouseState))
        {
            if (this.returnToPrevScreenRectangle.Contains(mouseState.Position))
            {
                this.GameStateHandler.ReturnToPreviousGameStateFromHelpPageScreen();
            }
        }
    }

    /// <inheritdoc/>
    public override void Draw(SpriteBatch spriteBatch)
    {
        int posX = this.CalculateTextHorizonPosition(Strings.HelpIntro);
        spriteBatch.Begin();
        spriteBatch.DrawString(this.Font, Strings.HelpIntro, new Vector2(posX, 10), Color.White);
        spriteBatch.DrawString(this.Font, Strings.WalkLeft, new Vector2(posX, 35), Color.White);
        spriteBatch.DrawString(this.Font, Strings.WalkRight, new Vector2(posX, 60), Color.White);
        spriteBatch.DrawString(this.Font, Strings.RunLeft, new Vector2(posX, 85), Color.White);
        spriteBatch.DrawString(this.Font, Strings.RunRight, new Vector2(posX, 110), Color.White);
        spriteBatch.DrawString(this.Font, Strings.Jump, new Vector2(posX, 135), Color.White);
        spriteBatch.DrawString(this.Font, Strings.GameDescription, new Vector2(posX, 160), Color.White);
        this.DrawButton(spriteBatch, 2, Strings.ReturnToPreviousScreen, this.returnToPrevScreenColor);
        spriteBatch.End();
    }
}
