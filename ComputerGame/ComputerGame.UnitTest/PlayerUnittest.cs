using ComputerGame.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ComputerGame.UnitTest;

[TestClass]
public class PlayerUnittest
{

    [TestMethod]
    public void PlayerMovementTest_right_Animation()
    {
        var player = new Player();
        var deltaTime1 = 1.0f;
        var deltaTime2 = 2.0f;
        var keyboarstate = new KeyboardState(Keys.D);
        Vector2 expposition1s = player.PlayerPosition;
        Vector2 position1 = new Vector2();
        Vector2 position2 = new Vector2();
        bool isMoving = false;
        //expected position const Wert
        expposition1s.X = 120f;


        player.HandleMovement(deltaTime1, keyboarstate, ref isMoving);
        position1 = player.PlayerPosition;
        player.HandleMovement(deltaTime2, keyboarstate, ref isMoving);
        position2 = player.PlayerPosition;

        //Test Bewegung nach 1s
        Assert.IsTrue(isMoving);
        Assert.AreEqual(player.AnimationHandler.WalkAnimation, player.AnimationHandler.CurrentAnimation);
        Assert.AreEqual(expposition1s, position1);

    }

    [TestMethod]
    public void PlayerMovement_left_Animation()
    {
        var player = new Player();
        var deltaTime = 1.0f;
        var keyboarstate = new KeyboardState(Keys.A);
        Vector2 expposition1s = player.PlayerPosition;
        expposition1s.X = -120.0f;
        Vector2 position1 = new Vector2();
        bool isMoving = false;

        player.HandleMovement(deltaTime, keyboarstate, ref isMoving);
        position1 = player.PlayerPosition;

        //Test Bewegung nach 1s
        Assert.IsTrue(isMoving);
        Assert.AreEqual(player.AnimationHandler.WalkAnimation, player.AnimationHandler.CurrentAnimation);
        Assert.AreEqual(expposition1s, position1);
    }

    [TestMethod]
    public void PlayerMovement_running_left_Animation()
    {
        var player = new Player();
        var deltaTime = 1.0f;
        var keyboarstate = new KeyboardState(Keys.A, Keys.LeftShift);
        Vector2 expposition1s = player.PlayerPosition;
        expposition1s.X = -150f;
        Vector2 position1 = new Vector2();
        bool isMoving = false;

        player.HandleMovement(deltaTime, keyboarstate, ref isMoving);
        position1 = player.PlayerPosition;

        //Test Bewegung nach 1s
        Assert.IsTrue(isMoving);
        Assert.AreEqual(player.AnimationHandler.RunAnimation, player.AnimationHandler.CurrentAnimation);
        Assert.AreEqual(expposition1s, position1);
    }

    [TestMethod]
    public void PlayerMovement_running_right_Animation()
    {
        var player = new Player();
        var deltaTime = 1.0f;
        var keyboarstate = new KeyboardState(Keys.D, Keys.LeftShift);
        Vector2 expposition1s = player.PlayerPosition;
        expposition1s.X = 150f;
        Vector2 position1 = new Vector2();
        bool isMoving = false;

        player.HandleMovement(deltaTime, keyboarstate, ref isMoving);
        position1 = player.PlayerPosition;

        //Test Bewegung nach 1s
        Assert.IsTrue(isMoving);
        Assert.AreEqual(player.AnimationHandler.RunAnimation, player.AnimationHandler.CurrentAnimation);
        Assert.AreEqual(expposition1s, position1);
    }


    [TestMethod]
    public void PlayerMovement_Jump_Animation()
    {
        var player = new Player();
        var deltaTime = 1.0f;
        Vector2 exppositionFirstStep = player.PlayerPosition;
        Vector2 exppositionSecondStep = player.PlayerPosition;
        Vector2 exppositionThirdStep = player.PlayerPosition;
        Vector2 position1 = new Vector2();
        Vector2 position2 = new Vector2();
        Vector2 position3 = new Vector2();
        var keyboardstate = new KeyboardState(Keys.Space);


        player.HandleJumping(deltaTime, keyboardstate);
        position1 = player.PlayerPosition;
        exppositionFirstStep.Y = -550.0f;
        player.HandleJumping(deltaTime, keyboardstate);
        position2 = player.PlayerPosition;
        exppositionSecondStep.Y = -100.0f;
        player.HandleJumping(deltaTime, keyboardstate);
        position3 = player.PlayerPosition;
        exppositionThirdStep.Y = 300.0f;


        //Test Bewegung nach 1s
        Assert.AreEqual(player.AnimationHandler.JumpAnimation, player.AnimationHandler.CurrentAnimation);
        Assert.AreEqual(exppositionFirstStep.Y, position1.Y);
        Assert.AreEqual(exppositionSecondStep.Y, position2.Y);
        Assert.AreEqual(exppositionThirdStep.Y, position3.Y);

    }

    [TestMethod]
    public void Player_Speed_Jump_right_walking()
    {
        var player = new Player();
        bool isMoving = true;
        KeyboardState keyboarstate = new KeyboardState(Keys.Space, Keys.D);
        float deltatime1s = 1;
        float playerspeedexp = 60.0f;

        player.HandleMovement(deltatime1s, keyboarstate, ref isMoving);
        float playerspeed = player.GetPlayerhorizontalSpeed();

        Assert.AreEqual(playerspeedexp, playerspeed);



    }

    [TestMethod]
    public void Player_Speed_Jump_left_walking()
    {
        var player = new Player();
        bool isMoving = true;
        KeyboardState keyboarstate = new KeyboardState(Keys.Space, Keys.A);
        float deltatime1s = 1;
        float Playerspeed = player.GetPlayerSpeed();
        float Playerspeedexp = -60.0f;

        player.HandleMovement(deltatime1s, keyboarstate, ref isMoving);
        Playerspeed = player.GetPlayerhorizontalSpeed();

        Assert.AreEqual(Playerspeedexp, Playerspeed);

    }

    [TestMethod]
    public void Player_Test_Get_Methodes()
    {
        var player = new Player();
        float expspeed = 0f;
        float jumpspeed = player.JumpSpeed;

        Assert.AreEqual(expspeed, jumpspeed);
    }

    [TestMethod]
    public void Player_Reset()
    {
        var player = new Player();
        Vector2 Positionexp = new Vector2(0, 200);
        Vector2 Position = player.PlayerPosition;

        player.ResetPlayerPosition();
        Position = player.PlayerPosition;

        Assert.AreEqual(Positionexp, Position);
    }
}
