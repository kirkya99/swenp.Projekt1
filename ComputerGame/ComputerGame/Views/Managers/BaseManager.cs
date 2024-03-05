// <copyright file="BaseManager.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Views.Managers;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The base class for the score manager and game time manager.
/// </summary>
public abstract class BaseManager
{
    private string displayedText;

    private Dictionary<char, Texture2D> numberSprites;
    private Dictionary<char, Texture2D> letterSprites;
    private Vector2 position;
    private float scale;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseManager"/> class.
    /// </summary>
    /// <param name="displayedText">Text.</param>
    /// <param name="position">Position.</param>
    /// <param name="scale">Scale.</param>
    public BaseManager(string displayedText, Vector2 position, float scale)
    {
        this.displayedText = displayedText;
        this.position = position;
        this.scale = scale;
    }

    /// <summary>
    /// Gets or sets the numeric value.
    /// </summary>
    protected int NumericValue { get; set; } = 0;

    /// <summary>
    /// Loads the content of the game timer or the score.
    /// </summary>
    /// <param name="content">Content.</param>
    public virtual void LoadContent(ContentManager content)
    {
        // Load number Sprites
        this.numberSprites = new Dictionary<char, Texture2D>();
        for (char c = '0'; c <= '9'; c++)
        {
            this.numberSprites[c] = content.Load<Texture2D>(c.ToString());
        }

        // Load letter sprites
        this.letterSprites = new Dictionary<char, Texture2D>();

        // const string letters = this.displayedText;
        foreach (char c in this.displayedText)
        {
            this.letterSprites[c] = content.Load<Texture2D>("Upper_" + c);
        }
    }

    /// <summary>
    /// Updates the game timer or score.
    /// </summary>
    /// <param name="gameTime">The GameTime.</param>
    public virtual void Update(GameTime gameTime)
    {
    }

    /// <summary>
    /// The base method for the drawing of the timer and score counter.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch.</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Vector2 fieldHeaderPosition = this.position;
        foreach (char c in this.displayedText)
        {
            Vector2 scaleVector = new Vector2(this.scale, this.scale);
            spriteBatch.Draw(this.letterSprites[c], fieldHeaderPosition, null, Color.White, 0, Vector2.Zero, scaleVector, SpriteEffects.None, 0);
            fieldHeaderPosition.X += this.letterSprites[c].Width * this.scale;
        }

        Vector2 fieldContentPosition = new Vector2(fieldHeaderPosition.X + 20, fieldHeaderPosition.Y);
        string numericString = this.NumericValue.ToString("D3");
        foreach (char c in numericString)
        {
            Vector2 scaleVector = new Vector2(this.scale, this.scale);
            spriteBatch.Draw(this.numberSprites[c], fieldContentPosition, null, Color.White, 0, Vector2.Zero, scaleVector, SpriteEffects.None, 0);
            fieldContentPosition.X += this.numberSprites[c].Width * this.scale;
        }
    }
}
