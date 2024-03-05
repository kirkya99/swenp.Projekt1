// <copyright file="IPlayer.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Players;

using Microsoft.Xna.Framework;

/// <summary>
/// Player interface.
/// </summary>
public interface IPlayer
{
    /// <summary>
    /// Gets the player's position.
    /// </summary>
    Vector2 PlayerPosition { get; }

    /// <summary>
    /// Gets the height of the player.
    /// </summary>
    int Height { get; }

    /// <summary>
    /// Gets the jump speed of the player.
    /// </summary>
    float JumpSpeed { get; }

    /// <summary>
    /// Gets the width of the player.
    /// </summary>
    int Width { get; }
}