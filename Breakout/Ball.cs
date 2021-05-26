using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO; 

namespace Breakout {

    public class Ball : Entity {
        public static float MOVEMENT_SPEED = 0.03f;

        public Ball(Vec2F Position, Vec2F Direction) 
            : base (new DynamicShape(Position, new Vec2F(0.03f, 0.03f), Direction), new Image(Path.Combine("Assets", "Images", "ball2.png"))) {                
            }

        public void AlignSpeed() {
            var x = Shape.AsDynamicShape().Direction.X;
            var y = Shape.AsDynamicShape().Direction.Y;
            var c = (Convert.ToSingle(MOVEMENT_SPEED/(Math.Sqrt(Math.Pow(x,2.0) + Math.Pow(y,2.0)))));
            Shape.AsDynamicShape().ChangeDirection(new Vec2F(c*x, c*y));
        }



        
    }
}