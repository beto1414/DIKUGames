using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;



namespace LevelLoading {
    public class Block : Entity {

        public Shape shape;
        public IBaseImage image;
        public int HealthPoint;


        public Block (Shape Shape, IBaseImage Image) 
            : base (Shape, Image) {
                HealthPoint = 1;

            }
    }
}

    // public class Enemy : Entity {
    //     public Shape shape {get; set;}
    //     public IBaseImage image {get;set;}
    //     public int hitpoints = 3;
    //     public Enemy(DynamicShape Shape, IBaseImage Image)
    //         : base(Shape, Image) {}
    //     public DynamicShape AsDynamicShape() {
    //         return shape.AsDynamicShape();