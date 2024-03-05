// <copyright file="AnimationHandler.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Players.Handlers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Controls the animations of the game player.
/// </summary>
public class AnimationHandler
{
    /// <summary>
    /// Gets the IdleAnimation.
    /// </summary>
    public Animation IdleAnimation { get; private set; }

    /// <summary>
    /// Gets the RunAnimation.
    /// </summary>
    public Animation RunAnimation { get; private set; }

    /// <summary>
    /// Gets the WalkAnimation.
    /// </summary>
    public Animation WalkAnimation { get; private set; }

    /// <summary>
    /// Gets the JumpAnimation.
    /// </summary>
    public Animation JumpAnimation { get; private set; }

    /// <summary>
    /// Gets the CurrentAnimation.
    /// </summary>
    public Animation CurrentAnimation { get; private set; }

    /// <summary>
    /// Gets the sprite effects.
    /// </summary>
    public SpriteEffects Effects { get; private set; } = SpriteEffects.None;

    /// <summary>
    /// Updates the current animation.
    /// </summary>
    /// <param name="gameTime">Animation instance.</param>
    public void Update(GameTime gameTime)
    {
        this.CurrentAnimation.Update(gameTime);
    }

    /// <summary>
    /// Loads the player content.
    /// </summary>
    /// <param name="content">The content manager.</param>
    public void LoadContent(ContentManager content)
    {
        Texture2D idleTexture = content.Load<Texture2D>("idle");
        Texture2D runTexture = content.Load<Texture2D>("run");
        Texture2D walkTexture = content.Load<Texture2D>("walk");
        Texture2D jumpTexture = content.Load<Texture2D>("jump");

        this.IdleAnimation = new Animation(idleTexture, 6, 0.2f);
        this.RunAnimation = new Animation(runTexture, 8, 0.1f);
        this.WalkAnimation = new Animation(walkTexture, 8, 0.15f);
        this.JumpAnimation = new Animation(jumpTexture, 10, 0.5f);

        this.CurrentAnimation = this.IdleAnimation;
    }

    /// <summary>
    /// Draw the player figure.
    /// </summary>
    /// <param name="spriteBatch">spriteBatch.</param>
    /// <param name="position">position.</param>
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        this.CurrentAnimation.Draw(spriteBatch, position, this.Effects);
    }

    /// <summary>
    /// Sets the animation to the idle animation.
    /// </summary>
    public void SetIdleAnimation()
    {
        this.CurrentAnimation = this.IdleAnimation;
    }

    /// <summary>
    /// Sets the animation to the jump animation.
    /// </summary>
    public void SetJumpAnimation()
    {
        this.CurrentAnimation = this.JumpAnimation;
    }

    /// <summary>
    /// Sets the animation to the run animation.
    /// </summary>
    public void SetRunAnimation()
    {
        this.CurrentAnimation = this.RunAnimation;
    }

    /// <summary>
    /// Sets the animation to the walk animation.
    /// </summary>
    public void SetWalkAnimation()
    {
        this.CurrentAnimation = this.WalkAnimation;
    }

    /// <summary>
    /// Sets the SpriteEffects to None.
    /// </summary>
    public void SetSpriteEffectsNone()
    {
        this.Effects = SpriteEffects.None;
    }

    /// <summary>
    /// Sets the SpriteEffects to horizontally flipped.
    /// </summary>
    public void SetSmokeEffectsFlippedHorizontally()
    {
        this.Effects = SpriteEffects.FlipHorizontally;
    }
}
