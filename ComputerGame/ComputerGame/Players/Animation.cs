// <copyright file="Animation.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Players;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Represents an animation with multiple frames.
/// </summary>
public class Animation
{
    private Texture2D spriteSheet;
    private float frameTime;
    private Rectangle[] frames;
    private int currentFrame;
    private double timeCounter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Animation"/> class.
    /// </summary>
    /// <param name="texture">The spritesheet texture.</param>
    /// <param name="frameCount">The number of frames in the animation.</param>
    /// <param name="frameTime">The time each frame is displayed.</param>
    public Animation(Texture2D texture, int frameCount, float frameTime)
    {
        this.SpriteSheet = texture;
        this.FrameTime = frameTime;
        this.Frames = new Rectangle[frameCount];

        int frameWidth = this.SpriteSheet.Width / frameCount;
        int frameHeight = this.SpriteSheet.Height;

        for (int i = 0; i < frameCount; i++)
        {
            this.Frames[i] = new Rectangle(i * frameWidth, 0, frameWidth, frameHeight);
        }
    }

    /// <summary>
    /// Gets the frame width of the animation.
    /// </summary>
    public int FrameWidth
    {
        get { return this.spriteSheet.Width / this.frames.Length; }
    }

    /// <summary>
    /// Gets the frame height of the animation.
    /// </summary>
    public int FrameHeight
    {
        get { return this.spriteSheet.Height; }
    }

    /// <summary>
    /// Gets or sets the spritesheet texture.
    /// </summary>
    public Texture2D SpriteSheet
    {
        get => this.spriteSheet;
        set => this.spriteSheet = value;
    }

    /// <summary>
    /// Gets or sets the time each frame is displayed.
    /// </summary>
    public float FrameTime
    {
        get => this.frameTime;
        set => this.frameTime = value;
    }

    /// <summary>
    /// Gets or sets the frames of the animation.
    /// </summary>
    public Rectangle[] Frames
    {
        get => this.frames;
        set => this.frames = value;
    }

    /// <summary>
    /// Gets or sets the index of the current frame.
    /// </summary>
    public int CurrentFrame
    {
        get => this.currentFrame;
        set => this.currentFrame = value;
    }

    /// <summary>
    /// Updates the animation based on game time.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public void Update(GameTime gameTime)
    {
        this.timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

        if (this.timeCounter >= this.FrameTime)
        {
            this.CurrentFrame = (this.CurrentFrame + 1) % this.Frames.Length;
            this.timeCounter -= this.FrameTime;
        }
    }

    /// <summary>
    /// Draws the animation at the specified position.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="position">The position to draw the animation.</param>
    /// <param name="effects">The sprite effects to apply.</param>
    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effects)
    {
        spriteBatch.Draw(this.SpriteSheet, position, this.Frames[this.CurrentFrame], Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
    }
}
