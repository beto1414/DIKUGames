 using DIKUArcade.Entities;
 using DIKUArcade.Graphics;
 using System;
 using Breakout.PowerUps;

namespace Breakout.Blocks {
    public class PowerUpBlock : Block {
        public Random rand = new Random();
        public PowerUpBlock (Shape shape, Image image) : base(shape, image) {
            visible = true;
            hitPoint = 1; 
            unbreakable = false; 
            blockType = BlockType.PowerUpBlock;
            blockValue = 5;
        }

        public override void Hit() {
            hitPoint -= 1;
            // var temp = rand.Next(5);
            //                     switch(temp) {
            //                         case 0:
            //                             GameRunning.powerUps.AddEntity(new DoubleSpeed(Shape.Position + Shape.Extent/2));
            //                             break;
            //                         case 1:
            //                             powerUps.AddEntity(new ExtraLife(block.Shape.Position + block.Shape.Extent/2));
            //                             break;
            //                         case 2:
            //                             powerUps.AddEntity(new HalfSpeed(block.Shape.Position + block.Shape.Extent/2));
            //                             break;
            //                         case 3:
            //                             powerUps.AddEntity(new ExtraBall(block.Shape.Position + block.Shape.Extent/2));
            //                             break;
            //                         case 4:
            //                             powerUps.AddEntity(new Infinite(block.Shape.Position + block.Shape.Extent/2));
            //                             break;
            //                     }
        }

    }
}