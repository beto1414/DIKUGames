using NUnit.Framework;
using Breakout;
using System.IO;
using System;
using Breakout.PowerUps;
using Breakout.LevelLoading;
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
    public class WinLoseTests {
        private WindowArgs winArgs;
        private GameRunning state;
        private GameTimer gameTimer;
        private StateMachine stateMachine;

        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs();
            gameTimer = new GameTimer();
            
            stateMachine = new StateMachine(); 
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent,stateMachine);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent,stateMachine);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, stateMachine);
            state = new GameRunning();
        }

        [Test]
        public void TestLifeNegative() {
            for (int i = 0;i<3;i++) {
                GameRunning.livesLeft.AddLife();
            }
            for (int i = 0;i<10;i++) {
                GameRunning.livesLeft.LoseLife();
                Assert.IsTrue(GameRunning.livesLeft.lives > -0.00001);
            }
        }

        [Test]
        public void PlayerLosesLifeOneBall() {
            GameRunning.balls.ClearContainer();
            GameRunning.balls.AddEntity(new Ball(new Vec2F(0.5f,0.5f),new Vec2F(0.0f, -0.3f)));
            for (int i = 0;i<10000;i++) {
                state.UpdateState();
            }
            //3 lives is the starting amount, this would confirm health diminishing when balls are lost.
            Assert.IsTrue(GameRunning.livesLeft.lives < 3);
        }
        [Test]
        public void LivesWhileBallsInPlay() {
            GameRunning.balls.ClearContainer();
            GameRunning.balls.AddEntity(new Ball(new Vec2F(0.5f,0.5f),new Vec2F(0.0f, -0.2f)));
            GameRunning.balls.AddEntity(new Ball(new Vec2F(0.5f,0.5f),new Vec2F(0.0f, 0.0f)));
            foreach(Ball x in GameRunning.balls) {x.AlignSpeed();}
            for (int i = 0;i<5;i++) {
                state.IterateBallz();
            }
            //Since >1 ball is always at play, losing one ball would not lose the player a life.
            Assert.IsTrue(GameRunning.livesLeft.lives == 3);
        }
        [Test]
        public void TimePerMap() {
        var level = new float[]{300,180,180};
        var counter = 0;
        var levels = new String[]{"level1.txt","level2.txt","level3.txt"};
        foreach (float i in level) {
            CreateLevel.ReadLevelFile(Path.Combine(FileIO.GetProjectPath(),Path.Combine("Assets","Levels",levels[counter])));
            state.blocks = CreateLevel.DrawMap();
            if (CreateLevel.Time > 0.00001){
                state.timeBoard.SetTimer(CreateLevel.Time);
                state.timeBoard.levelHasTimer = true;}
            if (counter < 3) {counter++;}
            Console.WriteLine(state.timeBoard.secondsLeft);
            Console.WriteLine(i);
            Console.WriteLine(i == state.timeBoard.secondsLeft);
            Assert.IsTrue(state.timeBoard.secondsLeft - i < 0.00001);
        }
        }
        [Test]
        public void LoseGameTimerZero() {

        }
        [Test]
        public void LoseGameLivesZero() {
        }
    }
}