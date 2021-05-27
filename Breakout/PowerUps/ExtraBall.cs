using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math; 
using System; 
using Breakout.BreakoutStates; 


namespace Breakout.PowerUps { 
    public class ExtraBall : PowerUp {
        private Random rand;
        ///<summary> Gør det muligt at tilføje en ekstra bold i en entity container</summary>
        public ExtraBall(DynamicShape Shape, IBaseImage Image)
         : base(Shape, Image) {
             image = new Image(Path.Combine("Assets", "Images", "ExtraBallPowerUp.png"));
         }
        public override void Activate() {
            GameRunning.balls.AddEntity(new Ball(new Vec2F(GameRunning.player.getShape().Position.X, GameRunning.player.getShape().Position.Y + 0.03f), 
            (new Vec2F((float)rand.NextDouble()-rand.Next(1),(float)rand.NextDouble()))));
        }
    }
}


