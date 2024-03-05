// <copyright file="CollisionDetector.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Players;

using ComputerGame.Platforms;
using Microsoft.Xna.Framework;

/// <summary>
/// Provides collision detection functionality for the game.
/// </summary>
public class CollisionDetector
{
    /// <summary>
    /// Checks for a collision between a player and a platform.
    /// </summary>
    /// <param name="player">The player to check for collisions.</param>
    /// <param name="platform">The platform to check for collisions.</param>
    /// <returns>true if there is a collision, otherwise false.</returns>
    public static bool CheckCollision(IPlayer player, Platform platform)
    {
        var playerBounds = GetPlayerBounds(player);
        var platformBounds = GetPlatformBounds(platform);

        return IsPlayerLandingOnPlatform(playerBounds, platformBounds, player.JumpSpeed);
    }

    private static Rectangle GetPlayerBounds(IPlayer player)
    {
        int offsetX = 45; // Displacement on the X axis
        int offsetY = 0; // Shift upwards.
        int boxWidth = player.Width - 90; // Decrease of the width
        int boxHeight = player.Height; // Decrease of the height

        return new Rectangle(
            (int)player.PlayerPosition.X + offsetX,
            (int)player.PlayerPosition.Y + offsetY,
            boxWidth,
            boxHeight);
    }

    private static Rectangle GetPlatformBounds(Platform platform)
    {
        var position = platform.GetPosition();
        var width = platform.GetWidth();
        var height = platform.GetHeight();

        return new Rectangle(
            (int)position.X,
            (int)position.Y,
            width,
            height);
    }

    private static bool IsPlayerLandingOnPlatform(Rectangle playerBounds, Rectangle platformBounds, float jumpSpeed)
    {
        bool isIntersecting = playerBounds.Intersects(platformBounds);
        bool isPlayerBottomNearPlatformTop = playerBounds.Bottom <= platformBounds.Top + 10; // 10 pixel tolerance
        bool isPlayerMovingDown = jumpSpeed >= 0;

        return isIntersecting && isPlayerBottomNearPlatformTop && isPlayerMovingDown;
    }
}