using NUnit.Framework;
using Breakout;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using DIKUArcade.Timers;
using Breakout.BreakoutStates;

namespace BreakoutTests.EntityTests {
    [TextFixture]
    public class WinLoseTests {
        WindowsArgs winArgs;
        Game newGame;
        float tolerance;
        GameRunning state;
        GameTimer gameTimer;

        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs(); 
            // newGame = new Game(winArgs);
            tolerance = 0.0000001f;
            state = new GameRunning();
            BreakoutBus.GetBus().RegisterEvent(            
                new GameEvent{EventType = GameEventType.GameStateEvent, 
                    From = this,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING",
                    StringArg2 = "RESET_STATE"});
            gameTimer = new GameTimer();
        }

        [Test]
        public void TestLifeNegative() {
            state.LivesLeft.AddLife(3);
            for (int i = 0;i<10;i++) {
                state.LivesLeft.LoseLife();
                Assert.IsTrue(state.LivesLeft.lives > -0.00001);
            }
        }

        [Test]
        public void PlayerLosesLife() {
            state.balls.ClearContainer;
            state.balls.AddEntity(new Ball(new Vec2F(0.5f,0.5f),new Vec2F(0.0f, -0.1f)));
            foreach(Ball x in state.balls) {x.AlignSpeed();}
            for (int i = 0;i<5;i++) {
                state.ShouldUpdate();
            }
            //3 lives is the starting amount, this would confirm such.
            Assert.IsTrue(state.livesLeft.lives < 3);
            //            balls.AddEntity(new Ball(new Vec2F(0.5f, 0.1f),new Vec2F((float)rand.NextDouble()-rand.Next(1),(float)rand.NextDouble())));
            //foreach(Ball x in balls) {x.AlignSpeed();}


        }

        [Test]
        public void LivesWhileBallsInPlay() {
        }
        [Test]
        public void TimePerMap() {
        }
        [Test]
        public void LoseGameTimerZero() {
        }
        [Test]
        public void LoseGameLivesZero() {
        }
    }
}