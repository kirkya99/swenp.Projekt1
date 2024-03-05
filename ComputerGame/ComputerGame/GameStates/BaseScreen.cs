// <copyright file="BaseScreen.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.GameStates;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// BaseScreen.
/// </summary>
public abstract class BaseScreen
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseScreen"/> class.
    /// </summary>
    /// <param name="viewport">Viewport.</param>
    /// <param name="gameStateHandler">GameStateHandler.</param>
    public BaseScreen(Viewport viewport, GameStateHandler gameStateHandler)
    {
        this.Viewport = viewport;
        this.GameStateHandler = gameStateHandler;
    }

    /// <summary>
    /// Gets or sets the button texture.
    /// </summary>
    protected Texture2D ButtonTexture { get; set; }

    /// <summary>
    /// Gets or sets the game state handler.
    /// </summary>
    protected GameStateHandler GameStateHandler { get; set; }

    /// <summary>
    /// Gets or sets the viewport.
    /// </summary>
    protected Viewport Viewport { get; set; }

    /// <summary>
    /// Gets or sets the x position of the button.
    /// </summary>
    protected int ButtonPosX { get; set; }

    /// <summary>
    /// Gets or sets the x position of the button text.
    /// </summary>
    protected int ButtonTextPosX { get; set; }

    /// <summary>
    /// Gets or sets the y position of the button text.
    /// </summary>
    protected int ButonTextPosY { get; set; }

    /// <summary>
    /// Gets or sets the font.
    /// </summary>
    protected SpriteFont Font { get; set; }

    /// <summary>
    /// Load mennu content.
    /// </summary>
    /// <param name="content">content.</param>
    public virtual void LoadContent(ContentManager content)
    {
        this.ButtonTexture = content.Load<Texture2D>("Button");
        this.ButtonPosX = (this.Viewport.Width / 2) - (this.ButtonTexture.Width / 2);

        this.ButtonTextPosX = this.Viewport.Width / 2;
        this.Font = content.Load<SpriteFont>("ButtonText");
    }

    /// <summary>
    /// Update menu.
    /// </summary>
    /// <param name="gameTime">gameTime.</param>
    public abstract void Update(GameTime gameTime);

    /// <summary>
    /// Draw game screens.
    /// </summary>
    /// <param name="spriteBatch">spriteBatch.</param>
    public abstract void Draw(SpriteBatch spriteBatch);

    /// <summary>
    /// Creates a button.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch.</param>
    /// <param name="buttonIndex">Index of the button.</param>
    /// <param name="text">Displayed text in the button.</param>
    /// <param name="color">Color.</param>
    protected void DrawButton(SpriteBatch spriteBatch, int buttonIndex, string text, Color color)
    {
        spriteBatch.Draw(this.ButtonTexture, new Vector2(this.ButtonPosX, this.CalculateHorizontalButtonPosition(buttonIndex)), color);
        spriteBatch.DrawString(this.Font, text, this.CalculateButtonTextPosition(buttonIndex, text), Color.Black);
    }

    /// <summary>
    /// Draw string.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch.</param>
    /// <param name="text">Text.</param>
    /// <param name="textIndex">TextIndex.</param>
    /// <param name="color">Color.</param>
    protected void DrawString(SpriteBatch spriteBatch, string text, int textIndex, Color color)
    {
        spriteBatch.DrawString(this.Font, text, this.CalculateButtonTextPosition(textIndex, text), color);
    }

    /// <summary>
    /// Returns rectangle of the button area.
    /// </summary>
    /// <param name="buttonIndex">Index of the button.</param>
    /// <returns>Rectangle.</returns>
    protected Rectangle GetButtonArea(int buttonIndex)
    {
        Rectangle buttonRectangle = new Rectangle(this.ButtonPosX, this.CalculateHorizontalButtonPosition(buttonIndex), this.ButtonTexture.Width, this.ButtonTexture.Height);
        return buttonRectangle;
    }

    /// <summary>
    /// Check if mouse is over button.
    /// </summary>
    /// <param name="buttonArea">Button area.</param>
    /// <param name="mousePosition">Mouse position.</param>
    /// <returns>boolean.</returns>
    protected bool ButtonAreaContainsMousePosition(Rectangle buttonArea, Point mousePosition)
    {
        if (buttonArea.X < mousePosition.X && buttonArea.X + buttonArea.Width < mousePosition.X && buttonArea.Y < mousePosition.X && buttonArea.Y + buttonArea.Height < mousePosition.Y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Calculates the horizontal text position.
    /// </summary>
    /// <param name="text">text.</param>
    /// <returns>position.</returns>
    protected int CalculateTextHorizonPosition(string text)
    {
        Vector2 textMeasurements = this.Font.MeasureString(text);
        int posX = (this.Viewport.Width / 2) - (int)(textMeasurements.X / 2);
        return posX;
    }

    /// <summary>
    /// Calculates the button position.
    /// </summary>
    /// <param name="buttonIndex">Index of the button.</param>
    /// <returns>Returns the y coordinate.</returns>
    private int CalculateHorizontalButtonPosition(int buttonIndex)
    {
        return this.ButtonTexture.Height * ((2 * buttonIndex) + 1);
    }

    /// <summary>
    /// Calculates the button text position.
    /// </summary>
    /// <param name="buttonIndex">ButtonIndex.</param>
    /// <param name="text">Text.</param>
    /// <returns>Vector2.</returns>
    private Vector2 CalculateButtonTextPosition(int buttonIndex, string text)
    {
        Vector2 textMeasurements = this.Font.MeasureString(text);

        int posX = (this.Viewport.Width / 2) - (int)(textMeasurements.X / 2);

        int buttonPosY = this.CalculateHorizontalButtonPosition(buttonIndex);
        int buttonTextYOffset = (this.ButtonTexture.Height / 2) - (int)(textMeasurements.Y / 2);
        int posY = buttonPosY + buttonTextYOffset;
        return new Vector2(posX, posY);
    }
}
