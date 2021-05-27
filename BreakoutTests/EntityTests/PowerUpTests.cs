using NUnit.Framework;
using Breakout;
using Breakout.PowerUps;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Utilities;

namespace BreakoutTests.EntityTests {
    [TextFixture]
    public class PowerUpTests {
        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs(); 
            newGame = new Game(winArgs);
            state = new GameRunning();
            doubleSpeed = new DoubleSpeed(new DynamicShape(new Vec2F(0.5f, 0.5f)));
            tolerance = 0.0000001f;
        }

        [Test]
        public void TestPowerUpMove() {
            doubleSpeed.Move();
            Assert.IsTrue(doubleSpeed.Shape.Position.Y - 0.49f < tolerance);
        }

        [Test]
        public void TestDeletePowerUp() {
                GameRunning.powerUps.AddEntity(doubleSpeed);
            for (int i = 0; i<100; i++) {
                GameRunning.IteratePowerUps;
            }
                Assert.IsTrue(GameRunning.powerUps.CountEntities() == 0);
        }

        [Test]
        public void TestSpeedUpActivateSpeedUp() {

        }
    }
}