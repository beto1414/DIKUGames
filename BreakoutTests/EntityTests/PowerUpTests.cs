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
using DIKUArcade.Input;
using System.Collections.Generic;
namespace BreakoutTests.EntityTests {
    [TestFixture]
    public class PowerUpTests {
        private WindowArgs winArgs;
        private ExtraLife extraLife;
        private ExtraBall extraBall;
        private DoubleSpeed doubleSpeed;
        private HalfSpeed halfSpeed;
        private Infinite infinite;
        private float tolerance;
        private StateMachine stateMachine;
        private GameTimer gameTimer;
        private GameRunning state;

        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs();
            stateMachine = new StateMachine(); 
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent,stateMachine);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent,stateMachine);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, stateMachine);
            state = new GameRunning();
            extraLife = new ExtraLife(new Vec2F(0.5f, 0.5f));
            extraBall = new ExtraBall(new Vec2F(0.5f, 0.5f));
            doubleSpeed = new DoubleSpeed(new Vec2F(0.5f, 0.5f));
            halfSpeed = new HalfSpeed(new Vec2F(0.5f, 0.5f));
            infinite = new Infinite((new Vec2F(0.5f, 0.5f)));
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
                state.powerUps.AddEntity(doubleSpeed);
            for (int i = 0; i<100; i++) {
                state.IteratePowerUps();
            }
                Assert.IsTrue(state.powerUps.CountEntities() == 0);
        }

        [Test]
        public void TestSpeedUpActivateSpeedUp() {
            doubleSpeed.Activate();
            BreakoutBus.GetBus().ProcessEvents();
            Assert.IsTrue(BreakoutBus.GetBus().HasTimedEvent(69));
        }
        // [Test]
        // public void TestNormalizeSpeed() { //TODO: add timer
        //     doubleSpeed.Activate();
        //     BreakoutBus.GetBus().ProcessEvents();
        //     for (int i = 0; i < 10000; i++) {
        //     state.UpdateState();
        //     }
        //     BreakoutBus.GetBus().ProcessEvents();
        //     Console.WriteLine(gameTimer.CapturedUpdates);
        //     Assert.IsTrue(!BreakoutBus.GetBus().HasTimedEvent(69));
        // }

        // [Test]
        // public void TestShouldNotNormalize() {
        //     doubleSpeed.Activate();
        //     //TODO: add timer
        //     doubleSpeed.Activate();
        //     //TODO: add timer
        //     Assert.IsTrue(BreakoutBus.GetBus().HasTimedEvent(69));
        // }

        [Test]
        public void TestActivateHalfSpeed() {
            halfSpeed.Activate();
            BreakoutBus.GetBus().ProcessEvents();
            Assert.IsTrue(BreakoutBus.GetBus().HasTimedEvent(420));
        }
        
        ///<summary> Tester om player for extra liv. livesLeft er standard 3,
        /// ved at sætte den til at være lig 4 kan vi så om der bliver tilføjet
        /// et ekstra liv </summary>
        [Test]
        public void TestExtraLife() {
            extraLife.Activate();
            Assert.IsTrue(GameRunning.livesLeft.lives == 4);
        }
        
        [Test]
        public void TestExtraball() {
            int startBalls = GameRunning.balls.CountEntities();
            extraBall.Activate();
            int endBalls = GameRunning.balls.CountEntities();
            Assert.IsTrue(endBalls-startBalls == 1);
        }

        [Test]
        public void TestInfiniteBallSpawn() {
            infinite.Activate();
            BreakoutBus.GetBus().ProcessEvents();
            Assert.IsTrue(GameRunning.infiniteMode);
        }

        // [Test]
        // public void TestInfinitePowerOff() {
        //     int startBalls = GameRunning.balls.CountEntities();
        //     infinite.Activate();
        //     for (int i = 0; i < 3000; i++) {
        //         gameTimer.ShouldUpdate();
        //     }
        //     stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Space);
        //     int endBalls = GameRunning.balls.CountEntities();
        //     Assert.IsTrue(endBalls-startBalls == 0);
        // }
    }
}