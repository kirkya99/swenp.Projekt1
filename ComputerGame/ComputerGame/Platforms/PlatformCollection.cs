// <copyright file="PlatformCollection.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Platforms;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Collection of platforms.
/// </summary>
public class PlatformCollection
{
    private int platformWidth = 153;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformCollection"/> class.
    /// </summary>
    /// <param name="numberOfPlatforms">numberOfPlatforms: int.</param>
    public PlatformCollection(int numberOfPlatforms)
    {
        this.CurrentPlatformPosX = 50;
        this.FirstPlatform = new Platform(this.CurrentPlatformPosX);
        this.Platforms.Add(this.FirstPlatform);
        this.NumberOfPlatforms = numberOfPlatforms;
        this.CreatePlatforms();
    }

    /// <summary>
    /// Gets or sets the Platform x position elements.
    /// </summary>
    private int CurrentPlatformPosX { get; set; }

    /// <summary>
    /// Gets or sets the number of platforms.
    /// </summary>
    private int NumberOfPlatforms { get; set; }

    /// <summary>
    /// Gets or sets the first platform.
    /// </summary>
    private Platform FirstPlatform { get; set; }

    /// <summary>
    /// Gets or sets the Platform collection elements.
    /// </summary>
    private List<Platform> Platforms { get; set; } = new List<Platform>();

    /// <summary>
    /// Retrieves a read-only list of the platforms in the collection.
    /// </summary>
    /// <returns>A read-only list (IReadOnlyList) of Platform objects.</returns>
    public IReadOnlyList<Platform> GetPlatforms()
    {
        return this.Platforms.AsReadOnly();
    }

    /// <summary>
    /// Load Content.
    /// </summary>
    /// <param name="content">Parameter ContentManager.</param>
    public void LoadContent(ContentManager content)
    {
        foreach (Platform platform in this.Platforms)
        {
            platform.LoadContent(content);
        }
    }

    /// <summary>
    /// Update sprites.
    /// </summary>
    /// <param name="gameTime">Parameter GameTime.</param>
    public void Update(GameTime gameTime)
    {
        foreach (Platform platform in this.Platforms)
        {
            platform.Update(gameTime);
        }

        // this.CreatePlatforms();
    }

    /// <summary>
    /// Draw sprites.
    /// </summary>
    /// <param name="spriteBatch">Parameter SpriteBatch.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (Platform platform in this.Platforms)
        {
            platform.Draw(spriteBatch);
        }
    }

    /// <summary>
    /// Resets the touched platform bool.
    /// </summary>
    public void ResetTouchedPlatforms()
    {
        foreach (Platform platform in this.Platforms)
        {
            platform.ResetTouched();
        }
    }

    private int CalculateRandomDistance()
    {
        return new Random().Next(75, 175);
    }

    private void CreatePlatforms()
    {
        for (int i = 0; i < this.NumberOfPlatforms; i++)
        {
            this.CurrentPlatformPosX = this.CurrentPlatformPosX + this.platformWidth + this.CalculateRandomDistance();
            this.Platforms.Add(new Platform(this.CurrentPlatformPosX));
        }
    }
}