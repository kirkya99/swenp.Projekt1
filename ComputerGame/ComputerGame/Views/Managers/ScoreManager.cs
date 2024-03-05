// <copyright file="ScoreManager.cs" company="Team14">
// Copyright (c) Team14. All rights reserved.
// </copyright>
namespace ComputerGame.Views.Managers;

using ComputerGame;
using Microsoft.Xna.Framework;

/// <summary>
/// ScoreManager for the Score of the Player.
/// </summary>
public class ScoreManager : BaseManager
{
    private int score = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreManager"/> class.
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="scale">Scale.</param>
    public ScoreManager(Vector2 position, float scale = 1.0f)
        : base(Strings.Score, position, scale)
    {
    }

    /// <summary>
    /// Increases the Score by 1.
    /// </summary>
    public void IncreaseScore()
    {
        this.score++;
        this.UpdateNumericValue();
    }

    /// <summary>
    /// Resets the score.
    /// </summary>
    /// <returns>Score.</returns>
    public int ResetScore()
    {
        int reached_score = this.score;
        this.score = 0;
        this.UpdateNumericValue();
        return reached_score;
    }

    /// <summary>
    /// Get the current score.
    /// </summary>
    /// <returns>score.</returns>
    public int GetScore()
    {
        return this.score;
    }

    private void UpdateNumericValue()
    {
        this.NumericValue = this.score;
    }
}