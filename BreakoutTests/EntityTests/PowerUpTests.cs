using NUnit.Framework;
using Breakout;
using System.IO;
using System;
using Breakout.PowerUps;
using Breakout.BreakoutStates;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
namespace BreakoutTests.EntityTests {
    [TestFixture]
    public class PowerUpTests {
        private WindowArgs winArgs;
        private Game newGame;
        private ExtraLife extraLife;
        private DoubleSpeed doubleSpeed;
        private HalfSpeed halfSpeed;
        private float tolerance;
        private GameTimer gameTimer;
        //private GameRunning state;

        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs(); 
            newGame = new Game(winArgs);
            //state = new GameRunning();
            BreakoutBus.GetBus().RegisterEvent(            
                new GameEvent{EventType = GameEventType.GameStateEvent, 
                    From = this,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING",
                    StringArg2 = "RESET_STATE"});
            BreakoutBus.GetBus().ProcessEvents();
            extraLife = new ExtraLife(new Vec2F(0.0f, 0.5f));
            doubleSpeed = new DoubleSpeed(new Vec2F(0.5f, 0.5f));
            halfSpeed = new HalfSpeed(new Vec2F(0.5f, 0.5f));
            tolerance = 0.0000001f;
            gameTimer = new GameTimer();
        }

        [Test]
        public void TestPowerUpMove() {
            doubleSpeed.Move();
            Assert.IsTrue(doubleSpeed.Shape.Position.Y - 0.49f < tolerance);
        }

        [Test]
        public void TestDeletePowerUp() {
                newGame.stateMachine.ActiveState.powerUps.AddEntity(doubleSpeed);
            for (int i = 0; i<100; i++) {
                newGame.stateMachine.ActiveState.GetInstance().IteratePowerUps();
            }
                Assert.IsTrue(state.powerUps.CountEntities() == 0);
        }

        [Test]
        public void TestSpeedUpActivateSpeedUp() {
            doubleSpeed.Activate();
            Assert.IsTrue(Ball.MOVEMENT_SPEED == 0.03f);
        }
        [Test]
        public void TestNormalizeSpeed() {
            doubleSpeed.Activate();
            for (int i = 0; i<150; i++) {
                gameTimer.ShouldUpdate();
            }
            Assert.IsTrue(Ball.MOVEMENT_SPEED == 0.015f);
        }

        [Test]
        public void TestShouldNotNormalize() {
            doubleSpeed.Activate();
            for (int i = 0; i<100; i++) {
                gameTimer.ShouldUpdate();
            }
            doubleSpeed.Activate();
            for (int i = 0; i<100; i++) {
                gameTimer.ShouldUpdate();
            }
                Assert.IsTrue(Ball.MOVEMENT_SPEED == 0.03f);
        }

        [Test]
        public void TestActivateHalfSpeed() {
            halfSpeed.Activate();
            Assert.IsTrue(Ball.MOVEMENT_SPEED == 0.0075f);
        }
        
        ///<summary> Tester om player for extra liv. livesLeft er standard 3,
        /// ved at sætte den til at være lig 4 kan vi så om der bliver tilføjet
        /// et ekstra liv </summary>
        [Test]
        public void TestExtraLife() {
            extraLife.Activate();
            Assert.IsTrue(GameRunning.livesLeft.lives == 4);
        }
        
        // [Test]
        // public void TestExtraLife2() {
        // }



    }
}