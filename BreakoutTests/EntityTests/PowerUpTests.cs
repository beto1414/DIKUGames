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
            doubleSpeed = new DoubleSpeed(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.05f, 0.05f)), 
                new Image(Path.Combine(FileIO.GetProjectPath(),"Assets", "Images", "DoubleSpeedPowerUp.png")));
        }
    }
}