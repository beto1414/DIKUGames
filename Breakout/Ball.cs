using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO; 

namespace Breakout {

    public class Ball : Entity {
        // private Vec2F Extent = new Vec2F(0.05f, 0.05f);
        //private Vec2F Direction = new Vec2F(0.0f, 0.0f);


        public Ball(Vec2F Position, Vec2F Direction) 
            : base (new DynamicShape(Position, new Vec2F(0.05f, 0.05f), Direction), new Image(Path.Combine("Assets", "Images", "ball2.png"))) {                
            }

        //public void Move() {}
        //}

        // public void ChangeDirection(Vec2F direction) {
        //     this.Direction = direction;
        // }





        
    }
}