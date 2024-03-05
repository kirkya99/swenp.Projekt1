// <copyright file="Player.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Players;

using ComputerGame.Platforms;
using ComputerGame.Players.Handlers;
using ComputerGame.Views.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Represents the player character.
/// </summary>
public class Player : IPlayer
{
    private Vector2 position;
    private bool isMoving;
    private bool isJumping;
    private bool isOnGround = true; // true for the HandleJump test
    private bool isRunning;
    private float horizontalSpeed = 0.0f;
    private float speed = 120.0f;
    private float jumpSpeed;
    private float gravity = 1000.0f;
    private float runSpeed = 150.0f; // running speed

    /// <summary>
    /// Gets the animation handler.
    /// </summary>
    public AnimationHandler AnimationHandler { get; } = new AnimationHandler();

    /// <inheritdoc/>
    public int Width { get => this.AnimationHandler.CurrentAnimation.FrameWidth; }

    /// <inheritdoc/>
    public int Height { get => this.AnimationHandler.CurrentAnimation.FrameHeight; }

    /// <inheritdoc/>
    public float JumpSpeed { get => this.jumpSpeed; }

    /// <summary>
    /// Gets the bounding box of the player.
    /// </summary>
    public Rectangle BoundingBox => new Rectangle((int)this.position.X + 40, (int)this.position.Y, this.Width - 90, this.Height);

    /// <inheritdoc/>
    public Vector2 PlayerPosition => this.position;

    /// <summary>
    /// Gets the player's speed.
    /// </summary>
    /// <returns>The current speed of the player.</returns>
    public float GetPlayerSpeed()
    {
        return this.speed;
    }

    /// <summary>
    /// Gets the player's horizontal speed.
    /// </summary>
    /// <returns>The current horizontal speed of the player.</returns>
    public float GetPlayerhorizontalSpeed()
    {
        return this.horizontalSpeed;
    }

    /// <summary>
    /// Loads the player content.
    /// </summary>
    /// <param name="content">The content manager.</param>
    public void LoadContent(ContentManager content)
    {
        this.AnimationHandler.LoadContent(content);
    }

    /// <summary>
    /// Updates the player state based on game time.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    /// <param name="platformCollection">The collection of platforms in the game.</param>
    /// <param name="scoreManager">ScoreManger.</param>
    public void Update(GameTime gameTime, PlatformCollection platformCollection, ScoreManager scoreManager)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var keyboardState = Keyboard.GetState();
        this.isMoving = false;
        this.CheckIfOnGround(platformCollection, scoreManager); // Checks if the player is on the ground

        if (!this.isOnGround && !this.isJumping)
        {
            // Application of the gravity even when isJumping is false
            this.jumpSpeed += this.gravity * deltaTime;
            this.position.Y += this.jumpSpeed * deltaTime;
        }

        // Movement and animations
        this.HandleMovement(deltaTime, keyboardState, ref this.isMoving);

        if (!this.isMoving && !this.isJumping)
        {
            this.AnimationHandler.SetIdleAnimation();
            this.AnimationHandler.SetSpriteEffectsNone();
        }

        this.AnimationHandler.Update(gameTime);
    }

    /// <summary>
    /// Checks if the player is on the ground.
    /// </summary>
    /// <param name="platformCollection">The platform list.</param>
    /// <param name="scoreManager">The ScoreManger instance.</param>
    public void CheckIfOnGround(PlatformCollection platformCollection, ScoreManager scoreManager)
    {
        this.isOnGround = false;
        foreach (var platform in platformCollection.GetPlatforms())
        {
            if (CollisionDetector.CheckCollision(this, platform))
            {
                this.isJumping = false;
                this.jumpSpeed = 0;
                this.position.Y = platform.GetPosition().Y - this.Height + 1;
                this.isOnGround = true;

                if (!platform.GetwasTouched())
                {
                    platform.SetwasTouched();
                    scoreManager.IncreaseScore();
                }

                break;
            }
        }
    }

    /// <summary>
    /// Draws the player using the current animation.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        this.AnimationHandler.Draw(spriteBatch, this.position);
    }

    /// <summary>
    /// Handles player movement and animations.
    /// </summary>
    /// <param name="deltaTime">The elapsed game time.</param>
    /// <param name="keyboardState">The current state of the keyboard.</param>
    /// <param name="isMoving">A reference to the isMoving flag.</param>
    public void HandleMovement(float deltaTime, KeyboardState keyboardState, ref bool isMoving)
    {
        if (this.isJumping == false)
        {
            if (keyboardState.IsKeyDown(Keys.D))
            {
                this.Move(deltaTime, ref keyboardState, 1, ref isMoving);
                this.AnimationHandler.SetSpriteEffectsNone();
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                this.Move(deltaTime, ref keyboardState, -1, ref isMoving);
                this.AnimationHandler.SetSmokeEffectsFlippedHorizontally();
            }
        }

        this.HandleJumping(deltaTime, keyboardState);
    }

    /// <summary>
    /// Handles the jumping logic for the player.
    /// </summary>
    /// <param name="deltaTime">The elapsed game time.</param>
    /// <param name="keyboardState">The current state of the keyboard.</param>
    public void HandleJumping(float deltaTime, KeyboardState keyboardState)
    {
        this.isRunning = keyboardState.IsKeyDown(Keys.LeftShift);
        bool wasMoving = this.isMoving; // Save the previous movement state

        if (this.isOnGround && keyboardState.IsKeyDown(Keys.Space))
        {
            this.isJumping = true;
            this.isOnGround = false;
            this.jumpSpeed = -550.0f;
        }

        if (this.isJumping)
        {
            // If the player is running, twice as fast as running. (is running? True : False : Null)
            this.horizontalSpeed = (keyboardState.IsKeyDown(Keys.D) ? this.isRunning ? this.runSpeed * 2 : this.speed : 0.0f)
                           - (keyboardState.IsKeyDown(Keys.A) ? this.isRunning ? this.runSpeed * 2 : this.speed : 0.0f);

            if ((this.horizontalSpeed > 0 && !wasMoving) || (this.horizontalSpeed < 0 && !wasMoving))
            {
                this.horizontalSpeed *= 0.5f; // Half of the speed if the direction is changed in the air
            }

            this.position.X += this.horizontalSpeed * deltaTime;
            this.position.Y += this.jumpSpeed * deltaTime;
            this.jumpSpeed += this.gravity * deltaTime;
            this.AnimationHandler.SetJumpAnimation();

            if (this.position.Y > 300)
            {
                this.position.Y = 300;
                this.isJumping = false;
                this.isOnGround = true;
            }
        }
    }

    /// <summary>
    /// Resets the player position.
    /// </summary>
    public void ResetPlayerPosition()
    {
        this.position.X = 0;
        this.position.Y = 200;
    }

    /// <summary>
    /// Executes the walk and run logic.
    /// </summary>
    /// <param name="deltaTime">GameTime.</param>
    /// <param name="keyboardState">Current Keyboard state.</param>
    /// <param name="moveDirection">Current move direction. Positive number equals moving right and negative number equals moving left.</param>
    /// <param name="isMoving">isMoving.</param>
    private void Move(float deltaTime, ref KeyboardState keyboardState, int moveDirection, ref bool isMoving)
    {
        // Moving left or right
        if (keyboardState.IsKeyDown(Keys.LeftShift))
        {
            this.position.X += moveDirection * this.runSpeed * deltaTime; // Increased running speed
            this.AnimationHandler.SetRunAnimation();
        }
        else
        {
            this.position.X += moveDirection * this.speed * deltaTime; // Normal walking speed
            this.AnimationHandler.SetWalkAnimation();
        }

        isMoving = true;
    }
}
