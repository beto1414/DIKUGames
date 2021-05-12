using Breakout;
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

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState{
        public EntityContainer<Block> blocks;
        public EntityContainer<Ball> balls;
        private static GameRunning instance = null;
        private Entity backGroundImage;
        private bool GameState = true;
        private Player player;
        
        private Random rand;

        private void GameOver() {
            GameState = false;
        }
        
        public void ResetState() { //skriv constructoren her
            Loader.Reader(Path.Combine("Assets","Levels","level1.txt"));
            blocks = Loader.DrawMap();
            backGroundImage = new Entity(
                new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            player = new Player(new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.20f, 0.05f)), 
                new Image(Path.Combine("Assets","Images","player.png")));
            rand = new System.Random();
            balls = new EntityContainer<Ball>();
            balls.AddEntity(new Ball(new Vec2F(0.5f, 0.1f),new Vec2F(-0.02f,0.01f)));
            
            //Convert.ToSingle(rand.Next(10)/10)
            
        }
        public void UpdateState() {
            player.Move();
            IterateBallz();
        }
        public void RenderState() { 
            backGroundImage.RenderEntity();
            if (GameState) {player.Render();} 
            if (GameState) {blocks.RenderEntities();}
            if (GameState) {balls.RenderEntities();}
        }
        public void HandleKeyEvent(KeyboardAction KeyAction, KeyboardKey keyValue) {
            if(KeyAction == KeyboardAction.KeyRelease) {
                player.KeyRelease(keyValue);
                switch(keyValue) {
                    case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent{EventType = GameEventType.GameStateEvent, 
                        From = this,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_PAUSED"});
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

        public void IterateBlocks() {
            blocks.Iterate(block => {
                if (block.HitPoint == 0) {
                    block.DeleteEntity();
                }
            });
        }

        public Vec2F BounceDirection(CollisionDirection dir, Ball ball) {
            if (dir == CollisionDirection.CollisionDirUp || dir == CollisionDirection.CollisionDirDown) {
                                    return new Vec2F(ball.Shape.AsDynamicShape().Direction.X, 
                                        ball.Shape.AsDynamicShape().Direction.Y*-1);
            } 
            if (dir == CollisionDirection.CollisionDirLeft || dir == CollisionDirection.CollisionDirRight) {
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
                if (ball.Shape.Position.Y < 0.0f) {
                    ball.DeleteEntity();
                }
                if (ball.Shape.Position.X + ball.Shape.AsDynamicShape().Direction.X <= 0.0f || ball.Shape.Position.X+ball.Shape.Extent.X >= 1.0f) {
                    ball.Shape.AsDynamicShape().ChangeDirection(new Vec2F(ball.Shape.AsDynamicShape().Direction.X*-1,
                        ball.Shape.AsDynamicShape().Direction.Y));
                } else {
                blocks.Iterate(block => {
                    if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).Collision) { 
                        block.HitPoint -= 1;
                        ball.Shape.AsDynamicShape().ChangeDirection(BounceDirection(
                            CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).CollisionDir, ball));
                        if(block.HitPoint < 2) { //doesn't remove
                            block.DeleteEntity();
                        }  
                    }
                });}
            });
        }
    }
}

                            // switch ("{0}{0}{0}{0}" ball.Shape.Position.X < block.Shape.Position.X, ball.Shape.Position.X > block.Shape.Position.X,
                            //   ball.Shape.Position.Y < block.Shape.Position.Y, ball.Shape.Position.Y > block.Shape.Position.Y)                                
//switch ("{0}{0}{0}{0}" ball.Shape.Position.X < block.Shape.Position, ball.Shape.Position.X > block.Shape.Position,
//   ball.Shape.Position.Y < block.Shape.Position, ball.Shape.Position.Y > block.Shape.Position)

//case "TRUE, TRUE, TRUE, TRUE":
                            // switch (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).CollisionDir) {
                            //     case CollisionDirection.CollisionDirUp :
                            //         new Vec2F(ball.Shape.AsDynamicShape().Direction.X, 
                            //             ball.Shape.AsDynamicShape().Direction.Y*-1);
                            //     break;
                            //     case CollisionDirection.CollisionDirDown :
                            //         new Vec2F(ball.Shape.AsDynamicShape().Direction.X,
                            //             ball.Shape.AsDynamicShape().Direction.Y*-1);
                            //     break;
                            //     case CollisionDirection.CollisionDirLeft :
                            //         new Vec2F(ball.Shape.AsDynamicShape().Direction.X*-1,
                            //             ball.Shape.AsDynamicShape().Direction.Y);
                            //     break;                                  
                            //     case CollisionDirection.CollisionDirRight :
                            //         new Vec2F(ball.Shape.AsDynamicShape().Direction.X*-1,
                            //             ball.Shape.AsDynamicShape().Direction.Y);
                            //     break;
                            //     case default :
                            //         ball.Shape.AsDynamicShape().Direction;
                            //         break;
                                    
                            // }