using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;
using Breakout.BreakoutStates;
using System;

namespace Breakout.PowerUps{
    public class Infinite : PowerUp {
        private Random rand;
        public Infinite(DynamicShape Shape, IBaseImage Image) : base(Shape, Image) {
            image = new Image(Path.Combine("Assets", "Images", "InfinitePowerUp.png"));
            rand = new Random();
        }

        public override void Activate() {
            for (int i = 0;i<5;i++) {
                GameRunning.balls.AddEntity(new Ball(new Vec2F(GameRunning.player.getShape().Position.X + GameRunning.player.getExtent()/2,GameRunning.player.getShape().Position.Y),new Vec2F((float)rand.NextDouble()-rand.Next(1),(float)rand.NextDouble())));
            }
            
        }
    }
}