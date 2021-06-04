using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math; 
using System; 
using Breakout.BreakoutStates; 
using DIKUArcade.Utilities;


namespace Breakout.PowerUps { 
    public class ExtraBall : PowerUp {
        private Random rand;
        ///<summary> Gør det muligt at tilføje en ekstra bold i en entity container</summary>
        public ExtraBall(Vec2F Position)
         : base(Position, new Image(Path.Combine(FileIO.GetProjectPath(),Path.Combine
            ("Assets", "Images", "ExtraBallPowerUp.png")))) {
                rand = new Random();
            }
///<summary>
///Adds a new ball entity whenever the powerup is picked up.
///</summary>
        public override void Activate() {
                GameRunning.balls.AddEntity(new Ball(
                    new Vec2F(GameRunning.player.getShape().Position.X + GameRunning.player.getExtent()/2,GameRunning.player.getShape().Position.Y),
                    new Vec2F((float)rand.NextDouble()-rand.Next(1),(float)rand.NextDouble())));
                foreach(Ball x in GameRunning.balls) {x.AlignSpeed();}
        }
    }
}