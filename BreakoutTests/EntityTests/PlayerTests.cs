using NUnit.Framework;
using Breakout;

namespace EntityTests {
    public class PlayerTests {
        private Player player;
        
        [SetUp]
        public void SetUp() {
            player = new Player(new DynamicShape(new Vec2F(0.5f, 0.05f), new Vec2F(0.20f, 0.05f)), 
                new Image(Path.Combine("Assets","Images","player.png")));
        }

        [Test]
        public void TestGetPos() {
            Assert.IsTrue(player.getPos() == 0.5f);
        }

        [Test]
        public void TestMoveRight() {
            SetMoveRight(true);
            player.Move();
            Assert.IsTrue(player.getPos() == 0.51f);
        }

        [Test]
        public void TestMoveLeft() {
            SetMoveLeft(true);
            player.Move();
            Assert.IsTrue(player.getPos() == 0.49f);
        }

        [Test]
        public void TestMoveOutOfBoundsRight() {
            SetMoveRight(true);
            for (int i = 0; i < 100; i++) {
                player.Move();
            }
            Assert.IsTrue(player.getPos() == (1.0f - player.Extent.X));
        }

        [Test]
        public void TestMoveOutOfBoundsLeft() {
            SetMoveLeft(true);
            for (int i = 0; i < 100; i++) {
                player.Move();
            }
            Assert.IsTrue(player.getPos() == (0.0f));
        }
    }
}