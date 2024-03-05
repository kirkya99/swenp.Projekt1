using ComputerGame.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGame.UnitTest
{

    [TestClass]
    public class CameraUnittest
    {
       

        [TestMethod]
        public void CameraMovement_follow_left()
        {
            Viewport viewport = new Viewport(0,0,800,600);
            Camera camera = new Camera(viewport);
            Vector2 playerPosition = new Vector2(100, 100);
            camera.Update(playerPosition);
            playerPosition = camera.GetCameraPosition();

            // Assert
            Matrix expectedMatrix = Matrix.CreateTranslation(new Vector3(-playerPosition , 0));
            Assert.AreEqual(expectedMatrix, camera.ViewMatrix);
        }
        [TestMethod]
        public void CameraMovement_follow_right()
        {
            Viewport viewport = new Viewport(0,0,800,600);
            Camera camera = new Camera(viewport);
            Vector2 playerPosition = new Vector2(700, 200);
            camera.Update(playerPosition);
            playerPosition = camera.GetCameraPosition();
            // Assert
            Matrix expectedMatrix = Matrix.CreateTranslation(new Vector3(-playerPosition , 0));
            Assert.AreEqual(expectedMatrix, camera.ViewMatrix);
        }

    }
}
