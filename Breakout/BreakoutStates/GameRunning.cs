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

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState{
        public EntityContainer<Block> blocks = new EntityContainer<Block>();
        public EntityContainer<Ball> balls;
        private static GameRunning instance = null;
        private Entity backGroundImage;
        private bool GameState = true;
        private Player player;
        private ScoreBoard scoreBoard;
        private Random rand;
        public MetaReader normalBlock;
        public string[] levels = new String[]{"level1.txt","level2.txt","level3.txt","level4.txt"};
        public int counter = 0;

        private void GameOver() {
            GameState = false;
        }
        
        public void ResetState() { //skriv constructoren her
            // normalBlock = new MetaReader (" ");
            // normalBlock.blockType = BlockType.Normal;
            //levels = new String[]{"level1.txt","level2.txt","level3.txt","level4.txt"};
            counter = 0;
            Loader.Reader(Path.Combine("Assets","Levels",levels[counter]));
            Console.WriteLine(counter);
            Console.WriteLine(levels[counter]);
            blocks = Loader.DrawMap();
            backGroundImage = new Entity(
                new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            player = new Player(new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.20f, 0.05f)), 
                new Image(Path.Combine("Assets","Images","player.png")));
            rand = new System.Random();
            balls = new EntityContainer<Ball>();
            balls.AddEntity(new Ball(new Vec2F(0.5f, 0.1f),new Vec2F(-0.02f,0.01f)));
            foreach(Ball x in balls) {x.AlignSpeed();}
            scoreBoard = new ScoreBoard("Score: ",new Vec2F(0.0f,0.6f), new Vec2F(0.4f,0.4f));
            //counter += 1;
            //Convert.ToSingle(rand.Next(10)/10)
            
        }
        public void UpdateState() {
            IterateBallz();
            player.Move();
            if (blocks.CountEntities() - CountUnbreakable() == 0) {
                counter++;
                balls.ClearContainer();
                balls.AddEntity(new Ball(new Vec2F(0.5f, 0.1f),new Vec2F(-0.02f,0.01f)));
                foreach(Ball x in balls) {x.AlignSpeed();}
                Loader.Reader(Path.Combine("Assets","Levels",levels[counter]));
                blocks = Loader.DrawMap();
                Console.WriteLine("resetting now"); 
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
            if (GameState) {player.Render();}
            if (GameState) {scoreBoard.RenderText();} 
            //if (GameState) {blocks.RenderEntities();}
            if (GameState) {
                blocks.Iterate(block => {
                    if (block.blockType == BlockType.Invisible) {
                        if (Invisible.visible) {
                            block.RenderEntity();
                    }} else {
                    block.RenderEntity();
                    }
                });
            }
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
            //if (counter < 2) ResetState();
        }

        public static GameRunning GetInstance () {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void IterateBlocks() {
            blocks.Iterate(block => {
                if (block.HitPoint < 0) {
                    block.DeleteEntity();
                }
            });
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
                if (ball.Shape.Position.Y < 0.0f) {
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
                    if(block.HitPoint <= 0) {
                        if (!block.unbreakable) {
                            block.DeleteEntity();
                            scoreBoard.AddPoints(block.blockValue);
                        }
                        //Console.WriteLine(blocks.CountEntities());
                    }
                });}
                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.getEntity().Shape).Collision) {
                    // ball.Shape.AsDynamicShape().ChangeDirection(BounceDirection(
                    //     CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.getEntity().Shape).CollisionDir, ball));
                    if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.getEntity().Shape).CollisionDir == CollisionDirection.CollisionDirUp) {//} || 
                        //CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.getEntity().Shape).CollisionDir == CollisionDirection.CollisionDirDown) {
                        //ball.Shape.AsDynamicShape().ChangeDirection(new Vec2F(((ball.Shape.Position.X - player.getPos().X)/player.getExtent()/2.0f)*0.1f,
                        //((ball.Shape.Position.Y - player.getPos().Y)/player.getExtent()/2.0f)*0.1f));
                        //ball.AlignSpeed();
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
    }
}
