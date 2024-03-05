// <copyright file="Platform.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Platforms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Platform.
/// </summary>
public class Platform
{
    private Vector2 position;
    private int width;
    private int height;
    private Texture2D texture;
    private int posX = 50;
    private int posY = 320;
    private float walkingSpeed = 150.0f;
    private float runningSpeed = 300.0f;
    private bool wasTouched;

    /// <summary>
    /// Initializes a new instance of the <see cref="Platform"/> class.
    /// </summary>
    /// <param name="positionX">The position of the plate on the x axis.</param>
    public Platform(int positionX)
    {
        this.posX = positionX;
        this.position = new Vector2(this.posX, this.posY);
        this.wasTouched = false;
    }

    /// <summary>
    /// Gets the position of the platform.
    /// </summary>
    /// <returns>The position of the platform.</returns>
    public Vector2 GetPosition()
    {
        return this.position;
    }

    /// <summary>
    /// Gets the width of the platform.
    /// </summary>
    /// <returns>The width of the platform.</returns>
    public int GetWidth()
    {
        return this.width;
    }

    /// <summary>
    /// Gets the height of the platform.
    /// </summary>
    /// <returns>The height of the platform.</returns>
    public int GetHeight()
    {
        return this.height;
    }

    /// <summary>
    /// Gets the bool, wasTouched.
    /// </summary>
    /// <returns>wasTouched.</returns>
    public bool GetwasTouched()
    {
        return this.wasTouched;
    }

    /// <summary>
    /// Sets wasTouched to true.
    /// </summary>
    public void SetwasTouched()
    {
        this.wasTouched = true;
    }

    /// <summary>
    /// Returns the bounding box of the platform.
    /// </summary>
    /// <returns>A rectangle which represents the bounding box.</returns>
    public Rectangle GetBoundingBox()
    {
        this.ScaleTexture(this.texture);
        return new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
    }

    /// <summary>
    /// Return left border of the platform.
    /// </summary>
    /// <returns>Vector2.</returns>
    public Vector2 GetLeftPlatformBorder()
    {
        return new Vector2(this.position.X - (this.width / 2), this.position.Y);
    }

    /// <summary>
    /// Return right border of the platform.
    /// </summary>
    /// <returns>Vector2.</returns>
    public Vector2 GetRightPlatformBorder()
    {
        return new Vector2(this.position.X + (this.width / 2), this.position.Y);
    }

    /// <summary>
    /// Load Content.
    /// </summary>
    /// <param name="content">Content.</param>
    public void LoadContent(ContentManager content)
    {
        this.texture = content.Load<Texture2D>("platform");
    }

    /// <summary>
    /// Updating game.
    /// </summary>
    /// <param name="gameTime">Time of game.</param>
    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.A))
        {
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                this.position.X += this.runningSpeed * deltaTime;
            }
            else
            {
                this.position.X += this.walkingSpeed * deltaTime;
            }
        }
        else if (keyboardState.IsKeyDown(Keys.D))
        {
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                this.position.X -= this.runningSpeed * deltaTime;
            }
            else
            {
                this.position.X -= this.walkingSpeed * deltaTime;
            }
        }
    }

    /// <summary>
    /// ScaleTexture.
    /// </summary>
    /// <param name="texture">scaling of the texture.</param>
    public void ScaleTexture(Texture2D texture)
    {
        float scale = 0.25f; // Modify this as needed

        // Calculate the new dimensions based on the scale
        this.width = (int)(texture.Width * scale);
        this.height = (int)(texture.Height * scale);
    }

    /// <summary>
    /// Draws the platform.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        this.ScaleTexture(this.texture);
        spriteBatch.Draw(this.texture, new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), Color.White);
    }

    /// <summary>
    /// Returns platform width.
    /// </summary>
    /// <returns>width: int.</returns>
    public int ReturnWidth()
    {
        return this.width;
    }

    /// <summary>
    /// Resets the was touched field.
    /// </summary>
    public void ResetTouched()
    {
        this.wasTouched = false;
    }
}
