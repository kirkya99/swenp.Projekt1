// <copyright file="GameTimeManager.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Views.Managers;

using System;
using ComputerGame;
using Microsoft.Xna.Framework;

/// <summary>
/// Represents a game timer.
/// </summary>
public class GameTimeManager : BaseManager
{
    private int remainingTime;
    private TimeSpan timer;
    private int allowedTime = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameTimeManager"/> class.
    /// </summary>
    /// <param name="position">The position of the timer.</param>
    /// <param name="scale">The scale of the timer.</param>
    public GameTimeManager(Vector2 position, float scale = 1.0f)
        : base(Strings.Time, position, scale)
    {
        this.remainingTime = this.allowedTime; // Startzeit in Sekunden
        this.timer = TimeSpan.FromSeconds(this.remainingTime);
    }

    /// <inheritdoc/>
    public override void Update(GameTime gameTime)
    {
        this.timer -= gameTime.ElapsedGameTime;
        if (this.timer <= TimeSpan.Zero)
        {
            this.timer = TimeSpan.Zero;
        }

        this.remainingTime = (int)this.timer.TotalSeconds;
        this.NumericValue = this.remainingTime;
    }

    /// <summary>
    /// Returns the remaining time.
    /// </summary>
    /// <returns>Integer.</returns>
    public int GetRemainingTime()
    {
        return (int)this.timer.TotalSeconds;
    }

    /// <summary>
    /// Resets the remaining game time.
    /// </summary>
    public void ResetRemainingTime()
    {
        this.timer = TimeSpan.FromSeconds(this.allowedTime);
    }
}