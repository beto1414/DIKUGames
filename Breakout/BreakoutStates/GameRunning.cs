using Breakout.Blocks;
using System;
using DIKUArcade.State;
using DIKUArcade.Input; 
using DIKUArcade.Entities;
using Breakout.LevelLoading;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using Breakout.PowerUps;

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState{
        public EntityContainer<Block> blocks = new EntityContainer<Block>();
        public static EntityContainer<Ball> balls;
        public EntityContainer<PowerUp> powerUps = new EntityContainer<PowerUp>();
        private static GameRunning instance = null;
        private Entity backGroundImage;
        private bool GameState = true;
        public static Player player {get;private set;}
        public static bool infiniteMode = false;
        private ScoreBoard scoreBoard;
        public static LivesLeft livesLeft;
        private TimeBoard timeBoard;
        public MetaReader normalBlock;
        public string[] levels = new String[]{"level1.txt","level2.txt","level3.txt","level4.txt"};
        public int counter = 0;
        public Random rand;

        private StaticTimer stopWatch = new StaticTimer();

        private void GameOver() {
            GameState = false;
        }
        
        public void ResetState() {
            counter = 0;
            infiniteMode = false;
            Ball.MOVEMENT_SPEED = 0.015f;
            Loader.Reader(Path.Combine("Assets","Levels",levels[counter]));
            blocks = Loader.DrawMap();
            powerUps.ClearContainer();
            rand = new Random();
            backGroundImage = new Entity(
                new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            player = new Player(new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.20f, 0.05f)), 
                new Image(Path.Combine("Assets","Images","player.png")));
            balls = new EntityContainer<Ball>();
            balls.AddEntity(new Ball(new Vec2F(0.5f, 0.1f),new Vec2F((float)rand.NextDouble()-rand.Next(1),(float)rand.NextDouble())));
            foreach(Ball x in balls) {x.AlignSpeed();}
            scoreBoard = new ScoreBoard("Score: ",new Vec2F(0.0f,0.6f), new Vec2F(0.4f,0.4f));
            livesLeft = new LivesLeft("Lives left: ",new Vec2F(0.0f,0.1f), new Vec2F(0.4f,0.4f));
            timeBoard = new TimeBoard("Time left: ",new Vec2F(0.0f,0.2f), new Vec2F(0.4f,0.4f));
            StaticTimer.RestartTimer();
            if (Loader.Time > 0.00001){
                timeBoard.SetTimer(Loader.Time);
                timeBoard.levelHasTimer = true;
            }
            //powerUps.AddEntity(new ExtraLife(new Vec2F(0.5f,0.6f)));
            powerUps.AddEntity(new DoubleSpeed(new Vec2F(0.5f, 0.6f)));
            // powerUps.AddEntity(new ExtraBall(new Vec2F(0.5f, 0.6f)));
            // powerUps.AddEntity(new ExtraLife(new Vec2F(0.5f, 0.6f)));
            // powerUps.AddEntity(new HalfSpeed(new Vec2F(0.5f, 0.6f)));
            //powerUps.AddEntity(new Infinite(new Vec2F(0.5f, 0.6f)));
            
        }
        public void UpdateState() {
            IterateBallz();
            IteratePowerUps();
            player.Move();
            timeBoard.RunClock();
            if (balls.CountEntities() == 0) {
                Console.WriteLine("Life has been lost mf");
                livesLeft.LoseLife();
                balls.AddEntity(new Ball(new Vec2F(player.getShape().Position.X + player.getExtent()/2,player.getShape().Position.Y),new Vec2F((float)rand.NextDouble()-rand.Next(1),(float)rand.NextDouble())));
                foreach(Ball x in balls) {x.AlignSpeed();}
            }
            if (blocks.CountEntities() - CountUnbreakable() == 0) {
                if (counter < levels.Length-1) {
                    counter++;
                    balls.ClearContainer();
                    balls.AddEntity(new Ball(new Vec2F(0.5f, 0.1f),new Vec2F(-0.02f,0.01f)));
                    foreach(Ball x in balls) {x.AlignSpeed();}
                    Loader.Reader(Path.Combine("Assets","Levels",levels[counter]));
                    blocks = Loader.DrawMap();
                } else {
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{EventType = GameEventType.GameStateEvent, 
                            From = this,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_WON"});
                }
            }
            if (livesLeft.lives == 0 || timeBoard.secondsLeft == 0) {
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{EventType = GameEventType.GameStateEvent, 
                            From = this,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_LOST"});
            }
        }
        public int CountUnbreakable(){
            var count =0 ;
            foreach (Block item in blocks) {
                if (item.blockType == BlockType.Unbreakable) {
                    count++; 
                }
            }
            return count;
        }
        
        public void RenderState() { 
            backGroundImage.RenderEntity();
            if (GameState) {
                player.Render();
                scoreBoard.RenderText();
                livesLeft.RenderText();
                powerUps.RenderEntities();
                balls.RenderEntities();
                blocks.Iterate(block => {
                    if (block.blockType == BlockType.Invisible) {
                        if (Invisible.visible) {
                            block.RenderEntity();
                    }} else {
                    block.RenderEntity();
                    }
                });
            } 
            if (GameState && timeBoard.levelHasTimer) {timeBoard.RenderText();}
        }
        public void HandleKeyEvent(KeyboardAction KeyAction, KeyboardKey keyValue) {
            if(KeyAction == KeyboardAction.KeyRelease) {
                // if (!infiniteMode) {
                // }
                // else {
                //     player.KeyRelease(keyValue);
                // }
                player.KeyRelease(keyValue);
                switch(keyValue) {
                    case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent{EventType = GameEventType.GameStateEvent, 
                        From = this,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_PAUSED"});
                    StaticTimer.PauseTimer();
                    break;
                    case KeyboardKey.Q:
                        if (counter < levels.Length-1) {
                            counter++;
                            balls.ClearContainer();
                            balls.AddEntity(new Ball(new Vec2F(0.5f, 0.1f),new Vec2F(-0.02f,0.01f)));
                            foreach(Ball x in balls) {x.AlignSpeed();}
                            Loader.Reader(Path.Combine("Assets","Levels",levels[counter]));
                            blocks = Loader.DrawMap();
                        } else {
                            BreakoutBus.GetBus().RegisterEvent(
                                new GameEvent{EventType = GameEventType.GameStateEvent, 
                                    From = this,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "MAIN_MENU"});
                        }
                        break;
                }
            }
            if(KeyAction == KeyboardAction.KeyPress) {
                player.KeyPress(keyValue);
            }  
        }

        public GameRunning() {
            ResetState();
        }

        public static GameRunning GetInstance () {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }


        public Vec2F BounceDirection(CollisionDirection dir, Ball ball) {
            if (dir == CollisionDirection.CollisionDirUp || dir == CollisionDirection.CollisionDirDown) {
                                    return new Vec2F(ball.Shape.AsDynamicShape().Direction.X,
                                        ball.Shape.AsDynamicShape().Direction.Y*-1);
            } else if (dir == CollisionDirection.CollisionDirLeft || dir == CollisionDirection.CollisionDirRight) {
                    return new Vec2F(ball.Shape.AsDynamicShape().Direction.X*-1,
                        ball.Shape.AsDynamicShape().Direction.Y);
            } else {return new Vec2F(ball.Shape.AsDynamicShape().Direction.X,
                        ball.Shape.AsDynamicShape().Direction.Y);}
            }

        ///<summary>
        ///Iterate balls vil tage sig af hvilke positioner forskellige entities har i forhold til hindanden og udfra det
        ///vurdere om to entities kollidere og i det tilfælde ændre balls direction og position.
        ///</summary>
        public void IterateBallz() {
            balls.Iterate(ball => {
                ball.Shape.Move();
                if (ball.Shape.Position.Y + ball.Shape.Extent.Y < 0.0f) {
                    ball.DeleteEntity();
                    }
                if (ball.Shape.Position.Y + ball.Shape.Extent.Y >= 1.0f) {
                    ball.Shape.AsDynamicShape().ChangeDirection(new Vec2F(ball.Shape.AsDynamicShape().Direction.X,
                        -ball.Shape.AsDynamicShape().Direction.Y));
                    ball.AlignSpeed();
                }
                if (ball.Shape.Position.X + ball.Shape.AsDynamicShape().Extent.X <= 0.0f || ball.Shape.Position.X+ball.Shape.Extent.X >= 1.0f) {
                    ball.Shape.AsDynamicShape().ChangeDirection(new Vec2F(ball.Shape.AsDynamicShape().Direction.X*-1,
                        ball.Shape.AsDynamicShape().Direction.Y));
                    ball.AlignSpeed();
                } else {
                blocks.Iterate(block => {
                    if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).Collision) { 
                        block.Hit();
                        ball.Shape.AsDynamicShape().ChangeDirection(BounceDirection(
                            CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).CollisionDir, ball));
                        ball.AlignSpeed();  
                    }
                    if(block.hitPoint <= 0) {
                        if (!block.unbreakable) {
                            block.DeleteEntity();
                            scoreBoard.AddPoints(block.blockValue);
                        }
                    }
                });}
                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.getEntity().Shape).Collision) {
                    if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.getEntity().Shape).CollisionDir == CollisionDirection.CollisionDirUp) {//} || 
                        ball.Shape.AsDynamicShape().ChangeDirection(
                            new Vec2F(-(player.getPos().X+(player.getExtent()/2.0f)-(ball.Shape.Position.X+(ball.Shape.Extent.X))), 0.1f)); //x is the difference between player.pos and ball.pos. y is a constant value
                        ball.AlignSpeed();
                    }
                    else if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.getEntity().Shape).CollisionDir == CollisionDirection.CollisionDirLeft || 
                        CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.getEntity().Shape).CollisionDir == CollisionDirection.CollisionDirRight) {
                        ball.Shape.AsDynamicShape().ChangeDirection(new Vec2F(ball.Shape.AsDynamicShape().Direction.X*-1, 
                            ball.Shape.AsDynamicShape().Direction.Y));
                        ball.AlignSpeed();
                    } else {
                        ball.Shape.AsDynamicShape().ChangeDirection(new Vec2F(ball.Shape.AsDynamicShape().Direction.X, 
                        ball.Shape.AsDynamicShape().Direction.Y));   
                    }
                }
            });
        }

        private void IteratePowerUps() {
            powerUps.Iterate(powerUp => {
                powerUp.Move();
                if (CollisionDetection.Aabb(powerUp.Shape.AsDynamicShape(), player.getEntity().Shape.AsDynamicShape()).Collision) {
                    powerUp.Activate();
                    powerUp.DeleteEntity();
                } else if (powerUp.Shape.Position.Y+powerUp.Shape.Extent.Y < 0.0f){
                    powerUp.DeleteEntity();
                }
            });
        }
    }
}
