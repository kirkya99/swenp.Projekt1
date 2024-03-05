// <copyright file="Background.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Views;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Background.
/// </summary>
public class Background
{
    private Texture2D texture;
    private Vector2 screenScale = Vector2.Zero;

    /// <summary>
    /// LOad COntent.
    /// </summary>
    /// <param name="content">Content.</param>
    /// <param name="graphicsDevice">GraphicsDevice.</param>
    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        // Hintergrund laden und skallieren
        this.texture = content.Load<Texture2D>("background1");

        this.screenScale = new Vector2(
            (float)graphicsDevice.Viewport.Width * 2 / this.texture.Width,
            (float)graphicsDevice.Viewport.Height / this.texture.Height);
    }

    /// <summary>
    /// Draw.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, this.screenScale, SpriteEffects.None, 0f);
    }
}