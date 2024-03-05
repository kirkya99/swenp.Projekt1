// <copyright file="Camera.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>

namespace ComputerGame.Views;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Camera.
/// </summary>
public class Camera
{
    /// <summary>
    /// Camera position.
    /// </summary>
    private Vector2 positioncamera;

    /// <summary>
    /// Player point of view.
    /// </summary>
    private Viewport viewport;

    /// <summary>
    /// Initializes a new instance of the <see cref="Camera"/> class.
    /// </summary>
    /// <param name="viewport">viewport.</param>
    public Camera(Viewport viewport) // Sichtpunkt
    {
        this.viewport = viewport;
        this.positioncamera = Vector2.Zero;
        this.ViewMatrix = Matrix.CreateTranslation(new Vector3(-this.positioncamera, 0));
    }

    /// <summary>
    ///  Gets or sets Matrix Camera.
    /// </summary>
    public Matrix ViewMatrix { get; set; } // Matrix für die Camera

    /// <summary>
    ///  Gets or sets Camera Position.
    /// </summary>
    /// <returns>Vector2.</returns>
    /// <returns> The Position of the Camera. </returns>
    public Vector2 GetCameraPosition()
    {
        return this.positioncamera;
    }

    /// <summary>
    /// Update content.
    /// </summary>
    /// <param name="player_position">player_position.</param>
    public void Update(Vector2 player_position)
    {
        this.positioncamera = player_position - new Vector2(this.viewport.Width / 2, 150);
        this.MatrixUpdate();
    }

    private void MatrixUpdate() // Update für Camera
    {
        this.ViewMatrix = Matrix.CreateTranslation(new Vector3(-this.positioncamera, 0));
    }
}
