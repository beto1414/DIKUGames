using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;



namespace Breakout.LevelLoading {
    public class Block : Entity {

        public Shape shape;
        public IBaseImage image;
        public int HitPoint;


        public Block (Shape Shape, IBaseImage Image, int Health) 
            : base (Shape, Image) {
                HitPoint = Health;
            }
        
    }
}